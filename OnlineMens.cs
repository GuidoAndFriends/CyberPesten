using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class OnlineMens : Speler
    {
        public int OnlineIndex;
        public OnlineMens(Spel s, int index)
        {
            hand = new List<Kaart>();
            naam = Online.username;
            OnlineIndex = index;
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

        public override void kiesKleur()
        {
            spel.speelveld.toonKleurknoppen();
        }
    }
}
