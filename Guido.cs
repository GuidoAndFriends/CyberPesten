using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace CyberPesten
{
    class Guido : AI
    {
        //voorbeeld AI
        public Guido(Spel s)
        {
            hand = new List<Kaart>();
            spel = s;
            int hash = GetHashCode();
            naam = "Guido" + hash;
            System.Diagnostics.Debug.WriteLine(hash.ToString());
        }

        public override void maakXY() { }

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
