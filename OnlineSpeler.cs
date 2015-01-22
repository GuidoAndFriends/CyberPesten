using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    class OnlineSpeler : Speler
    {
        public int OnlineIndex;
        public OnlineSpeler(Spel s, string Name, int index)
        {
            naam = Name;
            OnlineIndex = index;
            spel = s;
            hand = new List<Kaart>();
        }

        public override void doeZet()
        {
            bezig = true;
            System.Windows.Forms.MessageBox.Show("doeZet in OnlineSpeler.cs is nog leeg!");//tja het probleem is dat dit asynchroon gaat.
            //ik heb wel een idee.

        }
    }
}
