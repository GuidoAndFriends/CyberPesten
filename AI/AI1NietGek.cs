using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class AI1NietGek : Speler
    {
        public AI1NietGek(Spel spel, string naam)
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
                int index = 0;
                while (mogelijk[index].Kleur == 7 || mogelijk[index].Kleur == 13)
                {
                    if (straksGeenMogelijkeZet(mogelijk[index]) && index < mogelijk.Count - 1)
                    {
                        index++;
                    }
                }
                spel.speelKaart(mogelijk[index]);
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

        bool straksGeenMogelijkeZet(Kaart gepland)
        {
            List<Kaart> mogelijk = new List<Kaart>();
            foreach (Kaart kaart in hand)
            {
                if (spel.speelbaar(kaart))
                {
                    if (kaart.Kleur == gepland.Kleur ||
                        kaart.Waarde == gepland.Waarde ||
                        kaart.Kleur == 4 || //joker
                        kaart.Waarde == 11) //boer
                    {
                        mogelijk.Add(kaart);
                    }
                }
            }
            mogelijk.Remove(gepland);
            if (mogelijk.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
