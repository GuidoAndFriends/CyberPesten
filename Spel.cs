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
        public List<Speler> spelers;
        public Speelveld speelveld;
        public int spelend, richting, speciaal, pakAantal;
        public string status;

        //public Spel(Speelveld s, int aantalSpelers)
        public Spel()
        {

        }

        public bool speelKaart(Kaart kaart)
        {
            return speelKaart(spelers[spelend].hand.IndexOf(kaart));
        }

        public bool speelKaart(int index)//Legt een kaart met de gegeven index van degene die aan de beurt is op de stapel. Geeft true bij een geldige kaart, anders false
        {
            List<Kaart> hand = spelers.ElementAt(spelend).hand;
            Kaart k = hand[index];
            if (speelbaar(k))
            {
                /*
                Point p1;
                if (spelend == 0)
                {
                    p1 = new Point(k.X, k.Y);
                }
                else
                {
                    p1 = new Point(10 + (290 + 40) * (spelend - 1) + 100, 10);
                }
                speelveld.bewegendeKaart = k;
                speelveld.verplaatsen2(p1, new Point(350, 500), index);
                */
                /*
                speelveld.schuifAnimatie = new Thread(speelveld.verplaatsen);
                speelveld.schuifAnimatie.Start();
                for (int i = 0; i < 5 * 50; i++)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                */

                verplaatsKaart(hand, index, stapel);
                if (spelend == 0)
                {
                    spelers[0].maakXY();
                }
                else
                {
                    status = "Speler " + spelend + " speelde " + k.tekst;
                    speelveld.Invalidate();   
                }
                kaartActie();
                //volgende();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void pakKaart() //geeft de bovenste kaart van de pot aan degene die aan de beurt is, als er geen kaart gepakt kan worden, dan wordt de stapel de nieuwe pot. de bovenste kaart van de stapel blijft liggen.
        {
            Kaart i;
            if (pot.Count == 0)
            {
                int a = stapel.Count - 1;
                i = stapel[a];//haal de bovenste kaart van de stapel
                stapel.RemoveAt(a);

                pot = stapel;//pak de stapel en maak er de pot van
                stapel = new List<Kaart>();//maak de opleg stapel leeg.

                stapel.Add(i);//leg de bovenste kaart terug
                pot = schud(pot);//en schud de pot
            }
            verplaatsKaart(pot, 0, spelers[spelend].hand);
            if (spelend == 0)
            {
                spelers[0].maakXY();
            }
            else
            {
                status = ("Speler " + spelend + " kon niet en heeft een kaart gepakt");
            }
            speelveld.Invalidate();
        }

        public void pakKaart(int aantal)
        {
            for (int a = 0; a < aantal; a++)
            {
                pakKaart();
            }
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
            if (van.Count > 0)
            {
                naar.Add(van[van.Count - 1]);
                van.RemoveAt(van.Count - 1);
            }

        }

        public List<Kaart> schud(List<Kaart> stapel)
        {
            int i;
            Random r = new Random();
            List<Kaart> geschud = new List<Kaart>();
            while (stapel.Count > 0)
            {
                i = r.Next(stapel.Count);
                verplaatsKaart(stapel, i, geschud);
            }
            return geschud;
        }

        public void volgende()
        {
            spelend = (spelend + richting) % (spelers.Count);
            if (spelend != 0)
            {
                spelers[spelend].doeZet();
            }
        }
    }
}
