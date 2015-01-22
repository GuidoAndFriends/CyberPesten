using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class OnlineSpel : Spel 
    {
        Random rnd = Online.onlineRandom;
        public OnlineSpel(Speelveld s)
        {
            speelveld = s;
            spelers = new List<Speler>();
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            mens = true;
            aantalSpelers = Online.deelnemers.Count();

            int kaartspellen = (aantalSpelers) / 4 + 1; //hoeveel kaartspellen gebruikt worden
            int startkaarten = 7; //hoeveel kaarten de spelers in het begin krijgen
            //Online: is de beginnende speler altijd 0?
            spelend = 0; //welke speler aan de beurt is (zonder mens wordt aan het einde van de constructormethode afgehandeld)
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0; //hoeveel kaarten er gepakt moeten worden (voor 2 en joker)
            speciaalTekst = "-1 normaal";
            bezig = true;
            //Online: eigen variant met chat?
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
                int j = 0;
                if (!mens)
                {
                    j++;
                }
                for (; j < spelers.Count; j++)
                {
                    verplaatsKaart(pot, spelers[j].hand);
                }
            }

            foreach (Speler speler in spelers)
            {
                speler.updateBlok();
            }
            verplaatsKaart(pot, 0, stapel);
            //stapel.Add(new Kaart()); als je wilt testen hoe het gaat als de eerste kaart een joker is
            if (stapel[0].Kleur == 4)
            {
                speciaal = 5;
            }
            s.Invalidate();
            /*
            //Online: niet van toepassing
            if (!mens)
            {
                spelend++;
            }
            */
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
            return geschud;
        }
    }
}
