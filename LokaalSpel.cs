using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace CyberPesten
{
    class LokaalSpel : Spel
    {
        public LokaalSpel(Speelveld s, int aantalSpelers)
        {
            speelveld = s;
            spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            int kaartspellen = (aantalSpelers) / 4 + 1; //hoeveel kaartspellen gebruikt worden
            int startkaarten = 7; //hoeveel kaarten de spelers in het begin krijgen
            spelend = 0; //welke speler aan de beurt is
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0;

            timer = new System.Timers.Timer();
            timer.Elapsed += tijd;

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
                for (int j = 0; j < 2; j++)
                {
                    pot.Add(new Kaart(4));
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
            s.Invalidate();
        }
    }
}
