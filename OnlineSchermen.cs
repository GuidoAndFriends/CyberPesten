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
<<<<<<< HEAD:OnlineSchermen.cs
        bool done;
        Thread helper_thread;

        public inlogScherm()
=======
        Bitmap inlogMenu, login, maakAccount, terug;
        Rectangle loginButton, maakAccountButton, terugButton;
        bool loginHover, maakAccountHover, terugHover;
        float verhouding;
        Font arial;
        Form menuBack;

        public inlogScherm(Form _menu)
>>>>>>> origin/master:Online.cs
        {
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);//moet nog naar fullscreen
            DoubleBuffered = true;


            String bericht;
            string GNF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Guido&Friends");
            if (!Directory.Exists(GNF)) { Directory.CreateDirectory(GNF); }
            CP = Path.Combine(GNF, "Cyperpesten");
            if (!Directory.Exists(CP)) { Directory.CreateDirectory(CP); }
<<<<<<< HEAD:OnlineSchermen.cs
=======
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / BackgroundImage.Width;
            arial = new Font("Arial", (int)(15 * verhouding));
            menuBack = _menu;

            inlogMenu = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("online_menu"));
            login = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("login_select"));
            maakAccount = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("maak_account_select"));
            terug = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Terug_button"));

            loginButton = new Rectangle((int)(979 * verhouding), (int)(758 * verhouding), (int)(205 * verhouding), (int)(86 * verhouding));
            maakAccountButton = new Rectangle((int)(722 * verhouding), (int)(758 * verhouding), (int)(205 * verhouding), (int)(86 * verhouding));
            terugButton = new Rectangle(0, (int)(verhouding * 53), (int)(verhouding * 234), (int)(verhouding * 74));

            this.Paint += this.buildMenu;
            this.MouseMove += this.hover;
            this.MouseClick += this.klik;

