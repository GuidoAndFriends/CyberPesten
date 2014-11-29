using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class LokaalSpel : Spel
    {
        public LokaalSpel(Speelveld s, int aantalSpelers)
        {
            speelveld = s;
            bezig = false;
            spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            int kaartspellen = (aantalSpelers) / 4 + 1; //4 spelers per pak kaarten?
            int startkaarten = 7;
            spelend = 0;
            richting = 1;

            //Spelers toevoegen
            spelers.Add(new Mens());
            for (int i = 1; i < aantalSpelers; i++)
            {
                spelers.Add(new Guido(this));
            }

            //Kaarten toevoegen
            for (int i = 0; i < kaartspellen; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 1; k < 14; k++)
                    {
                    pot.Add(new Kaart(j,k));
                    }
                }
            }
            pot = schud(pot);

            //Kaarten delen
            for (int i = 0; i < startkaarten; i++)
            {
                foreach (Speler speler in spelers)
                {
                    verplaatsKaart(pot, speler.hand);
                }
            }

            spelers[0].maakXY();
            verplaatsKaart(pot, 0, stapel);
            bezig = true;
            s.Invalidate();
        }
    }
}
