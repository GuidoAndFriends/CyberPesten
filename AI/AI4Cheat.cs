using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class AI4Cheat : Speler
    {
        public AI4Cheat(Spel s, string n)
        {
            achterkant = new Bitmap((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Back_design_2"), 110, 153);
            hand = new List<Kaart>();
            spel = s;
            naam = n;
            blok = new System.Drawing.Bitmap(10, 10);
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
            }
            else
            {
                spel.pakKaart();
                //Hier moet een eigen functie voor pakKaart
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
            spel.status += " en koos voor " + kleur;
            spel.volgende();
        }
    }
}
