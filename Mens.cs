using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Mens : Speler
    {
        public Mens(Spel s)
        {
            hand = new List<Kaart>();
            naam = "Speler";
            spel = s;
        }

        public override void updateBlok()
        {
            int breedte = hand.Count * (110 + 10) - 10;
            int basis = spel.speelveld.Width / 2 - breedte / 2;
            int index = 0;
            int y = spel.speelveld.Height - 153 - 10;
            foreach (Kaart kaart in hand)
            {
                kaart.X = basis + index * (110 + 10);
                kaart.Y = y;
                index++;
            }
        }

        public override void doeZet() { }

        public override void kiesKleurEnVolgende()
        {
            KleurKiezen kleur = new KleurKiezen(spel);
        }
    }
}
