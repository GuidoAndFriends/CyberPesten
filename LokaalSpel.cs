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
        public LokaalSpel(Speelveld s, Instellingen _instellingen)
        {
            speelveld = s;
            spelers = new List<Speler>();
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            instellingen = _instellingen;
            mens = instellingen.mensSpelend;
            aantalSpelers = instellingen.aantalSpelers;

            int kaartspellen = (aantalSpelers) / 4 + 1; //hoeveel kaartspellen gebruikt worden
            int startkaarten = 7; //hoeveel kaarten de spelers in het begin krijgen
            //welke speler aan de beurt is
            if (mens)
            {
                spelend = 0;
            }
            else
            {
                spelend = 1;
            }
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0; //hoeveel kaarten er gepakt moeten worden (voor 2 en joker)
            speciaalTekst = "-1 normaal";

            timerAI = new System.Timers.Timer();
            timerAI.Elapsed += tijd;

            //Spelers toevoegen
            namen = new List<string>();
            namen.Add("Guido");
            namen.Add("Ayco");
            namen.Add("Kaj");
            namen.Add("Mehul");
            namen.Add("Noah");
            namen.Add("Norico");
            namen.Add("Rik");
            namen.Add("Sjaak");

            spelers.Add(new Mens(this));
            for (int i = 1; i < aantalSpelers; i++)
            {
                spelers.Add(willekeurigeAI());
            }

            //Kaarten toevoegen
            for (int i = 0; i < kaartspellen; i++)
            {
                extraPak(pot);
            }
            pot = schud(pot);
            aantalKaarten = pot.Count.ToString();

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
            if (stapel[0].Kleur == 4)
            {
                speciaal = 5;
            }
            s.Invalidate();
            if (! mens)
            {
                spelend++;
                spelers[spelend].doeZet();
            }
            checkNullKaart();
        }
    }
}
