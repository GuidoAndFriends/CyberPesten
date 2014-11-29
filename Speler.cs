﻿using System;
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
        public Spel spel;

        public abstract void maakXY();

        public abstract void doeZet();

        public Bitmap blok
        {
            get
            {
                Bitmap b = new Bitmap(290, 180);
                Graphics gr = Graphics.FromImage(b);
                for (int i = 0; i < hand.Count && i < 3; i++)
                {
                    gr.DrawImage(hand[0].achterkant, i * 100, 0);
                }
                string tekst = naam + " - " + hand.Count;
                gr.DrawString(tekst, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 145);
                return b;
            }
        }
    }
}
