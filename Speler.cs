using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    abstract class Speler
    {
        private List<Kaart> _hand;

        public List<Kaart> hand
        {
            get { return _hand; }
            set { _hand = value; }
        }
    }
}