>>>>>>> origin/master:Online.cs
            string inlogPath = Path.Combine(CP,"inlogData.cyberpesten");
            if(File.Exists(inlogPath)){//misschien een knop om van account te wisselen
                string[] s = Online.FileLines(inlogPath);
                string[] str2 = {"name","unid"};
                string[] str3 = {s[0],s[1]};
                string str1 = Online.PHPrequest("http://harbingerofme.info/GnF/login.php",str2,str3);
                if(str1 == "ja"){
                    bericht = s[0]+", je bent succesvol ingelogd. Welkom terug, vriend!";
                    Online.username = s[0];
                    Online.token = s[1];
                    gaVerder();
                }else{
                    bericht = "Er is iets misgegaan, kijk op onze site voor hulp!";
                }
                               
            }else{//als er nog geen inlog bestand bestaat.
                /*bericht = "Welkom!";
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
                maakAccountLabel2.Parent = a;*/

                maakAccountTextbox1.AcceptsReturn = false;
                maakAccountTextbox1.AcceptsTab = false;
                maakAccountTextbox1.BackColor = Color.White;
                maakAccountTextbox1.MaxLength = 20;
                maakAccountTextbox1.TextAlign = HorizontalAlignment.Left;
                maakAccountTextbox1.Top = (int)(485 * verhouding);
                maakAccountTextbox1.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - (int)(80 * verhouding);
                maakAccountTextbox1.Width = (int)(320 * verhouding);
                maakAccountTextbox1.ShortcutsEnabled = true;//staat de gebruiker toe om te plakken, of om control+a te drukken, etc.
                maakAccountTextbox1.TabIndex = 0;
                maakAccountTextbox1.Parent = a;
                maakAccountTextbox1.Font = arial;

                /*Label maakAccountLabel3 = new Label();
                maakAccountLabel3.Font = new Font("Arial", 12);
                maakAccountLabel3.Top = 180;
                maakAccountLabel3.Left = 30;
                maakAccountLabel3.Text = "Email (optioneel):";
                maakAccountLabel3.Width = 600;
                maakAccountLabel3.BackColor = Color.Transparent;
                maakAccountLabel3.Height = 200;
                maakAccountLabel3.Parent = a;*/
               

                maakAccountTextbox2 = new TextBox();
                maakAccountTextbox2.AcceptsReturn = false;
                maakAccountTextbox2.AcceptsTab = false;
                maakAccountTextbox2.ShortcutsEnabled = true;
                maakAccountTextbox2.BackColor = Color.White;
                maakAccountTextbox2.TextAlign = HorizontalAlignment.Left;
                maakAccountTextbox2.Top = (int)(570 * verhouding);
                maakAccountTextbox2.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - (int)(80 * verhouding);
                maakAccountTextbox2.Width = (int)(320 * verhouding);
                maakAccountTextbox2.TabIndex = 1;
                maakAccountTextbox2.Parent = a;
                maakAccountTextbox2.Font = arial;

                /*Button maakAccountKnop = new Button();
                maakAccountKnop.AutoSize = true;
                maakAccountKnop.Font = new Font("Arial", 20);
                maakAccountKnop.Text = "Maak Account";
                maakAccountKnop.Top = 210;
                maakAccountKnop.Left = 30;
                maakAccountKnop.Parent = a;
                maakAccountKnop.Click += maakAccountKnop_Click;*/
                //int.Parse("assda");
                //Controls.Add(a);
                //Controls.Add(maakAccountKnop);
                Controls.Add(maakAccountTextbox2);
                //Controls.Add(maakAccountLabel3);
                Controls.Add(maakAccountTextbox1);
                //Controls.Add(maakAccountLabel2);
                //Controls.Add(maakAccountLabel1);
                maakAccountTextbox1.Select();
            }
            /*berichtHouder = new Label();
            berichtHouder.Text = bericht;
            berichtHouder.Left = 30;
            berichtHouder.Top = 30;
            berichtHouder.Width = 1000;
            berichtHouder.Height = 200;
            berichtHouder.BackColor = Color.Transparent;
            berichtHouder.Font = new Font("Arial", 40);
            Controls.Add(berichtHouder);*/

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
                string hash = Online.GetMd5Hash(md5hash, maakAccountTextbox1.Text + dt);
                string[] str1 = { "name", "email", "datetime", "unid" };
                string[] str2 = { maakAccountTextbox1.Text, maakAccountTextbox2.Text, dt, hash };
                string ret = Online.PHPrequest("http://harbingerofme.info/GnF/new_user.php", str1, str2);
                if (ret == "ja")
                {
                    string stuff = str2[0] + "\n" + hash + "\n";
                    if (Online.writeFile(Path.Combine(CP, "inlogdata.cyberpesten"), stuff))
                    {
                        a.Visible = false;
                        string str12 = Online.PHPrequest("http://harbingerofme.info/GnF/login.php", str1, str2);//we sturen wat data meer, maar dat maakt niet uit
                        if (str12 == "ja")
                        {
                            maakAccountLabel1.Text = str2[0] + ", je account is aangemaakt, en je bent alvast ingelogd";
                            Online.username = str2[0];
                            Online.token = str2[3];
                            gaVerder();
                        }
                        else
                        {
                            maakAccountLabel1.Text = str2[0] + ", je account is aangemaakt, maar er is iets mis gegaan bij het inloggen. Contacteer ons gelijk.";//Dit zou niet moeten kunnen gebeuren.
                        }
                    }
                    else//fout in het opslaan van de gegevens
                    {
                        berichtHouder.Text = "Fout in het filesysteem, probeer het opnieuw";
                        Online.PHPrequest("htttp://harbingerofme.info/GnF/delete_user.php", str1, str2);
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
        public void gaVerder(){
          /*  if (!done)
            {
                helper_thread = new Thread(wachtEven);
            }
            else
            {
                this.Close();
                helper_thread.Abort();
            }*/
        }
        public void wachtEven(){
            Thread.Sleep(500);
            done = true;
            gaVerder();
            
        }

    }

    class lobbyScherm : Form
    {
        public lobbyScherm()
        {

        }
    }


    class openSpellenScherm : Form
    {

        public openSpellenScherm()
        {
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);//moet nog naar fullscreen
            DoubleBuffered = true;
            this.Show();


            Online.username = "Guido";
            Online.token = "9e6a2c7a42c27b5852d709f162f21332";
            online_openSpel[] list = krijgSpellen();
            if(list.Count()>0){
                foreach (online_openSpel oo in list) { 
                //laat het zien
                
                }
            }
            else
            {
                //laat zien: Geen open spellen gevonden.
            }
        }



        public bool join_spel(int spelid)
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/join_game.php", new string[] { "name", "token","spelid" }, new string[] { Online.username, Online.token,spelid.ToString() });
            return raw == "ja";//kweenie volgens mij moet er iets meer gebeuren
        }




        public online_openSpel[] krijgSpellen()
        {
            List<online_openSpel> returnal = new List<online_openSpel>();
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/get_games.php", new string[] { "name", "token" }, new string[] { Online.username, Online.token });
            if (!raw.StartsWith("Error:"))
            {
                string copy = raw.Substring(1, raw.Length - 2);//strip begin en einde
                while(copy.Length>0)
                {
                    string substr = copy.Substring(1, copy.IndexOf('}') - 1);//misschien hoeft die min 1 niet? Testen nodig.
                    string[] splits = substr.Split('|');
                    string[] temp = splits[3].Split(',');string temp2 = "";
                    for(int i = 1; i<temp.Count();i++){
                        temp2 += temp[i]+" ";
                    }
                    temp2 = temp2.Trim();
                    returnal.Add(new online_openSpel(int.Parse(splits[0]),"Missend",temp.Count(),int.Parse(splits[2]),splits[5],splits[4].Equals("2"),temp[0],temp2));

                }
            }
            return returnal.ToArray();
        }
    }

    public class online_openSpel//een online spel
    {
        public string spelnaam;
        public int spelerAantal;
        public int maxSpelerAantal;
        public string Spelregel;//kan ook als int, met 0 standaard, 1 voor familie regels, 2 voor iets anders, 3 voor custom ofzo.
        public bool begonnen;
        public string tags;//zodat er gezocht kan worden, er is nu nog geen definitie voor.
        public string host;//voor het archief
        public string spelernamen;//de spelernamen, gescheiden met een spatie
        public int id;//voor het archief

        //wachtwoord?


        public online_openSpel(string spelname, int spelers, int maxSpelers,string regels, bool gestart)//goed genoeg voor testen
        {
            spelnaam = spelname;
            spelerAantal = spelers;
            maxSpelerAantal = maxSpelers;
            Spelregel = regels;
            begonnen = gestart;
            host = "Guido";
            tags = "naam:"+spelnaam + "regels:"+regels + spelernamen + "host:"+host;
            
        }

        public online_openSpel(int ID, string spelname, int spelers, int maxSpelers,string regels, bool gestart, string hoster, string spelernames)//wat we echt gaan gebruiken
        {
            spelnaam = spelname;
            spelerAantal = spelers;
            maxSpelerAantal = maxSpelers;
            Spelregel = regels;
            begonnen = gestart;
            id = ID;
            host = hoster;
            spelernamen = spelernames;
            tags = "naam:" + spelnaam.ToLower() + "regels:" + regels.ToLower() + spelernamen.ToLower() + "host:" + host.ToLower() + "id:"+id;
        }
    }
    
    
    class Online//bevat al onze hulp methoden
    {
        public static string username;
        public static string token;
        public static int game;


        //onderstaande methoden moeten waarschijnlijk naar een hoger niveua verplaatst worden
        public static string PHPrequest(string URL, string[] argument_names, string[] argument_values)
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

        public static string FileReadAll(string path)//stuurt het hele bestand terug als tekst, geeft een null terug als er een fout optreed (bestand bestaat niet, geen toegang, etc.) - uiteraard is het makkelijker om dit zelf aan te roepen, maar ja
        {
            try { return File.ReadAllText(path); }
            catch { return null; }
        }

        public static string[] FileLines(string path)//stuurt het hele bestand terug als losse regels, null bij een fout
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

        public static bool writeFile(string path, string stuff)//schrijft naar een bestand, mocht dit bestand niet bestaan, dan wordt het gemaakt. Geeft true als gelukt, false als mislukt om wat voor reden dan ook
        {
            try
            {
                File.WriteAllText(path, stuff);

                return true;
            }
            catch { return false; }
        }

        public static bool appendFile(string path, string stuff)//schrijft aan het einde van een bestand, pas op met regel eindes.
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

        public static string GetMd5Hash(MD5 md5Hash, string input)//Gekopierd van msdn
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

        private void buildMenu(object sender, PaintEventArgs pea)
        {
            pea.Graphics.DrawImage(inlogMenu, 0, 0, (int)inlogMenu.Width * verhouding, (int)inlogMenu.Height * verhouding);

            if (loginHover)
            {
                pea.Graphics.DrawImage(login, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            }
            if (maakAccountHover)
            {
                pea.Graphics.DrawImage(maakAccount, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            }
            if (terugHover)
            {
                pea.Graphics.DrawImage(terug, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            }
        }

        public void hover(object sender, MouseEventArgs mea)
        {
            if (loginButton.Contains(mea.Location))
            {
                loginHover = true;
            }
            else
            {
                loginHover = false;
            }

            if (maakAccountButton.Contains(mea.Location))
            {
                maakAccountHover = true;
            }
            else
            {
                maakAccountHover = false;
            }

            if (terugButton.Contains(mea.Location))
            {
                terugHover = true;
            }
            else
            {
                terugHover = false;
            }

            Invalidate();
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (loginHover)
            {
                //Login
            }
            else if (maakAccountHover)
            {
                //Maak account
            }
            else if (terugHover)
            {
                menuBack.Show();
                this.Close();
            }
        }

    }

}
