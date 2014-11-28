using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Guido : AI
    {
        //voorbeeld AI
        public Guido()
        {
            hand = new List<Kaart>();
            int hash = GetHashCode();
            naam = "Guido" + hash;
            System.Diagnostics.Debug.WriteLine(hash.ToString());
        }

        public override void maakXY() { }
    }
}
