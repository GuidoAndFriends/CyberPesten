using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    partial class Spel
    {
        public List<Kaart> pot, stapel;
        public List<string> namen;
        public List<Speler> spelers;
        public Speelveld speelveld;
        public int spelend, richting, speciaal, pakAantal, aantalSpelers;
        public string status;
        public System.Timers.Timer timerAI;
        public bool mens;
        public Instellingen instellingen;

        public Spel()
        {

        }
        
        public void verplaatsKaart(List<Kaart> van, int index, List<Kaart> naar)
        {
            if (van.Count > 0)
            {
                naar.Add(van[index]);
                van.RemoveAt(index);
            }
        }

        public void verplaatsKaart(List<Kaart> van, List<Kaart> naar)
        {
            verplaatsKaart(van, van.Count - 1, naar);
        }

        public bool speelKaart(int index)
        //Legt een kaart met de gegeven index van degene die aan de beurt is op de stapel. Geeft true bij een geldige kaart, anders false
        {
            List<Kaart> hand = spelers.ElementAt(spelend).hand;
            Kaart kaart = hand[index];
            if (speelbaar(kaart))
            {
                //Controle bij laatste kaart
                if (hand.Count == 1)
                {
                    if (spelers[spelend].gemeld)
                    {
                        speelKaartNu(hand, index, stapel);
                        if (spelend == 0)
                        {
                            status = "Jij speelde " + kaart.tekst;
                            spelers[0].updateBlok();
                        }
                        else
                        {
                            status = "Speler " + spelend + " speelde " + kaart.tekst;
                        }
                        spelers[spelend].updateBlok();
                        speelveld.Invalidate();
                        //kaartActie();
                        eindeSpel();
                        return true;
                    }
                    else
                    {
                        speelKaartNu(hand, index, stapel);
                        pakKaart(5);
                        laatsteKaart(0);
                        volgende();
                        return true;
                    }
                }
                else
                {
                    speelKaartNu(hand, index, stapel);
                    if (spelend == 0)
                    {
                        status = "Jij speelde " + kaart.tekst;
                    }
                    else
                    {
                        status = "Speler " + spelend + " speelde " + kaart.tekst;
                    }
                    spelers[spelend].updateBlok();
                    speelveld.Invalidate();
                    kaartActie();
                    laatsteKaart(0);
                    //volgende();
                    return true;
                }
            }
            else
            {
                //kaart mag niet opgelegd worden
                return false;
            }
        }

        public bool speelKaart(Kaart kaart)
        {
            return speelKaart(spelers[spelend].hand.IndexOf(kaart));
        }


        public void speelKaartNu(List<Kaart> van, int index, List<Kaart> naar)
        {
            Kaart kaart = van[index];
            Point p1;
            if (spelend == 0)
            {
                p1 = new Point(kaart.X, kaart.Y);
            }
            else
            {
                /*
                 * int breedte = (spel.spelers.Count - 1) * 350;
                int tussenruimte = (Width - breedte - 20) / (spel.spelers.Count - 2);
                for (int i = 1; i < spel.spelers.Count; i++)
                {
                    gr.DrawImage(spel.spelers[i].blok, 10 + (350 + tussenruimte) * (i - 1), 10);
                }
                 */

                int breedte = (spelers.Count - 1) * 350;
                int tussenruimte = (speelveld.Width - breedte - 20) / (spelers.Count - 2);
                p1 = new Point(10 + (350 + tussenruimte) * (spelend - 1) + 120, 10);
            }
            speelveld.bewegendeKaart = kaart;
            van.RemoveAt(index);
            speelveld.verplaatsen(p1, speelveld.stapelPlek, index);
            naar.Add(speelveld.bewegendeKaart);

        }

        public void pakKaart()
        //geeft de bovenste kaart van de pot aan degene die aan de beurt is.
        {
            if (speciaal == 4)
            {
                regelPakkenNu();
            }
            else
            {
                //als de pot leeg is, gaan de kaarten die opgelegd zijn op de stapel naar de pot, op de bovenste kaart na
                if (pot.Count == 0)
                {

                    int boven = stapel.Count - 1;
                    Kaart bovenste = stapel[boven];
                    stapel.RemoveAt(boven);

                    pot = stapel;
                    pot = schud(pot);

                    stapel = new List<Kaart>();
                    stapel.Add(bovenste);
                }

                verplaatsKaart(pot, 0, spelers[spelend].hand);

                if (spelend == 0)
                {
                    status = ("Je kon niet en hebt een kaart gepakt");

                }
                else
                {
                    status = ("Speler " + spelend + " kon niet en heeft een kaart gepakt");
                }
                spelers[spelend].updateBlok();
                speelveld.Invalidate();
            }
        }

        public void pakKaart(int aantal)
        {
            for (int a = 0; a < aantal; a++)
            {
                pakKaart();
            }
        }

        public List<Kaart> schud(List<Kaart> stapel)
        {
            int i;
            Random r = new Random();
            List<Kaart> geschud = new List<Kaart>();
            while (stapel.Count > 0)
            {
                //verplaatst een willekeurige kaart van de oorspronkelijke stapel naar de geschudde stapel
                i = r.Next(stapel.Count);
                verplaatsKaart(stapel, i, geschud);
            }
            return geschud;
        }

        public void volgende()
        {
            string s = "";
            foreach (Kaart kaart in spelers[0].hand)
            {
                s+= kaart.ToString();
            }
            System.Diagnostics.Debug.WriteLine(s);

            Speler oud = spelers[spelend];

            spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            if (!(mens) & spelend == 0)
            {
                spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            }

            oud.updateBlok();
            
            if (spelend != 0)
            {
                spelers[spelend].updateBlok();
                if (mens)
                {
                    timerAI.Interval = 1000;
                    //Eventueel nog random
                }
                else
                {
                    timerAI.Interval = 10; 
                }
                timerAI.Start();
            }
        }
        
        public void tijd(object sender, EventArgs ea)
        {
            timerAI.Stop();
            spelers[spelend].doeZet();
        }

        public void laatsteKaart(int sender)
        {
            if (sender == 1)
            {
                spelers[0].gemeld = true;
                //speelveld.laatsteKaart.BackColor = Color.Green;
                
            }
            else
            {
                spelers[0].gemeld = false;
                //speelveld.laatsteKaart.BackColor = Color.Red;
           }
        }

        public void eindeSpel()
        {
            MessageBox.Show("Speler " + spelend + " heeft gewonnen");
        }

        public void veranderKleur(int kleur)
        {

        }


        public Speler willekeurigeAI()
        {
            Random random = new Random();
            string naam = namen[random.Next(namen.Count)];
            namen.Remove(naam);
            
            int aantal = 1;
            int nummer = random.Next(aantal);
            Speler gekozen;

            switch (nummer)
            {
                case 0:
                    gekozen = new AI1Random(this, naam);
                    break;
                case 1:
                    gekozen = new AI2Pester(this, naam);
                    break;
                /*
                case 2:
                    gekozen = new AI3Oke(this, naam);
                    break;
                 */
                default:
                    MessageBox.Show("Er is iets mis in de functie willekeurigeAI");
                    gekozen = new AI1Random(this, naam);
                    break;
            }

            return gekozen;
        }
    }
}
