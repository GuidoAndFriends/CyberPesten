using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class AI0Random : Speler
    {
        public AI0Random(Spel spel, string naam)
        {
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

            hand = new List<Kaart>();
            this.spel = spel;
            this.naam = naam;
            blok = new Bitmap(1, 1);
        }

        public override void doeZet()
        {
            bezig = true;
            List<Kaart> mogelijk = new List<Kaart>();
            foreach (Kaart kaart in hand)
            {
                if (spel.speelbaar(kaart))
                {
                    mogelijk.Add(kaart);
                }
            }
            
            if (mogelijk.Count > 0)
            {
                spel.speelKaart(mogelijk[0]);
                if (hand.Count == 1)
                {
                    gemeld = true;
                } 
            }
            else
            {
                spel.pakKaart();
            }
            bezig = false;
        }
    }
}
