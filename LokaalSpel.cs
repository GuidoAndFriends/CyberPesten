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
        public LokaalSpel(Speelveld s, int aantalSpelers, bool mensSpelend)
        {
            speelveld = s;
            spelers = new List<Speler>();
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            mens = mensSpelend;

            int kaartspellen = (aantalSpelers) / 4 + 1; //hoeveel kaartspellen gebruikt worden
            int startkaarten = 7; //hoeveel kaarten de spelers in het begin krijgen
            spelend = 0; //welke speler aan de beurt is
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0; //hoeveel kaarten er gepakt moeten worden (voor 2 en joker)

            timerAI = new System.Timers.Timer();
            timerAI.Elapsed += tijd;

            //Spelers toevoegen
            spelers.Add(new Mens(this));
            for (int i = 1; i < aantalSpelers; i++)
            {
                spelers.Add(willekeurigeAI());
                    
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
                int j = 0;
                if (! mens)
                {
                    j++;
                }
                for (; j < spelers.Count; j++)
                {
                    verplaatsKaart(pot, spelers[j].hand);
                }
            }

            foreach(Speler speler in spelers)
            {
                speler.updateBlok();
            }
            verplaatsKaart(pot, 0, stapel);
            s.Invalidate();
            if (! mens)
            {
                spelend++;
                spelers[spelend].doeZet();
            }
        }
    }
}
