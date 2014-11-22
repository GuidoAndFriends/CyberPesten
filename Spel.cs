using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Spel
    {
        public List<Kaart> pot;
        public List<Speler> spelers;
        public List<Kaart> stapel;

        public Spel(int aantalAI)
        {
            spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            int kaartspellen = (aantalAI + 1) / 4 + 1; //4 spelers per pak kaarten?

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
                pot.Add(new Kaart(4));//2 jokers per kaartspel.
                pot.Add(new Kaart(4));
            }
            pot = schud(pot);
            verplaatsKaart(pot, 0, stapel);

            System.Diagnostics.Debug.WriteLine("Er zijn  nu " + spelers.Count + " spelers.");
            System.Diagnostics.Debug.WriteLine("De bovenste kaart op de stapel is " + stapel[stapel.Count - 1].tekst);
            System.Diagnostics.Debug.WriteLine("Er zitten nog " + pot.Count + " kaarten in de pot");
        }

        public bool speelKaart(Speler doelwit, int index)//Legt een kaart met de gegeven index van de doelwit diens hand op de stapel. Geeft true bij een geldige kaart, anders false
        {
            List<Kaart> hand = doelwit.hand;
            Kaart k = hand[index];
            if (isLegaal(k))
            {
                hand.RemoveAt(index);
                doelwit.hand = hand;
                stapel.Add(k);
            }
            return isLegaal(k);

        }

        public bool isLegaal(Kaart k)//MOET NOG AANGEPAST WORDEN
        {
            bool result = false;
            if (k.kleur == stapel[stapel.Count - 1].kleur || k.waarde == stapel[stapel.Count - 1].waarde||k.kleur==5)
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
            naar.Add(van[index]);
            van.RemoveAt(index);
        }

        public List<Kaart> schud(List<Kaart> stapel)
        {
            int i;
            Random r = new Random(DateTime.Today.Millisecond);//zorgt ervoor dat we elke keer een stapel op een andere manier schudden.//Gaat dat niet altijd met random.next?//In feite is de lege methode new Random() hetzelfde, maar ik vertrouw het niet, daarom maken we een expliciete nieuwe random seed.
            List<Kaart> result = new List<Kaart>();
            while(stapel.Count>0){
                i = r.Next(stapel.Count);
                result.Add(stapel[i]);
                stapel.RemoveAt(i);
            }
            return result;
        }
    }
}
