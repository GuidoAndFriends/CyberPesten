using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    public class Kaart
    {
        private int _kleur; //integere getal van de kleur, 0 = harten, 1 = klaver, 2 = ruiten, 3= schoppen (alfabetische volgorde) en 4 = joker (kleurloos)
        private int _waarde; //waarde van de kaart, 0 doet niet mee, 1 is een aas, 2-10 komen overeen met de nummers zelf, 11 boer, 12 vrouw, 13 heer.

        public Kaart(int k, int w)//maakt een nieuwe kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        {
            if (k > -1 && k < 5 && w > 0 && w < 14)
            {
                _kleur = k;
                _waarde = w;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Kaart: argument out of range. k was: " + k + " and w was: " + w + ".");
            }
        }

        public Kaart(int k)//maakt een nieuwe joker
        {
            if (k == 4)
            {
                _kleur = 4;
                _waarde = 0;
            }
            else
            {
                throw new ArgumentException("Ongeldige waarde, deze methode accepteerd alleen maar 4 als parameter");
            }
        }

        public int kleur //maak of verkrijgt de kleur van een kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        {
            get { return _kleur; }
            set
            {
                if (value > -1 && value < 5)
                {
                    _kleur = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Kleur niet binnen accepteerbare normen.");
                }
            }
        }

        public int waarde
        {
            get { return _waarde; }
            set
            {
                if (value > 0 && value < 14)
                {
                    _waarde = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Waarde niet binnen accepteerbare normen.");
                }
            }
        }

        public string tekst//geeft een geschreven versie van een kaart (niet dat we die nodig hebben, maar het is fijn om te hebben)
        {
            get
            {
                string a="";
                switch (_kleur)
                {
                    case 0: a = "Harten ";   break;
                    case 1: a = "Klaver ";   break;
                    case 2: a = "Ruiten ";   break;
                    case 3: a = "Schoppen "; break;
                    case 4: a = "Joker";     break;
                }
                if (_kleur != 4)
                {
                    switch (_waarde)
                    {
                        case 1: a += "Aas";         break;
                        case 11: a += "Boer";       break;
                        case 12: a += "Vrouw";      break;
                        case 13: a += "Heer";       break;
                        default: a += "" + waarde;  break;
                    }
                }
                return a;
            }
        }
    }

        public Bitmap bitmap
        {
            get
            {
                Bitmap b = new Bitmap(140, 80);
                Brush kwast;
                if (this.kleur % 2 == 0)
                {
                    kwast = Brushes.Red;
                }
                else
                {
                    kwast = Brushes.Black;
                }
                Graphics gr = Graphics.FromImage(b);
                gr.FillRectangle(Brushes.White, 0, 0, b.Width, b.Height);
                gr.DrawString(this.tekst, new Font(FontFamily.GenericSansSerif, 14), kwast, new Point(10, 10));
                b.RotateFlip(RotateFlipType.Rotate90FlipNone);
                return b;
            }
        }
    }
}
