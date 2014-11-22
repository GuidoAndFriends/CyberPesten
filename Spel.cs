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
        public List<Kaart> oplegstapel;

        public Spel()
        {
            spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            oplegstapel = new List<Kaart>();
            pot = new List<Kaart>();
            int kaartspellen;//ook deze
            //Nog aanpassen aan hoeveelheid mensen en AI
            spelers.Add(new Mens());
            for (int i = 0; i < 5; i++)
            {
                spelers.Add(new Guido());
            }
            kaartspellen = 1;

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
            pot = schud(pot);
            oplegstapel.Add(pot[0]);
            pot.RemoveAt(0);

            System.Diagnostics.Debug.WriteLine("Er zijn  nu " + spelers.Count + " spelers.");
        }

        public bool speelKaart(Speler doelwit, int index)//Legt een kaart met de gegeven index van de doelwit diens hand op de oplegstapel. Geeft true bij een geldige kaart, anders false
        {
            List<Kaart> hand = doelwit.hand;
            Kaart k = hand[index];
            if (isLegaal(k))
            {
                hand.RemoveAt(index);
                doelwit.hand = hand;
                oplegstapel.Add(k);
            }
            return isLegaal(k);

        }

        public bool isLegaal(Kaart k)//MOET NOG AANGEPAST WORDEN
        {
            return true;
        }

        public void pakKaart(Speler doelwit)//geeft de bovenste kaart van de pot aan het doelwit, als er geen kaart gepakt kan worden, dan wordt de oplegstapel de nieuwe pot. de bovenste kaart van de oplegstapel blijft liggen.
        {
            Kaart i;
            if (pot.Count == 0)//wanneer de pot leeg is
            {
                int a = oplegstapel.Count-1;
                i = oplegstapel[a];//haal de bovenste kaart van de stapel
                oplegstapel.RemoveAt(a);

                pot = oplegstapel;//pak de oplegstapel en maak er de pot van
                oplegstapel = new List<Kaart>;//maak de opleg stapel leeg.

                oplegstapel.Add(i);//leg de bovenste kaart terug
                pot = schud(pot);//en schud de pot
            }

            i = pot[0];
            pot.RemoveAt(0);
            doelwit.hand.Add(i);
        }
        public void pakKaart(Speler doelwit, int aantal)
        {
            for(int a = 0; a<aantal;a++)
            {
                pakKaart(doelwit);
            }
        }

        public List<Kaart> schud(List<Kaart> stapel)
        {
            int i;
            Random r = new Random(DateTime.Today.Millisecond);//zorgt ervoor dat we elke keer een stapel op een andere manier schudden.
            List<Kaart> result = new List<Kaart>();
            while(result.Count>0){
                i = r.Next(result.Count);
                result.Add(stapel[i]);
                stapel.RemoveAt(i);
            }
            return result;
        }
    }
}
