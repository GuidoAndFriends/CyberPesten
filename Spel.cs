using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    class Spel
    {
        public Spel()
        {
            List<Speler> spelers = new List<Speler>();
            //Nog aanpassen aan hoeveelheid mensen en AI
            spelers.Add(new Mens());
            for (int i = 0; i < 5; i++)
            {
                spelers.Add(new Guido());
            }
            System.Diagnostics.Debug.WriteLine("Er zijn  nu " + spelers.Count + " spelers.");
        }
    }
}
