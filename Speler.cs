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
        private List<Kaart> _hand;

        public List<Kaart> hand //Op deze manier heeft het toch geen nut? Dit heeft hetzelfde effect als een public variabele 23-11NG
        {
            get { return _hand; }
            set { _hand = value; }
        }

        public string naam;

        public abstract void maakXY();

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
