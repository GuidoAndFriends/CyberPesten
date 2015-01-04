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
        public AI0Random(Spel _spel, string _naam)
        {
            achterkant = new Bitmap((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Back_design_2"), 110, 153);
            hand = new List<Kaart>();
            spel = _spel;
            naam = _naam;
            blok = new Bitmap(1, 1);
        }

        public override void doeZet()
        {
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
                spel.volgende();
            }
        }
    }
}
