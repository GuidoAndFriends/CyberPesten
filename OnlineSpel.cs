using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace CyberPesten
{
    class OnlineSpel : Spel 
    {
        Random rnd = Online.onlineRandom;
        Thread data_thread;
        bool spel_loopt;
        List<string> chatregels,actieregels;
        List<Speler> onlineSpelers;
        public int actieCount;

        public OnlineSpel(Speelveld s)
        {
            actieCount = 0;
            speelveld = s;
            spelers = new List<Speler>();
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            mens = true;
            aantalSpelers = Online.deelnemers.Count();
            spel_loopt = true;
            data_thread = new Thread(data);
            data_thread.IsBackground = true;
            data_thread.Name = "Data";
            data_thread.Start();
            chatregels = new List<string>();
            actieregels = new List<string>();

            int kaartspellen = (aantalSpelers) / 4 + 1; //hoeveel kaartspellen gebruikt worden
            int startkaarten = 7; //hoeveel kaarten de spelers in het begin krijgen
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0; //hoeveel kaarten er gepakt moeten worden (voor 2 en joker)
            speciaalTekst = "-1 normaal";
            bezig = true;
            //Online: eigen variant met chat?//nee, we kunnen prima dingen hierin doen.
            chat = new Chat();

            int einde = aantalSpelers * 2;
            for (int i = 0; i < einde; i++)
            {
                if (Online.deelnemers[i % aantalSpelers].Split(':')[0] == Online.username)
                {
                    spelers.Add(new OnlineMens(this, i));
                    einde = i + aantalSpelers - 1;
                }
                else
                {
                    if (einde != aantalSpelers * 2)
                    {
                        spelers.Add(new OnlineSpeler(this, Online.deelnemers[i%aantalSpelers].Split(':')[0], i % aantalSpelers));
                    }
                }   
            }
            onlineSpelers = new List<Speler>();//spelers gesorteerd op online index (feitelijke volgorde)
            foreach (Speler r in spelers)
            {
                onlineSpelers.Add(new Mens(this));
            }
            foreach (Speler r in spelers)
            {
                
                if(r.GetType() == typeof(OnlineSpeler)){
                    OnlineSpeler b = (OnlineSpeler) r;
                    onlineSpelers[b.OnlineIndex] = b;
                }
                if (r.GetType() == typeof(OnlineMens))
                {
                    OnlineMens b = (OnlineMens)r;
                    onlineSpelers[b.OnlineIndex] = b;
                }
            }
            spelend = Online.onlineRandom.Next(aantalSpelers);//iedereen heeft dezelfde seed, dus dit gaat goed.


                //Kaarten toevoegen
                for (int i = 0; i < kaartspellen; i++)
                {
                    extraPak(pot);
                }
            pot = schud(pot);
            aantalKaarten = pot.Count.ToString();

            //Kaarten delen
            for (int i = 0; i < startkaarten; i++)
            {
                for (int j = 0; j < spelers.Count; j++)
                {
                    verplaatsKaart(pot, onlineSpelers[j].hand);//onlineSpelers is voor iedereen hetzelfde, dus iedeen krijgt de juiste kaarten.
                }
            }

            foreach (Speler speler in spelers)
            {
                speler.updateBlok();
            }
            verplaatsKaart(pot, 0, stapel);
            if (stapel[0].Kleur == 4)
            {
                speciaal = 5;
            }
            s.Invalidate();
            checkNullKaart();
        }

        protected override List<Kaart> schud(List<Kaart> stapel)
        {
            int i;
            List<Kaart> geschud = new List<Kaart>();
            while (stapel.Count > 0)
            {
                i = rnd.Next(stapel.Count());//omdat iedereen dezelfde seed heeft, levert dit dezelde stapel op.
                verplaatsKaart(stapel,i,geschud);
            }
            schudSound();
            return geschud;
        }

        private void schudSound()
        {
            Stream s = CyberPesten.Properties.Resources.schudden;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        void onlineLaatsteKaart(bool melden, int speler)
        {
            if (melden) { 
                Speler r = onlineSpelers[speler];
                if (r.hand.Count == 1) {
                    r.gemeld = true;
                }else
                {
                    r.gemeld = false;
                }

                }
            }

        public void data()
        {
            while (spel_loopt)
            {
                //pull data
                string raw = Online.PHPrequest("http://harbingerofme.info/GnF/read_messages.php", new string[] { "name", "token", "gameid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                string[] lines = raw.Split(new string[] {"|"},StringSplitOptions.RemoveEmptyEntries);
                if (raw == "Error: Game bestaat niet!")
                {
                    OnlineSpeelveld a = (OnlineSpeelveld)speelveld;
                    a.stop();
                }
                if (!raw.StartsWith("Error:"))
                {
                    for (int a = chatregels.Count(); a < lines.Count(); a++)
                    {
                        chatregels.Add(lines[a]);
                        chat.nieuw("<" + lines[a].Split(':')[0] + ">: " + lines[a].Split(new string[] { ":" }, 2, StringSplitOptions.None)[1]);
                    }
                }

                raw = Online.PHPrequest("http://harbingerofme.info/GnF/read_action.php", new string[] { "name", "token", "gameid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                if (raw != "" && !raw.StartsWith("Error:"))
                {
                    raw = raw.Substring(1);//het start met een "|"
                    actieregels = raw.Split('|').ToList();
                    doe_acties();
                }
                if (raw == "Error: Game bestaat niet!")
                {
                    OnlineSpeelveld a = (OnlineSpeelveld)speelveld;
                    a.stop();
                }
                Thread.Sleep(1000);
            }
        }

        public void doe_acties()
        {
            try
            {
                for (int a = actieCount; a < actieregels.Count; a++)
                {
                    string[] splits = actieregels[a].Split(':');
                    Speler s = onlineSpelers[int.Parse(splits[0])];
                    switch (splits[1])
                    {
                        case "lK": s.gemeld = true; break;
                        case "eV": pakKaart(); break;
                        case "rPN": regelPakkenNu(); break;
                        case "pK": pakKaart(); break;
                        case "sK": speelKaart(int.Parse(splits[2])); break;
                        case "bK": Kaart k = s.hand[int.Parse(splits[2])]; s.hand.Remove(k); s.hand.Add(k); break;
                        case "kV": speciaal = int.Parse(splits[2]); string tekst = s.naam + "koos voor ";
                            switch (speciaal)
                            {
                                case 0: tekst += "Harten."; break;
                                case 1: tekst += "Klaver."; break;
                                case 2: tekst += "Ruiten."; break;
                                case 3: tekst += "Schoppen."; break;
                            }
                            chat.nieuw(tekst);
                            break;
                    }
                }
            }
            catch
            {
            MessageBox.Show("Kritieke fout, spel is afgesloten, er is niks van je rating afgetrokken");
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/leave_game.php", new string[] { "name", "token" , "spelid","spelerror"}, new string[] { Online.username, Online.token, Online.game.ToString(), "waarde"});
            OnlineSpeelveld a = (OnlineSpeelveld)speelveld;
            a.stop();
            }
        }
    }
}
