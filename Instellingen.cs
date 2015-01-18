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
        public int regelset, aantalSpelers, achterkant;
        public bool mensSpelend, muziek;
        public List<int> regelsIngeschakeld, AIIngeschakeld;
        string instellingenPad;

        public Instellingen()
        {
            //Er wordt gecontroleerd of de benodigde mappen en het instellingenbestand bestaan en eventueel worden ze aangemaakt
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
            System.Diagnostics.Debug.WriteLine("De instellingen zijn: " + regelset + " - " + regelsIngeschakeld.ToString() + " - " + aantalSpelers + " - " + AIIngeschakeld + " - " + mensSpelend);
        }

        public void lezen()
        {
            try
            {
                List<string> regels = FileToStringList(instellingenPad);

                //0 regelset
                regelset = Int32.Parse(regels[0]);

                //1 regels ingeschakeld
                string[] delen = regels[1].Split(new char[] { ',' });
                regelsIngeschakeld = new List<int>();
                if (delen[0] != "")
                {
                    for (int i = 0; i < delen.Length; i++)
                    {
                        regelsIngeschakeld.Add(Int32.Parse(delen[i]));
                    }
                }

                //2 aantal spelers
                aantalSpelers = Int32.Parse(regels[2]);

                //3 AI ingeschakeld
                delen = regels[3].Split(new char[] { ',' });
                AIIngeschakeld = new List<int>();
                if (delen[0] != "")
                {
                    for (int i = 0; i < delen.Length; i++)
                    {
                        AIIngeschakeld.Add(Int32.Parse(delen[i]));
                    }
                }

                //4 mens speelt mee
                mensSpelend = Boolean.Parse(regels[4]);

                //5 muziek
                muziek = Boolean.Parse(regels[5]);

                //6 achterkant
                achterkant = Int32.Parse(regels[6]);

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Er is iets mis met het instellingenbestand. De instellingen zullen nu gereset worden.");
                standaard();
            }
            
        }

        public void schrijven()
        {
            List<string> regels = new List<string>();

            //0 regelset
            regels.Add(regelset.ToString());

            //1 regels ingeschakeld
            string regel = "";
            if (regelsIngeschakeld != null)
            {
                if (regelsIngeschakeld.Count != 0)
                {
                    regel += regelsIngeschakeld[0].ToString();
                    for (int i = 1; i < regelsIngeschakeld.Count; i++)
                    {
                        regel += "," + regelsIngeschakeld[i].ToString();
                    }
                }
            }
            regels.Add(regel);

            //2 aantal spelers
            regels.Add(aantalSpelers.ToString());

            //3 AI ingeschakeld
            regel = "";
            if (AIIngeschakeld != null)
            {
                if (AIIngeschakeld.Count != 0)
                {
                    regel += AIIngeschakeld[0].ToString();
                    for (int i = 1; i < AIIngeschakeld.Count; i++)
                    {
                        regel += "," + AIIngeschakeld[i].ToString();
                    }
                }
            }
            regels.Add(regel);

            //4 mens speelt mee
            regels.Add(mensSpelend.ToString());

            //5 muziek
            regels.Add(muziek.ToString());

            //6 achterkant
            regels.Add(achterkant.ToString());

            StringListToFile(regels, instellingenPad);
        }

        public void standaard()
        {
            regelset = 0;

            regelsIngeschakeld = new List<int>();
            regelsIngeschakeld.Add(0);
            regelsIngeschakeld.Add(1);
            regelsIngeschakeld.Add(2);
            regelsIngeschakeld.Add(3);
            regelsIngeschakeld.Add(4);
            regelsIngeschakeld.Add(5);
            regelsIngeschakeld.Add(6);

            aantalSpelers = 4;

            AIIngeschakeld = new List<int>();
            AIIngeschakeld.Add(2);
            AIIngeschakeld.Add(3);

            mensSpelend = true;

            muziek = true;

            achterkant = 1;

            schrijven();
        }

        List<string> FileToStringList(string path) //stuurt het hele bestand terug als losse regels, null bij een fout
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

        void StringListToFile(List<string> regels, string path) //maakt van de list met regels een tekstbestand in het gegeven pad
        {
            //maakt het bestand leeg
            System.IO.File.WriteAllText(path, string.Empty);

            StreamWriter a = new StreamWriter(path);
            foreach (string regel in regels)
            {
                a.WriteLine(regel);
            }
            a.Close();
        }
    }
}
