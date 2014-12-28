using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace CyberPesten
{
    class AI2Pester : Speler
    {
        public List<Speler> spelers;
        public int spelend, richting;
        public bool mens;


        public AI2Pester(Spel s, string n)
        {
            hand = new List<Kaart>();
            spel = s;
            int hash = GetHashCode();
            naam = n;
            System.Diagnostics.Debug.WriteLine(hash.ToString());
            blok = new System.Drawing.Bitmap(10, 10);
        }

        List<Kaart> mogelijk = new List<Kaart>();
        List<Kaart> pester = new List<Kaart>();
        List<Kaart> bonus = new List<Kaart>();

        public override void doeZet()
        {
            
            foreach (Kaart kaart in hand)
            {
                if (spel.speelbaar(kaart))
                {
                    mogelijk.Add(kaart);
                }
            }

            foreach (Kaart kaart in mogelijk)
            {
                if (kaart.Waarde == 2 || kaart.Waarde == 4 || kaart.Waarde == 8 || kaart.Waarde == 1)
                {
                    pester.Add(kaart);
                }
            }

            foreach (Kaart kaart in mogelijk)
            {
                if (kaart.Waarde == 7 || kaart.Waarde == 13)
                {
                    bonus.Add(kaart);
                }
            }


            if (mogelijk.Count > 0)
            {
                if (pester.Count > 0)
                {
                    spel.speelKaart(pester[0]);
                }
                else
                {
                    if (bonus.Count > 0)
                    {
                        spel.speelKaart(bonus[0]);
                    }
                    
                    else
                    {
                        spel.speelKaart(mogelijk[0]);
                    }                    
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

        