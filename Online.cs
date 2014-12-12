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
using System.Security.Cryptography;

namespace CyberPesten
{
    class inlogScherm :  Form
    {
        public PictureBox a = new PictureBox();
        Label berichtHouder = new Label();
        TextBox maakAccountTextbox1 = new TextBox();
        TextBox maakAccountTextbox2 = new TextBox();
        Label maakAccountLabel1 = new Label();
        string CP;
        public inlogScherm()
        {
            String bericht;
            string GNF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Guido&Friends");
            if (!Directory.Exists(GNF)) { Directory.CreateDirectory(GNF); }
            CP = Path.Combine(GNF, "Cyperpesten");
            if (!Directory.Exists(CP)) { Directory.CreateDirectory(CP); }
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);//moet nog naar fullscreen
            DoubleBuffered = true;
            string inlogPath = Path.Combine(CP,"inlogData.cyberpesten");
            if(File.Exists(inlogPath)){//misschien een knop om van account te wisselen
                string[] s = FileLines(inlogPath);
                string[] str2 = {"name","unid"};
                string[] str3 = {s[0],s[1]};
                string str1 = PHPrequest("http://harbingerofme.info/GnF/login.php",str2,str3);
                if(str1 == "ja"){
                    bericht = s[0]+", je bent succesvol ingelogd. Welkom terug, vriend!";
                }else{
                    bericht = "Er is iets misgegaan, kijk op onze site voor hulp!";
                }
                               
            }else{//als er nog geen inlog bestand bestaat.
                bericht = "Welkom!";
                a = new PictureBox();
                a.BackColor = Color.Transparent;
                a.Top = 0;
                a.Left = 0;
                a.Width = 4000;
                a.Height = 4000;
                a.Visible = true;

                maakAccountLabel1.Font = new Font("Arial", 15);
                maakAccountLabel1.Top = 85;
                maakAccountLabel1.Left = 30;
                maakAccountLabel1.Text = "Om gebruik te kunnen maken van de online modus, hebben wij je naam nodig.\nJe email is ook handig. Dit adres zal alleen gebruikt worden voor accountmigratie.";
                maakAccountLabel1.Width = 1000;
                maakAccountLabel1.Height = 200;
                maakAccountLabel1.BackColor = Color.Transparent;
                maakAccountLabel1.Parent = a;

                Label maakAccountLabel2 = new Label();
                maakAccountLabel2.Font = new Font("Arial", 12);
                maakAccountLabel2.Top = 150;
                maakAccountLabel2.Left = 30;
                maakAccountLabel2.Text = "Naam (verplicht):";
                maakAccountLabel2.Width = 600;
                maakAccountLabel2.BackColor = Color.Transparent;
                maakAccountLabel2.Height = 200;
                maakAccountLabel2.Parent = a;

                maakAccountTextbox1.AcceptsReturn = false;
                maakAccountTextbox1.AcceptsTab = false;
                maakAccountTextbox1.BackColor = Color.White;
                maakAccountTextbox1.MaxLength = 20;
                maakAccountTextbox1.TextAlign = HorizontalAlignment.Right;
                maakAccountTextbox1.Top = 150;
                maakAccountTextbox1.Left = 160;
                maakAccountTextbox1.Width = 120;
                maakAccountTextbox1.Height = 20;
                maakAccountTextbox1.ShortcutsEnabled = true;//staat de gebruiker toe om te plakken, of om control+a te drukken, etc.
                maakAccountTextbox1.TabIndex = 0;
                maakAccountTextbox1.Parent = a;

                Label maakAccountLabel3 = new Label();
                maakAccountLabel3.Font = new Font("Arial", 12);
                maakAccountLabel3.Top = 180;
                maakAccountLabel3.Left = 30;
                maakAccountLabel3.Text = "Email (optioneel):";
                maakAccountLabel3.Width = 600;
                maakAccountLabel3.BackColor = Color.Transparent;
                maakAccountLabel3.Height = 200;
                maakAccountLabel3.Parent = a;
               

                maakAccountTextbox2 = new TextBox();
                maakAccountTextbox2.AcceptsReturn = false;
                maakAccountTextbox2.AcceptsTab = false;
                maakAccountTextbox2.ShortcutsEnabled = true;
                maakAccountTextbox2.BackColor = Color.White;
                maakAccountTextbox2.TextAlign = HorizontalAlignment.Right;
                maakAccountTextbox2.Top = 180;
                maakAccountTextbox2.Left = 160;
                maakAccountTextbox2.Width = 180;
                maakAccountTextbox2.Height = 20;
                maakAccountTextbox2.TabIndex = 1;
                maakAccountTextbox2.Parent = a;

                Button maakAccountKnop = new Button();
                maakAccountKnop.AutoSize = true;
                maakAccountKnop.Font = new Font("Arial", 20);
                maakAccountKnop.Text = "Maak Account";
                maakAccountKnop.Top = 210;
                maakAccountKnop.Left = 30;
                maakAccountKnop.Parent = a;
                maakAccountKnop.Click += maakAccountKnop_Click;
                //int.Parse("assda");
                //Controls.Add(a);
                Controls.Add(maakAccountKnop);
                Controls.Add(maakAccountTextbox2);
                Controls.Add(maakAccountLabel3);
                Controls.Add(maakAccountTextbox1);
                Controls.Add(maakAccountLabel2);
                Controls.Add(maakAccountLabel1);
                maakAccountTextbox1.Select();
            }
            berichtHouder = new Label();
            berichtHouder.Text = bericht;
            berichtHouder.Left = 30;
            berichtHouder.Top = 30;
            berichtHouder.Width = 1000;
            berichtHouder.Height = 200;
            berichtHouder.BackColor = Color.Transparent;
            berichtHouder.Font = new Font("Arial", 40);
            Controls.Add(berichtHouder);

