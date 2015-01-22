using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class OnlineSpeler : Speler
    {
        public int OnlineIndex;
        public OnlineSpeler(Spel s, string Name, int index)
        {
            naam = Name;
            OnlineIndex = index;
            spel = s;
            hand = new List<Kaart>();
        }



        public virtual void updateBlok()
        {
            //Maakt een plaatje met maximaal drie kaarten en de naam, het aantal kaarten en een pijltje als de speler aan de beurt is
            //Breedte is 3 kaarten en 2 keer tussenruimte: 3 * 110 + 2 * 10 = 350
            Bitmap b = new Bitmap(350, 193, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics gr = Graphics.FromImage(b);

            for (int i = 0; i < hand.Count && i < 3; i++)
            {
                gr.DrawImage(achterkant, i * 120, 0);
            }

            string tekst = naam + " : " + hand.Count;
            if (spel.spelers[spel.spelend] == this)
            {
                tekst += " \u25C0";
            }
            gr.DrawString(tekst, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 153 + 10);

            blok =  b;
        }

        public virtual void kiesKleur()
        {
            //er wordt gekeken welke kleur het vaakst voor komt in de hand
            int[] kleuren = { 0, 0, 0, 0 };
            foreach (Kaart kaart in hand)
            {
                if (kaart.Kleur != 4) //joker
                {
                    kleuren[kaart.Kleur]++;
                }
            }
            int kleur = 0;
            for (int i = 1; i < 4; i++)
            {
                if (kleuren[i] > kleuren[kleur])
                {
                    kleur = i;
                }
            }

            spel.speciaal = kleur;
            string tekst = spel.spelers[spel.spelend].naam + " koos voor ";
            switch (kleur)
            {
                case 0: tekst += "Harten "; break;
                case 1: tekst += "Klaver "; break;
                case 2: tekst += "Ruiten "; break;
                case 3: tekst += "Schoppen "; break;
            }
            spel.chat.nieuw(tekst);
        }
        
        public int laatsteKaart
        {
            get
            {
                if (hand.Count == 1)
                {
                    if (gemeld)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    gemeld = false;
                    return 0;
                }
            }
        }
    }
}
