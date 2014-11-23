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

        public List<Kaart> hand //Op deze manier heeft het toch geen nut? Dit heeft hetzelfde effect als een public variabele 23-11NG
        {
            get { return _hand; }
            set { _hand = value; }
        }

        public string naam;
    }
}
