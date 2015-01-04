using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CyberPesten
{
    partial class Spel
    {
        public List<Kaart> pot, stapel;
        public List<string> namen;
        public List<Speler> spelers;
        public Speelveld speelveld;
        public int spelend, richting, speciaal, pakAantal, aantalSpelers;
        public string status, aantalKaarten, speciaalTekst;
        public System.Timers.Timer timerAI;
        public bool mens;
        public Instellingen instellingen;


        public Spel()
        {

        }
        
        public void verplaatsKaart(List<Kaart> van, int index, List<Kaart> naar)
        {
            checkNullKaart();
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
            checkNullKaart();

            Kaart kaart = van[index];
            Point p1;
            if (spelend == 0)
            {
                p1 = new Point(kaart.X, kaart.Y);
            }
            else
            {
                int breedte = (spelers.Count - 1) * 350;
                int tussenruimte = (speelveld.Width - breedte - 20) / (spelers.Count - 2);
                p1 = new Point(10 + (350 + tussenruimte) * (spelend - 1) + 120, 10);
            }
            speelveld.bewegendeKaart = kaart;
            van.RemoveAt(index);
            speelveld.verplaatsen(p1, speelveld.stapelPlek, true);
            naar.Add(speelveld.bewegendeKaart);

            checkNullKaart();
        }

        public void pakKaart()
        //geeft de bovenste kaart van de pot aan degene die aan de beurt is.
        {
            checkNullKaart();

            if (speciaal == 4)
            {
                regelPakkenNu();
            }
            else
            {
                //als de pot leeg is, gaan de kaarten die opgelegd zijn op de stapel naar de pot, op de bovenste kaart na
                if (pot.Count < 2)
                {
                    //if (stapel.Count > 3)
                    {
                        int boven = stapel.Count - 1;
                        Kaart bovenste = stapel[boven];
                        stapel.RemoveAt(boven);

                        pot = stapel;
                        pot = schud(pot);

                        stapel = new List<Kaart>();
                        stapel.Add(bovenste);
                    }
                    //else
                    {
                        //extraPak(pot);
                    }
                }

                Point p2;
                if (spelend == 0)
                {
                    p2 = new Point(spelers[0].hand[spelers[0].hand.Count - 1].X + 10 + 110, spelers[0].hand[0].Y);
                }
                else
                {
                    int breedte = (spelers.Count - 1) * 350;
                    int tussenruimte = (speelveld.Width - breedte - 20) / (spelers.Count - 2);
                    p2 = new Point(10 + (350 + tussenruimte) * (spelend - 1) + 120, 10);
                }
                speelveld.bewegendeKaart = pot[0];
                pot.RemoveAt(0);
                speelveld.verplaatsen(speelveld.potPlek, p2, false);
                spelers[spelend].hand.Add(speelveld.bewegendeKaart);

                checkNullKaart();
                //verplaatsKaart(pot, 0, spelers[spelend].hand);


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

        protected List<Kaart> schud(List<Kaart> stapel)
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
            Speler oud = spelers[spelend];

            spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            if (!(mens) & spelend == 0)
            {
                spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            }

            //vanaf hier is de volgende speler spelend

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
                    timerAI.Interval = 100; 
                }
                timerAI.Start();
            }
        }
        
        protected void tijd(object sender, EventArgs ea)
        {
            timerAI.Stop();
            spelers[spelend].doeZet();
        }

        public void laatsteKaart(int sender)
        {
            Speler sjaak = spelers[spelend];
            
            if (sender == 1)
            {
                if(sjaak.hand.Count == 1)
                {
                    spelers[0].gemeld = true;
                    MessageBox.Show("Laatste kaart gemeld");
                    //speelveld.laatsteKaart.BackColor = Color.Green;
                }
                else
                {
                    spelers[0].gemeld = false;
                    MessageBox.Show("Dit is niet van toepassing");
                    //speelveld.laatsteKaart.BackColor = Color.Red;
                }                
            }
        }

        protected void eindeSpel()
        {
            if (spelend == 0)
            {
                MessageBox.Show("Gefeliciteerd, je hebt gewonnen!");
            }
            else
            {
                MessageBox.Show("Helaas, " + spelers[spelend].naam + " heeft gewonnen.");
            }
            //Terugkeren naar het menu?
        }

        public void veranderKleur(int kleur)
        {

        }


        protected Speler willekeurigeAI()
        {
            Random random = new Random();
            string naam = namen[random.Next(namen.Count)];
            namen.Remove(naam);
            
            int aantal = 4;
            int nummer = random.Next(aantal);
            Speler gekozen;

            int teller = 0;
            while (instellingen.AIUitgeschakeld.Contains(nummer) && teller < 1000)
            {
                nummer = random.Next(aantal);
                teller++;
            }
            if (teller  == 1000)
            {
                MessageBox.Show("Alle AI's zijn uitgeschakeld? Let op dat het aantal voor random.Next(aantal) 1 hoger is dan het aantal AI");
            }

            switch (nummer)
            {
                case 0:
                    gekozen = new AI0Random(this, "0 " + naam);
                    break;
                case 1:
                    gekozen = new AI1NietGek(this, "1 " + naam);
                    break;
                case 2:
                    gekozen = new AI2Pester(this, "2 " + naam);
                    break;
                case 3:
                    gekozen = new AI3Oke(this, "3 " + naam);
                    break;
                default:
                    MessageBox.Show("Er is iets mis in de functie willekeurigeAI");
                    gekozen = new AI0Random(this, naam);
                    break;
            }

            return gekozen;
        }

        protected void extraPak(List<Kaart> lijst)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 1; k < 14; k++)
                {
                    lijst.Add(new Kaart(j, k));
                }
            }
            lijst.Add(new Kaart());
        }

        public void checkNullKaart()
        {
            int aantal = 0;
            foreach (Speler speler in spelers)
            {
                foreach (Kaart kaart in speler.hand)
                {
                    aantal++;
                    if (kaart == null)
                    {
                        MessageBox.Show("null kaart gevonden in hand van speler " + speler.naam);
                    }
                }
            }
            foreach (Kaart kaart in stapel)
            {
                aantal++;
                if (kaart == null)
                {
                    MessageBox.Show("null kaart gevonden in stapel");
                }
            }
            foreach (Kaart kaart in pot)
            {
                aantal++;
                if (kaart == null)
                {
                    MessageBox.Show("null kaart gevonden in pot");
                }
            }
            aantalKaarten = aantal.ToString();
        }
    }
}
