using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    abstract class Speler
    {
        public List<Kaart> hand;
        public string naam;
        public bool gemeld;
        public Spel spel;
        public Bitmap blok;
        

        public abstract void doeZet();

        public virtual void updateBlok()
        {
            //Maakt een plaatje met maximaal drie kaarten en de naam, het aantal kaarten en een pijltje als de speler aan de beurt is
            //Breedte is 3 kaarten en 2 keer tussenruimte: 3 * 110 + 2 * 10 = 350
            Bitmap b = new Bitmap(350, 193, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics gr = Graphics.FromImage(b);

            for (int i = 0; i < hand.Count && i < 3; i++)
            {
                gr.DrawImage(hand[0].achterkant, i * 120, 0);
            }

            string tekst = naam + " : " + hand.Count;
            if (spel.spelers[spel.spelend] == this)
            {
                tekst += " \u25C0";
            }
            gr.DrawString(tekst, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 153 + 10);

            blok =  b;
        }

        public abstract void kiesKleurEnVolgende();
        
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
