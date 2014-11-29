using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Spel
    {
        public List<Kaart> pot;
        public List<Speler> spelers;
        public List<Kaart> stapel;
        public Speelveld speelveld;
        public bool bezig;
        public int spelend;
        public int richting;

        //public Spel(Speelveld s, int aantalSpelers)
        public Spel()
        {

        }
    }
}
