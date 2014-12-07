using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CyberPesten
{
    public class Kaart : PictureBox
    {
        private int kleur; //integere getal van de kleur, 0 = harten, 1 = klaver, 2 = ruiten, 3= schoppen (alfabetische volgorde) en 4 = joker (kleurloos)
        private int waarde; //waarde van de kaart, 0 doet niet mee, 1 is een aas, 2-10 komen overeen met de nummers zelf, 11 boer, 12 vrouw, 13 heer.
        public Bitmap voorkant, achterkant;
        public LokaalSpel spel;

        public Kaart(int k, int w, LokaalSpel s)//maakt een nieuwe kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        //public Kaart(int k, int w)//maakt een nieuwe kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        {
            if (k > -1 && k < 5 && w > 0 && w < 14)
            {
                kleur = k;
                waarde = w;
                voorkant = voor();
                achterkant = achter();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Kaart: argument out of range. k was: " + k + " and w was: " + w + ".");
            }
            Location = new Point(0, 600);
            Size = new Size(90, 135);
            Image = voor();
            spel = s;
            Click += klik;
        }

        public Kaart(int k)//maakt een nieuwe joker
        {
            if (k == 4)
            {
                kleur = 4;
                waarde = 0;
                voorkant = voor();
                achterkant = achter();
            }
            else
            {
                throw new ArgumentException("Ongeldige waarde, deze methode accepteerd alleen maar 4 als parameter");
            }
        }

        public Bitmap voor()
        {
            Bitmap b = new Bitmap(90, 135);
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
            string beeld;
            switch (waarde)
            {
                case 1: beeld = "A"; break;
                case 11: beeld = "J"; break;
                case 12: beeld = "Q"; break;
                case 13: beeld = "K"; break;
                default: beeld = waarde.ToString(); break;
            }
            gr.DrawString(beeld, new Font(FontFamily.GenericSansSerif, 14), kwast, new Point(10, 10));
            switch (kleur)
            {
                case 0: beeld = "♥"; break;
                case 1: beeld = "♣"; break;
                case 2: beeld = "♦"; break;
                case 3: beeld = "♠"; break;
                case 4: beeld = "\u1F0CF"; break;
            }
            gr.DrawString(beeld, new Font(FontFamily.GenericSansSerif, 14), kwast, new Point(10, 40));
            return b;
        }

        public Bitmap achter()
        {
            Bitmap b = new Bitmap(90, 135);
            Graphics gr = Graphics.FromImage(b);
            gr.FillRectangle(Brushes.Red, 0, 0, b.Width, b.Height);
            return b;
        }

        public int Kleur //maak of verkrijgt de kleur van een kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        {
            get { return kleur; }
            set
            {
                if (value > -1 && value < 5)
                {
                    kleur = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Kleur niet binnen accepteerbare normen.");
                }
            }
        }

        public int Waarde
        {
            get { return waarde; }
            set
            {
                if (value > 0 && value < 14)
                {
                    waarde = value;
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
                switch (kleur)
                {
                    case 0: a = "Harten ";   break;
                    case 1: a = "Klaver ";   break;
                    case 2: a = "Ruiten ";   break;
                    case 3: a = "Schoppen "; break;
                    case 4: a = "Joker";     break;
                }
                if (kleur != 4)
                {
                    switch (waarde)
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

        public string tekst2 //geeft een tweede geschreven versie van een kaart
        {
            get
            {
                string a = "";
                if (kleur != 4)
                {
                    switch (waarde)
                    {
                        case 1: a = "Aas"; break;
                        case 11: a = "Boer"; break;
                        case 12: a += "Vrouw"; break;
                        case 13: a += "Heer"; break;
                        default: a += "" + waarde; break;
                    }
                }
                switch (kleur)
                {
                    case 0: a = "Harten "; break;
                    case 1: a = "Klaver "; break;
                    case 2: a = "Ruiten "; break;
                    case 3: a = "Schoppen "; break;
                    case 4: a = "Joker"; break;
                }
                return a;
            }
        }

        public int X
        {
            get
            {
                return Location.X;
            }
            set
            {
                Location = new Point(value, Location.X);
            }
        }

        public int Y
        {
            get
            {
                return Location.Y;
            }
            set
            {
                Location = new Point(Location.X, value);
            }
        }

        private void klik(object sender, EventArgs ea)
        {
            MessageBox.Show("Geklikt");
            if (spel.spelend == 0)
            {
                MessageBox.Show("Spelend");
                MessageBox.Show(spel.spelers[0].hand.IndexOf(this) + ".");
                if (spel.speelKaart(spel.spelers[0].hand.IndexOf(this)))
                {
                    MessageBox.Show("Goed");
                    //Invalidate();
                    return;
                }
            }
                    
        }
    }
}
