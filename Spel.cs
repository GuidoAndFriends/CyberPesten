using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class Spel
    {
        public List<Kaart> pot;
        public List<Speler> spelers;
        public List<Kaart> stapel;
        public Speelveld speelveld;
        public bool bezig;
        public int spelend;
        public int richting;

        public Spel(Speelveld s, int aantalAI)
        {
            speelveld = s;
            bezig = false;
            spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            int kaartspellen = (aantalAI + 1) / 4 + 1; //4 spelers per pak kaarten?
            int startkaarten = 7;
            spelend = 0;
            richting = 1;

            //Spelers toevoegen
            spelers.Add(new Mens());
            for (int i = 0; i < aantalAI; i++)
            {
                spelers.Add(new Guido());
            }

            //Kaarten toevoegen
            for (int i = 0; i < kaartspellen; i++)
            {
                for (int j = 0; j <4; j++)
                {
                    for (int k =1; k<14;k++)
                    {
                    pot.Add(new Kaart(j,k));
                    }
                }
            }

            //Kaarten schudden
            pot = schud(pot);

            //Kaarten delen
            for (int i = 0; i < startkaarten; i++)
            {
                foreach (Speler speler in spelers)
                {
                    verplaatsKaart(pot, speler.hand);
                    if (spelend == 0)
                    {
                        speler.hand[speler.hand.Count - 1].X = 100 + 100 * speler.hand.Count * 100;
                    }
                }
            }

            verplaatsKaart(pot, 0, stapel);

            bezig = true;
            s.Invalidate();

            System.Diagnostics.Debug.WriteLine("Er zijn  nu " + spelers.Count + " spelers.");
            System.Diagnostics.Debug.WriteLine("De bovenste kaart op de stapel is " + stapel.ElementAt(stapel.Count - 1).tekst);
            System.Diagnostics.Debug.WriteLine("De speler heeft " + spelers.ElementAt(0).hand.Count + " kaarten");
            System.Diagnostics.Debug.WriteLine("Er zitten nog " + pot.Count + " kaarten in de pot");
        }

        public bool speelKaart(int index)//Legt een kaart met de gegeven index van de doelwit diens hand op de stapel. Geeft true bij een geldige kaart, anders false
        {
            List<Kaart> hand = spelers.ElementAt(spelend).hand;
            Kaart k = hand[index];
            if (isLegaal(k))
            {
                verplaatsKaart(hand, index, stapel);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isLegaal(Kaart k)//MOET NOG AANGEPAST WORDEN
        {
            bool result = false;
            if (k.Kleur == stapel[stapel.Count - 1].Kleur || k.Waarde == stapel[stapel.Count - 1].Waarde||k.Kleur==5)
            {
                result = true;
            }

            return result;
        }

        public void pakKaart(Speler doelwit)//geeft de bovenste kaart van de pot aan het doelwit, als er geen kaart gepakt kan worden, dan wordt de stapel de nieuwe pot. de bovenste kaart van de stapel blijft liggen.
        {
            Kaart i;
            if (pot.Count == 0)//wanneer de pot leeg is
            {
                int a = stapel.Count-1;
                i = stapel[a];//haal de bovenste kaart van de stapel
                stapel.RemoveAt(a);

                pot = stapel;//pak de stapel en maak er de pot van
                stapel = new List<Kaart>();//maak de opleg stapel leeg.

                stapel.Add(i);//leg de bovenste kaart terug
                pot = schud(pot);//en schud de pot
            }

            verplaatsKaart(pot, 0, doelwit.hand);
        }

        public void pakKaart(Speler doelwit, int aantal)
        {
            for(int a = 0; a<aantal;a++)
            {
                pakKaart(doelwit);
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
            Random r = new Random(DateTime.Today.Millisecond);//zorgt ervoor dat we elke keer een stapel op een andere manier schudden.//Gaat dat niet altijd met random.next?
            List<Kaart> geschud = new List<Kaart>();
            while (stapel.Count > 0)
            {
                i = r.Next(stapel.Count);
                verplaatsKaart(stapel, i, geschud);
            }
            return geschud;
        }
    }
}
