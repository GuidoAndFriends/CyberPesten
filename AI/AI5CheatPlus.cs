using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class AI5Cheat : Speler
    {
        public AI5Cheat(Spel spel, string naam)
        {
            achterkant = new Bitmap(110, 153, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            string achterkantDesign;
            if (spel.instellingen.achterkant == 0)
            {
                achterkantDesign = "Back_design_1";
            }
            else
            {
                achterkantDesign = "Back_design_2";
            }
            Graphics.FromImage(achterkant).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject(achterkantDesign), 0, 0, 110, 153);

            hand = new List<Kaart>();
            this.spel = spel;
            this.naam = naam;
            blok = new System.Drawing.Bitmap(10, 10);
        }

        public void welkePakken() //pakt 8 en azen
        {
            int acht = 0;
            int aas = 0;
            int tellerAas = 0;
            int tellerAcht = 0;
            foreach (Kaart kaart in hand)
            {
                if (spel.isWacht(kaart))
                    acht++;
                if (spel.isDraai(kaart))
                    aas++;
            }

            foreach (Kaart kaart in spel.pot)
            {
                if (spel.isDraai(kaart))
                    tellerAas++;
                if (spel.isWacht(kaart))
                    tellerAcht++;
            }

            if (tellerAas > 0 && tellerAcht > 0)
            {
                if (aas > acht)
                    spel.pakKaartAI(8);
                else
                    spel.pakKaartAI(1);
            }
            else if (tellerAas > 0 && tellerAcht == 0)
            {
                spel.pakKaartAI(1);
            }
            else if (tellerAas == 0 && tellerAcht > 0)
            {
                spel.pakKaartAI(8);
            }
            else
                spel.pakKaartAI(99);
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
            }
            else
            {
                welkePakken();
                //Hier moet een eigen functie voor pakKaart
            }
            bezig = false;
        }
    }
}
