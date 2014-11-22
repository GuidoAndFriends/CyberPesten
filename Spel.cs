using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Spel
    {
        public Spel()
        {
            List<Speler> spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            List<Kaart> oplegstapel = new List<Kaart>();
            List<Kaart> pot = new List<Kaart>();
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

            System.Diagnostics.Debug.WriteLine("Er zijn  nu " + spelers.Count + " spelers.");
        }

        public List<Kaart> schud(List<Kaart> stapel)
        {
            return stapel;
        }
    }
}
