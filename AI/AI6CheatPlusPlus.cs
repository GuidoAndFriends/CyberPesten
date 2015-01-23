using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class AI6Cheat : Speler
    {
        public AI6Cheat(Spel spel, string naam)
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
            blok = new System.Drawing.Bitmap(10, 10);
        }

        public void welkePakken() //7 en heren, mits joker, of boer als > 50% van de kaarten zelfde soort is
        {
            int harten = 0; //0
            int ruiten = 0; //2
            int schoppen = 0; //3
            int klaver = 0; //1
            bool joker = false, twee = false, normaal = false;

            foreach (Kaart kaart in hand)
            {
                if (kaart.Kleur == 4 || kaart.Waarde == 2)
                    joker = true;

                if (kaart.Waarde == 3 || kaart.Waarde == 4 || kaart.Waarde == 5 || kaart.Waarde == 6 || kaart.Waarde == 9 || kaart.Waarde == 12)
                    normaal = true;

                if (kaart.Kleur == 0)
                    harten++;
                else if (kaart.Kleur == 1)
                    klaver++;
                else if (kaart.Kleur == 2)
                    ruiten++;
                else if (kaart.Kleur == 3)
                    schoppen++;
            }

            if (normaal)
            {
                if (!joker)
                {
                    foreach (Kaart kaart in spel.pot)
                    {
                        if (kaart.Kleur == 4)
                        {
                            twee = false;
                            break;
                        }
                        else if (kaart.Waarde == 2)
                        {
                            twee = true;
                            break;
                        }
                    }
                    if (!twee)
                    {
                        spel.pakKaartAI(0);
                    }
                    else
                        spel.pakKaartAI(2);
                }
                else if (hand.Count > 5)
                    if (harten > hand.Count / 2 || klaver > hand.Count / 2 || ruiten > hand.Count / 2 || schoppen > hand.Count / 2)
                    {
                        spel.pakKaartAI(11);
                    }
                else
                    spel.pakKaart(7);
            }
            else
                spel.pakKaartAI(3);
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
