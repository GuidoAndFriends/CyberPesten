using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    abstract class AI : Speler
    {
        /*
        public Bitmap blok
        {
            get
            {
                Bitmap b = new Bitmap(290, 160);
                Graphics gr = Graphics.FromImage(b);
                for (int i = 0; i < hand.Count && i < 3; i++)
                {
                    gr.DrawImage(hand[0].achterkant, i * 100, 0);
                }
                return b;
            }
        }
         */
    }
}
