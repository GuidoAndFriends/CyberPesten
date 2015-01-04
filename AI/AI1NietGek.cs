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

        public override void kiesKleurEnVolgende()
        {
            int[] kleuren = { 0, 0, 0, 0 };
            foreach (Kaart kaart in hand)
            {
                if (kaart.Kleur != 4) //joker
                {
                    kleuren[kaart.Kleur]++;
                }
                
            }

            int kleur = 0;
            for (int i = 1; i < 4; i++)
            {
                if (kleuren[i] > kleuren[kleur])
                {
                    kleur = i;
                }
            }
           
            spel.speciaal = kleur;
            spel.status += " en koos voor ";
            switch (kleur)
            {
                case 0: spel.status += "Harten "; break;
                case 1: spel.status += "Klaver "; break;
                case 2: spel.status += "Ruiten "; break;
                case 3: spel.status += "Schoppen "; break;
            }
            spel.volgende();
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
