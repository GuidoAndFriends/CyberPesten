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
        public AI1NietGek(Spel _spel, string _naam)
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
                spel.volgende();
            }
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
