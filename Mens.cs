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
            int index = 1;
            foreach (Kaart kaart in hand)
            {
                kaart.X = index * 100;
                kaart.Y = 500;
                index++;
            }
        }
    }
}
