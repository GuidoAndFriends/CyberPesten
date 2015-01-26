using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class OnlineSpeler : Speler
    {
        public int OnlineIndex;
        public OnlineSpeler(Spel s, string Name, int index)
        {
            naam = Name;
            OnlineIndex = index;
            spel = s;
            hand = new List<Kaart>();
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

        public override void doeZet()
        {
            bezig = true;
            System.Windows.Forms.MessageBox.Show("doeZet in OnlineSpeler.cs is nog leeg!");//tja het probleem is dat dit asynchroon gaat.
            //ik heb wel een idee.

        }
    }
}
