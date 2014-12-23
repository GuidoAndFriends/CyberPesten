using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace CyberPesten
{
    class AI1Random : Speler
    { //soort van nodig
        //voorbeeld AI
        public AI1Random(Spel s, string n)
        {
            hand = new List<Kaart>();
            spel = s;
            int hash = GetHashCode();
            naam = n;
            System.Diagnostics.Debug.WriteLine(hash.ToString());
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
                spel.volgende();
            }
        }
    }
}
