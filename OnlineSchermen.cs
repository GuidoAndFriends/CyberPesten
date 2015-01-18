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

        Bitmap inlogMenu, login, maakAccount, terug;
        Rectangle loginButton, maakAccountButton, terugButton;
        bool loginHover, maakAccountHover, terugHover;
        float verhouding;
        Font arial;
        Form menuBack;
            

        public inlogScherm(Form _menu)
        {
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / BackgroundImage.Width;
            arial = new Font("Arial", (int)(15 * verhouding));
            menuBack = _menu;

            String bericht;
            string GNF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Guido&Friends");
            if (!Directory.Exists(GNF)) { Directory.CreateDirectory(GNF); }
            CP = Path.Combine(GNF, "Cyperpesten");
            if (!Directory.Exists(CP)) { Directory.CreateDirectory(CP); }
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

            Controls.Add(maakAccountTextbox2);
            Controls.Add(maakAccountTextbox1);


            maakAccountTextbox1.Select();
            berichtHouder = new Label();
            berichtHouder.Text = "";
            berichtHouder.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - (int)(300 * verhouding);
            berichtHouder.Top = (int)(400*verhouding);
            berichtHouder.TextAlign = ContentAlignment.TopCenter;
            berichtHouder.Width = (int)(600*verhouding);
            berichtHouder.Height = (int)(80*verhouding);
            berichtHouder.BackColor = Color.Transparent;
            berichtHouder.ForeColor = Color.Red;
            berichtHouder.Font = new Font("Arial", (int)(20*verhouding));
            Controls.Add(berichtHouder);

            this.Show();


            string inlogPath = Path.Combine(CP, "inlogData.cyberpesten");
            if (File.Exists(inlogPath))
            {
                string[] s = Online.FileLines(inlogPath);
                Online.username = s[0];
                Online.token = s[1];
                gaVerder();

            }

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
                maakAccountKnop_Click();
            }
            else if (terugHover)
            {
                menuBack.Show();
                this.Close();
            }
        }


        public void maakAccountKnop_Click()
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
                            berichtHouder.Text = str2[0] + ", je account is aangemaakt, en je bent alvast ingelogd";
                            Online.username = str2[0];
                            Online.token = str2[3];
                            gaVerder();
                        }
                        else
                        {
                            berichtHouder.Text = str2[0] + ", je account is aangemaakt, maar er is iets mis gegaan bij het inloggen. Contacteer ons gelijk.";//Dit zou niet moeten kunnen gebeuren.
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
                        this.berichtHouder.Text = "Er is iets fout gegaan, controleer of je naam geldig is, en probeer het opnieuw.";
                    }
                    if (ret.Contains("database"))
                    {
                        this.berichtHouder.Text = "Er is iets fout gegaan in onze database, probeer het opnieuw.";
                    }
                    if (ret.Contains("gegevens"))
                    {
                        this.berichtHouder.Text = "Er is iets fout gegaan, controleer of je gegevens kloppen.";
                    }
                }
            }
        }
        public void gaVerder(){
            Form Veld = new openSpellenScherm(menuBack);
            Veld.FormClosed += Veld_FormClosed;
            this.Hide();
            //deze functie zou het programma (misschien via een splashscreen) naar het openspellenScherm moeten leiden
        }

        public void Veld_FormClosed(object o, EventArgs e){
            Close();
        }

    }

    class lobbyScherm : Form
    {
        float verhouding;
        Font arial;


        public string[] deelnemers;
        public string[] rankings;//nog niet zo relevant
        public bool begonnen = false;
        Thread data_thread;

        Form menuBack;
        public lobbyScherm(Form back)
        {
            menuBack = back;
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / BackgroundImage.Width;
            arial = new Font("Arial", (int)(15 * verhouding));
            this.Show();
            data_thread = new Thread(dataThread);
            data_thread.Start();
            //volgende dingen zijn nodig: 
            //gedeelte met huidige deelnemers (en hun ranking)
            //een plek om de chat te dumpen
            //een knop voor de host om de game te starten (start_spel)
            //een knop om de game te leaven/te verwijderen (voor de host alleen) (leave_spel/delete_spel)
            //die knp moet een bevestiging opleveren
        }

        public void sluitThreads(object sender, EventArgs e)
        {
            try
            {
                data_thread.Abort();
            }
            catch { }
        }

        public bool start_spel()
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/start_game.php", new string[] { "name", "token" }, new string[] { Online.username, Online.token });
            begonnen = true;
            return raw == "Ja";
        }

        public bool leave_spel()
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/leave_game.php", new string[] { "name", "token" , "spelid"}, new string[] { Online.username, Online.token, Online.game.ToString() });
            return raw == "ja";
        }

        public bool delete_spel()
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/delete_game.php", new string[] { "name", "token", "spelid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
            if (raw == "ja")
            {
                raw = Online.PHPrequest("http://harbingerofme.info/GnF/delete_messages.php", new string[] { "name", "token", "spelid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
            }
            return raw == "ja";
        }

        public bool stuur_chat(string bericht)
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/add_message.php", new string[] { "name", "token", "gameid","message" }, new string[] { Online.username, Online.token, Online.game.ToString(),bericht });
            return raw == "ja";
        }

        public void dataThread()
        {
            string berichten_raw, status_raw, deelnemers_raw;
            while (begonnen == false)
            {
                status_raw = Online.PHPrequest("http://harbingerofme.info/GnF/get_game_status.php", new string[] { "name", "token", "spelid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                if (status_raw == "2")
                {
                    begonnen = true;
                }
                deelnemers_raw = Online.PHPrequest("http://harbingerofme.info/GnF/get_game_deelnemers.php", new string[] { "name", "token", "spelid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                //doe er iets mee (manier om deelnemers te laten zien mist nog)
                berichten_raw = Online.PHPrequest("http://harbingerofme.info/GnF/read_messages.php",new string[] {"name","token","gameid"},new string[] {Online.username,Online.token,Online.game.ToString()});
                //doe er iets mee (manier om chat te laten zien mist nog)

                Thread.Sleep(1000);//slaap voor een seconde.
            }
            //begonnen is veranderd, ga naar het spel
        }
    }

class openSpellenScherm : Form
        {

    Form menuBack;
    float verhouding;
    Font arial;
    Control container = new Control();

        public openSpellenScherm(Form back)
        {
            menuBack = back;
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / BackgroundImage.Width;
            arial = new Font("Arial", (int)(15 * verhouding));
            this.Show();
            container.Location = new Point(0,0);
            Controls.Add(container);
            laatSpellenzien();
            
            //er is een knop nodig die laat zien create game, met de volgende opties daarin:
            // spelnaam, aantal max spelers en regelset
            // stuur ze dan inclusief parameters door naar create_spel(string spelnaam, int spelers,string/int regelset)

            //als één van de knoppen true retourneert, moet er naar het volgende scherm gegaan worden: lobbyScherm
        }

        public void laatSpellenzien()
        {
            online_openSpel[] list = krijgSpellen();
            if (list.Count() > 0)
            {
                int a = 0;
                foreach (online_openSpel oo in list)
                {
                    Label lbl1 = new Label();
                    lbl1.Top = (int)((100 + a * 15) * verhouding);
                    lbl1.Left = (int)(10 * verhouding);
                    lbl1.Text = oo.spelnaam;
                    lbl1.Font = arial;
                    lbl1.BackColor = Color.Transparent;
                    lbl1.ForeColor = Color.White;
                    lbl1.Size = new Size((int)(200 * verhouding), (int)(20 * verhouding));
                    lbl1.Parent = container;
                    Controls.Add(lbl1);

                    Label lbl2 = new Label();
                    lbl2.Top = (int)((100 + a * 15) * verhouding);
                    lbl2.Left = (int)(210 * verhouding);
                    lbl2.Text = oo.host;
                    lbl2.Font = arial;
                    lbl2.BackColor = Color.Transparent;
                    lbl2.ForeColor = Color.White;
                    lbl2.Size = new Size((int)(150 * verhouding), (int)(20 * verhouding));
                    lbl2.Parent = container;
                    Controls.Add(lbl2);

                    Label lbl3 = new Label();
                    lbl3.Top = (int)((100 + a * 15) * verhouding);
                    lbl3.Left = (int)(360 * verhouding);
                    lbl3.Text = oo.spelerAantal + "/" + oo.maxSpelerAantal;
                    lbl3.Font = arial;
                    lbl3.BackColor = Color.Transparent;
                    lbl3.ForeColor = Color.White;
                    lbl3.Size = new Size((int)(60 * verhouding), (int)(20 * verhouding));
                    lbl3.Parent = container;
                    Controls.Add(lbl3);

                    Label lbl4 = new Label();
                    lbl4.Top = (int)((100 + a * 15) * verhouding);
                    lbl4.Left = (int)(420 * verhouding);//420 blaze it
                    lbl4.Text = oo.Spelregel;
                    lbl4.Font = arial;
                    lbl4.BackColor = Color.Transparent;
                    lbl4.ForeColor = Color.White;
                    lbl4.Size = new Size((int)(100 * verhouding), (int)(20 * verhouding));
                    lbl4.Parent = container;
                    Controls.Add(lbl4);

                    PictureBox but5 = new PictureBox();
                    but5.Name = oo.id.ToString();
                    //but5.Image = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Join_knop"));
                    but5.Size =  new Size((int)(152*verhouding),(int)(47*verhouding));
                    but5.Location = new Point((int)((100 + a * 15) * verhouding), (int)(520 * verhouding));
                    but5.Parent = container;
                    but5.Click += join_Click;
                    Controls.Add(but5);

                    a++;
                }
            }
            else
            {
                Label lbl = new Label();
                lbl.Top = (int)((115) * verhouding);
                lbl.Left = (int)(10 * verhouding);
                lbl.Text = "Geen spellen gevonden";
                lbl.Font = arial;
                lbl.ForeColor = Color.White;
                lbl.Size = new Size((int)(200 * verhouding), (int)(20 * verhouding));
                Controls.Add(lbl);
            }
        }

        public void join_Click(object sender, EventArgs e)
        {
            PictureBox but = (PictureBox)sender;
            if(join_spel(int.Parse(but.Name))){
                ga_verder();
            }
        }
        
        public bool create_spel(string spelnaam, int spelers, string regelset)
        {
            int seed = new Random().Next(400000000);
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/create_game.php", new string[] { "name", "token", "seed", "naam", "aantal", "metadata" }, new string[] { Online.username, Online.token, seed.ToString(), spelnaam, spelers.ToString(), regelset });
            if(raw.StartsWith("Error:")){
                return false;
            }
            else{
                Online.game = int.Parse(raw);
                Online.onlineRandom = new Random(seed);
            return true;
            }
        } 

        public bool join_spel(int spelid)
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/join_game.php", new string[] { "name", "token","spelid" }, new string[] { Online.username, Online.token,spelid.ToString() });
            if (raw.StartsWith("Error:"))
            {
                return false;
            }
            else
            {
                Online.onlineRandom = new Random(int.Parse(raw));
                Online.game = spelid;
                return true;
            }
        }

        public void ga_verder()
        {
            this.Hide();
            Form veld = new lobbyScherm(this);
            veld.FormClosing += veld_FormClosing;
        }

        public void veld_FormClosing(object o, EventArgs e)
        {
            lobbyScherm sender = (lobbyScherm) o;
            if (!this.Visible)
            {
                this.Show();
                if (!sender.leave_spel())
                {
                    sender.delete_spel();
                }
            }
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
                    string[] temp = splits[2].Split(',');string temp2 = "";
                    List<string> temp4 =  new List<string>();
                    foreach (string dn in temp)
                    {
                        temp4.AddRange(dn.Split(';'));
                    }
                    for(int i = 2; i<temp4.Count();i+=2){
                        temp2 += temp[i]+" ";
                    }
                    temp2 = temp2.Trim();
                    returnal.Add(new online_openSpel(int.Parse(splits[0]),"Missend",temp.Count(),int.Parse(splits[1]),splits[4],splits[3].Equals("2"),temp4[0],temp2));
                    copy =  copy.Substring(copy.IndexOf('}')+1);

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
        public static Random onlineRandom;
        public bool is_host;


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

    }

}