            this.Show();
        }

        public void maakAccountKnop_Click(object sender, EventArgs e)
        {
            if (maakAccountTextbox1.Text.Length > 3)
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
                string dt = "" + Math.Floor(diff.TotalSeconds);
                MD5 md5hash = MD5.Create();
                string hash = GetMd5Hash(md5hash, maakAccountTextbox1.Text + dt);
                string[] str1 = { "name", "email", "datetime", "unid" };
                string[] str2 = { maakAccountTextbox1.Text, maakAccountTextbox2.Text, dt, hash };
                string ret = PHPrequest("http://harbingerofme.info/GnF/new_user.php", str1, str2);
                if (ret == "ja")
                {
                    string stuff = str2[0] + "\n" + hash + "\n";
                    if (writeFile(Path.Combine(CP, "inlogdata.cyberpesten"), stuff))
                    {
                        a.Visible = false;
                        string str12 = PHPrequest("http://harbingerofme.info/GnF/login.php", str1, str2);//we sturen wat data meer, maar dat maakt niet uit
                        if (str12 == "ja")
                        {
                            maakAccountLabel1.Text = str2[0] + ", je account is aangemaakt, en je bent alvast ingelogd";
                        }
                        else
                        {
                            maakAccountLabel1.Text = str2[0] + ", je account is aangemaakt, maar er is iets mis gegaan bij het inloggen. Contacteer ons gelijk.";//Dit zou niet moeten kunnen gebeuren.
                        }
                    }
                    else//fout in het opslaan van de gegevens
                    {
                        berichtHouder.Text = "Fout in het filesysteem, probeer het opnieuw";
                        PHPrequest("htttp://harbingerofme.info/GnF/delete_user.php", str1, str2);
                    }
                }
                else
                {
                    if (ret.Contains("naam"))
                    {
                        this.maakAccountLabel1.Text = "Er is iets fout gegaan, controleer of je naam geldig is, en probeer het opnieuw.";
                    }
                    if (ret.Contains("database"))
                    {
                        this.maakAccountLabel1.Text = "Er is iets fout gegaan in onze database, probeer het opnieuw.";
                    }
                    if (ret.Contains("gegevens"))
                    {
                        this.maakAccountLabel1.Text = "Er is iets fout gegaan, controleer of je gegevens kloppen";
                    }
                }
            }
        }

        //onderstaande methoden moeten waarschijnlijk naar een hoger niveua verplaatst worden
        public string PHPrequest(string URL, string[] argument_names, string[] argument_values)
        {
            if (argument_names.Count() != argument_values.Count()) { throw new ArgumentException(); }
            WebClient wc = new WebClient();
            NameValueCollection data = new NameValueCollection();
            for (int a = 0; a < argument_values.Count(); a++)
            {
                data[argument_names[a]] = argument_values[a];
            }
            byte[] responseBytes = wc.UploadValues(URL, "POST", data);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            System.Diagnostics.Debug.WriteLine("Request to \"" + URL + "\". Response was: \"" + responsefromserver + "\".");
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
                string[] ret = lst.ToArray();
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

        static string GetMd5Hash(MD5 md5Hash, string input)//Gekopierd van msdn
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

    }
    class Online
    {
        

    }

}
