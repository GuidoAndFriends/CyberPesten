using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace CyberPesten
{
    partial class Spel
    {
        public Spel(Speelveld s, Instellingen _instellingen)
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
            spelend = 0; //welke speler aan de beurt is (zonder mens wordt aan het einde van de constructormethode afgehandeld)
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0; //hoeveel kaarten er gepakt moeten worden (voor 2 en joker)
            speciaalTekst = "-1 normaal";
            bezig = true;
            chat = new Chat(speelveld);

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
            //stapel.Add(new Kaart()); als je wilt testen hoe het gaat als de eerste kaart een joker is
            if (stapel[0].Kleur == 4)
            {
                speciaal = 5;
            }
            s.Invalidate();
            if (instellingen.regelset == 1)
            {
                spelend = (spelend - richting + spelers.Count) % (spelers.Count);
                kaartActie();
            }
            if (! mens)
            {
                spelend++;
                //Even wachten en daarna de eerste AI laten spelen
                timerAI.Interval = 1000;
                timerAI.Start();
            }
        }
    }
}
