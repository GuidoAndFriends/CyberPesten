using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Specialized;
using System.Net;
using System.IO;

namespace CyberPesten
{
    class inlogScherm :  Form
    {
        public inlogScherm()
        { 
            string GNF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Guido&Friends");
            if (!Directory.Exists(GNF)) { Directory.CreateDirectory(GNF); }
            string CP = Path.Combine(GNF, "Cyperpesten");
            if (!Directory.Exists(CP)) { Directory.CreateDirectory(CP); }


            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);//Ja we gebruiken nog een kleine versie omdat redenen, en ik ben moe -Guido
            DoubleBuffered = true;
            string inlogPath = Path.Combine(CP,"inlogData.cyberpesten";
            if(File.Exists(inlogPath)){
                string[] s = FileLines(inlogPath);
                String bericht = "Welkom terug, "+ s[0];
                                //stiekem moet ik nog calls maken om in te loggen
            }else{

            }

            this.Show();
        }
        //onderstaande methoden moeten waarschijnlijk naar een hoger niveua verplaatst worden
        public string PHPrequest(string URL, string[] argument_names, string[] argument_values)
        {
            if (argument_names.Count() != argument_values.Count()) { throw new ArgumentException(); }
            WebClient wc = new WebClient();
            NameValueCollection data = new NameValueCollection();
            for (int a = 0; a < argument_values.Count(); a++)
            {
                data[argument_names[a]] = argument_values[0];
            }
            byte[] responseBytes = wc.UploadValues(URL, "POST", data);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            wc.Dispose();
            return responsefromserver;
        }

        public string FileReadAll(string path)//stuurt het hele bestand terug als tekst, geeft een null terug als er een fout optreed (bestand bestaat niet, geen toegang, etc.) - uiteraard is het makkelijker om dit zelf aan te roepen, maar ja
        {
            try { return File.ReadAllText(path); }
            catch { return null; }
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
                string[] ret = new string[lst.Count]; int b = 0;
                foreach (string str2 in lst)
                {
                    ret[b] = str2;
                    b++;
                }
                return ret;
            }
            catch { return null; }
        }

        public bool writeFile(string path, string stuff)//schrijft naar een bestand, mocht dit bestand niet bestaan, dan wordt het gemaakt. Geeft true als gelukt, false als mislukt om wat voor reden dan ook
        {
            try
            {
                File.WriteAllText(path, stuff);

                return true;
            }
            catch { return false; }
        }

        public bool appendFile(string path, string stuff)//schrijft aan het einde van een bestand, pas op met regel eindes.
        {
            try
            {
                File.AppendAllText(path, stuff);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
    class Online
    {
        

    }

}
