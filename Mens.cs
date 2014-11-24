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
            int breedte = hand.Count * (100 + 10) - 10;
            int basis = 1000 / 2 - breedte / 2;
            int index = 0;
            foreach (Kaart kaart in hand)
            {
                kaart.X = basis + index * 110;
                kaart.Y = 500;
                index++;
            }
        }
    }
}
