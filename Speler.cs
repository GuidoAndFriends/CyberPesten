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
        public bool gemeld;
        public Spel spel;

        public abstract void doeZet();

        public abstract void maakXY();

        public Bitmap blok
        {
            //Maakt een plaatje met maximaal drie kaarten en de naam, het aantal kaarten en een pijltje als de speler aan de beurt is
            get
            {
                Bitmap b = new Bitmap(290, 180);
                Graphics gr = Graphics.FromImage(b);

                for (int i = 0; i < hand.Count && i < 3; i++)
                {
                    gr.DrawImage(hand[0].achterkant, i * 100, 0);
                }

                string tekst = naam + " : " + hand.Count;
                if (spel.spelers[spel.spelend] == this)
                {
                    tekst += " \u25C0";
                }
                gr.DrawString(tekst, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 145);

                return b;
            }
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
