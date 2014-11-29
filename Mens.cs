using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Mens : Speler
    {
        public Mens()
        {
            hand = new List<Kaart>();
            naam = "Speler";
        }

        public override void maakXY()
        {
            int breedte = hand.Count * (90 + 10) - 10;
            int basis = 1000 / 2 - breedte / 2;
            int index = 0;
            foreach (Kaart kaart in hand)
            {
                kaart.X = basis + index * (90 + 10);
                kaart.Y = 500;
                index++;
            }
        }

        public override void doeZet() { }
    }
}
