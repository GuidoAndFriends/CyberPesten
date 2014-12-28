using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CyberPesten
{
    class Instellingen
    {
        public int regelset, aantalSpelers;
        public bool mensSpelend;
        public List<int> regelsUitgeschakeld;

        public Instellingen()
        {
            string GNF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Guido&Friends");
            if (!Directory.Exists(GNF))
            {
                Directory.CreateDirectory(GNF);
            }
            string CP = Path.Combine(GNF, "Cyperpesten");
            if (!Directory.Exists(CP))
            {
                Directory.CreateDirectory(CP);
            }
            string instellingenPad = Path.Combine(CP, "instellingen.cyberpesten");
            if (File.Exists(instellingenPad))
            {
                lezen(instellingenPad);
            }
            else
            {
                standaard();
                
            }
            
        }

        public void lezen(string pad)
        {
            string[] regels = FileLines(pad);
            regelset = Int32.Parse(regels[0]);
            int aantal = Int32.Parse(regels[1]);
            if (aantal > 0)
            {
                regelsUitgeschakeld = new List<int>();
                for (int i = 0; i < aantal; i++)
                {
                    regelsUitgeschakeld.Add(Int32.Parse(regels[aantal + 2]));
                }
            }
            aantalSpelers = Int32.Parse(regels[2 + aantal]);
            mensSpelend = Boolean.Parse(regels[3 + aantal]);
        }

        public void schrijven()
        {

        }

        public void standaard()
        {
            regelset = 0;
            regelsUitgeschakeld = null;
            aantalSpelers = 4;
            mensSpelend = true;
            schrijven();
        }

        public string[] FileLines(string path)//stuurt het hele bestand terug als losse regels, null bij een fout
        {
            try
            {
                StreamReader a = File.OpenText(path);
                List<string> lst = new List<string>();
                string str;
                while ((str = a.ReadLine()) != null)
                {
                    lst.Add(str);
                }
                a.Close();
                string[] ret = lst.ToArray();
                return ret;
            }
            catch { return null; }
        }
    }
}
