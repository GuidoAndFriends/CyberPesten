using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
            achterkant = new Bitmap(110, 153, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            string achterkantDesign;
            if (spel.instellingen.achterkant == 0)
            {
                achterkantDesign = "Back_design_1";
            }
            else if (spel.instellingen.achterkant == 1)
            {
                achterkantDesign = "Back_design_2";
            }
            else
                achterkantDesign = "Back_design_3";
            Graphics.FromImage(achterkant).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject(achterkantDesign), 0, 0, 110, 153);
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
