﻿using System;
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
        public List<int> regelsUitgeschakeld, AIUitgeschakeld;
        public string instellingenPad;

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
            instellingenPad = Path.Combine(CP, "instellingen.cyberpesten");
            if (File.Exists(instellingenPad))
            {
                lezen();
            }
            else
            {
                standaard();
            }
            
        }

        public void lezen()
        {
            List<string> regels = FileToStringList(instellingenPad);

            regelset = Int32.Parse(regels[0]);

            string[] delen = regels[1].Split(new char[] { ',' });
            regelsUitgeschakeld = new List<int>();
            if (delen[0] != "")
            {
                for (int i = 0; i < delen.Length; i++)
                {
                    regelsUitgeschakeld.Add(Int32.Parse(delen[i]));
                }
            }

            aantalSpelers = Int32.Parse(regels[2]);

            delen = regels[3].Split(new char[] {','});
            AIUitgeschakeld = new List<int>();
            if (delen[0] != "")
            {
                for (int i = 0; i < delen.Length; i++)
                {
                    AIUitgeschakeld.Add(Int32.Parse(delen[i]));
                }
            }

            mensSpelend = Boolean.Parse(regels[4]);
        }

        public void schrijven()
        {
            List<string> regels = new List<string>();

            regels.Add(regelset.ToString());

            string regel = "";
            if (regelsUitgeschakeld != null)
            {
                if (regelsUitgeschakeld.Count != 0)
                {
                    regel += regelsUitgeschakeld[0].ToString();
                    for (int i = 1; i < regelsUitgeschakeld.Count; i++)
                    {
                        regel += ',' + regelsUitgeschakeld[i];
                    }
                }
            }
            regels.Add(regel);

            regels.Add(aantalSpelers.ToString());

            regel = "";
            if (AIUitgeschakeld != null)
            {
                if (AIUitgeschakeld.Count != 0)
                {
                    regel += AIUitgeschakeld[0].ToString();
                    for (int i = 1; i < AIUitgeschakeld.Count; i++)
                    {
                        regel += ',' + AIUitgeschakeld[i];
                    }
                }
            }
            regels.Add(regel);

            regels.Add(mensSpelend.ToString());

            StringListToFile(regels, instellingenPad);
        }

        public void standaard()
        {
            regelset = 0;
            regelsUitgeschakeld = null;
            aantalSpelers = 4;
            AIUitgeschakeld = new List<int>();
            AIUitgeschakeld.Add(1);
            AIUitgeschakeld.Add(2);
            mensSpelend = true;
            schrijven();
        }

        public List<string> FileToStringList(string path)//stuurt het hele bestand terug als losse regels, null bij een fout
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
                return lst;
            }
            catch { return null; }
        }

        public void StringListToFile(List<string> regels, string path)
        {
            StreamWriter a = new StreamWriter(path);
            foreach (string regel in regels)
            {
                a.WriteLine(regel);
            }
            a.Close();
        }
    }
}
