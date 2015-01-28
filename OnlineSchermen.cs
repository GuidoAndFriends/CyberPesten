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
using System.Text.RegularExpressions;
using System.Media;

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

        Bitmap maakAccountMenu, maakAccount, terug;
        Rectangle maakAccountButton, terugButton;
        bool maakAccountHover, terugHover;
        double verhoudingW,verhoudingH;
        Font arial;
        Form menuBack;
            

        public inlogScherm(Form _menu)
        {
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhoudingW = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)1920;
            verhoudingH = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)1080;
            arial = new Font("Arial", (int)(15 * verhoudingH));
            menuBack = _menu;

            string GNF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Guido&Friends");
            if (!Directory.Exists(GNF)) { Directory.CreateDirectory(GNF); }
            CP = Path.Combine(GNF, "Cyperpesten");
            if (!Directory.Exists(CP)) { Directory.CreateDirectory(CP); }
            maakAccountMenu = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("online_menu"));
            maakAccount = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("maak_account_select"));
            terug = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Terug_button"));

            maakAccountButton = new Rectangle((int)(857 * verhoudingW), (int)(758 * verhoudingH), (int)(205 * verhoudingW), (int)(86 * verhoudingH));
            terugButton = new Rectangle(0, (int)(verhoudingH * 53), (int)(verhoudingW * 234), (int)(verhoudingH * 74));

            this.Paint += this.buildMenu;
            this.MouseMove += this.hover;
            this.MouseClick += this.klik;

            maakAccountTextbox1.AcceptsReturn = false;
            maakAccountTextbox1.AcceptsTab = false;
            maakAccountTextbox1.BackColor = Color.White;
            maakAccountTextbox1.MaxLength = 20;
            maakAccountTextbox1.TextAlign = HorizontalAlignment.Left;
            maakAccountTextbox1.Top = (int)(485 * verhoudingH);
            maakAccountTextbox1.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - (int)(80 * verhoudingW);
            maakAccountTextbox1.Width = (int)(320 * verhoudingW);
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
            maakAccountTextbox2.Top = (int)(570 * verhoudingH);
            maakAccountTextbox2.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - (int)(80 * verhoudingW);
            maakAccountTextbox2.Width = (int)(320 * verhoudingW);
            maakAccountTextbox2.TabIndex = 1;
            maakAccountTextbox2.Parent = a;
            maakAccountTextbox2.Font = arial;

            Controls.Add(maakAccountTextbox2);
            Controls.Add(maakAccountTextbox1);


            maakAccountTextbox1.Select();
            berichtHouder = new Label();
            berichtHouder.Text = "";
            berichtHouder.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - (int)(300 * verhoudingW);
            berichtHouder.Top = (int)(400*verhoudingH);
            berichtHouder.TextAlign = ContentAlignment.TopCenter;
            berichtHouder.Width = (int)(600*verhoudingW);
            berichtHouder.Height = (int)(80*verhoudingH);
            berichtHouder.BackColor = Color.Transparent;
            berichtHouder.ForeColor = Color.Red;
            berichtHouder.Font = new Font("Arial", (int)(20*verhoudingH));
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

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        private void buildMenu(object sender, PaintEventArgs pea)
        {
            pea.Graphics.DrawImage(maakAccountMenu, 0, 0, (float)(maakAccountMenu.Width * verhoudingW), (float)(maakAccountMenu.Height * verhoudingH));

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
            if (maakAccountHover)
            {
                buttonSound();
                maakAccountKnop_Click();
            }
            else if (terugHover)
            {
                buttonSound();
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
        double verhoudingW,verhoudingH;
        Font arial;
        Form menuBack;
        Form spel;

        public string[] deelnemers;
        List<String> raw_chat;
        public bool begonnen = false;
        Thread data_thread;
        bool closed = false;

        Rectangle delGame, startGame, leaveGame;
        bool delHover, startHover, leaveHover;
        List<Control> deelnemerLijst = new List<Control>();
        TextBox invoer;
        Label uitvoer;//ha!
        Bitmap buttons;

        GraphicsUnit units = GraphicsUnit.Pixel;


        public lobbyScherm(Form back)
        {
            raw_chat = new List<string>();
            menuBack = back;
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Lobby_menu");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhoudingW = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)1920;
            verhoudingH = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)1080;
            arial = new Font("Arial", (int)(15 * verhoudingH));
            this.Show();
            this.FormClosing += sluitThreads;
            this.MouseMove += hover;
            this.Click += klik;
            this.Paint += this.teken;

            delGame = new Rectangle((int)(109*verhoudingW),(int)(956*verhoudingH),(int)(191*verhoudingW),(int)(89*verhoudingH));
            startGame = new Rectangle((int)(309 * verhoudingW), (int)(956 * verhoudingH), (int)(191 * verhoudingW), (int)(89 * verhoudingH));
            leaveGame = new Rectangle((int)(700 * verhoudingW), (int)(956 * verhoudingH), (int)(191 * verhoudingW), (int)(89 * verhoudingH));

            buttons = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Lobby_buttons"));

            invoer = new TextBox();
            invoer.Location = new Point((int)(verhoudingW * 1155), (int)(verhoudingH * 1040));
            invoer.Size = new Size((int)(765 * verhoudingW), (int)(44 * verhoudingH));
            invoer.Font = arial;
            invoer.BackColor = System.Drawing.ColorTranslator.FromHtml("#121212");
            invoer.KeyPress += invoer_KeyPress;
            invoer.ShortcutsEnabled = false;//we willen niet dat mensen kunnen control+v'en in chat, omdat spam.
            invoer.TextAlign = HorizontalAlignment.Left;
            invoer.MaxLength = 1024;
            invoer.ForeColor = Color.White;
            Controls.Add(invoer);
            invoer.Focus();

            uitvoer = new Label();
            uitvoer.Location = new Point((int)(verhoudingW * 1155), (int)(verhoudingH * 147));//iets van de randen af.
            uitvoer.Size = new Size((int)(765 * verhoudingW),(int)(890 * verhoudingH));
            uitvoer.TextAlign = ContentAlignment.BottomLeft;
            uitvoer.Font = arial;
            uitvoer.BackColor = Color.Transparent;
            uitvoer.ForeColor = Color.White;
            Controls.Add(uitvoer);

            data_thread = new Thread(dataThread);
            data_thread.IsBackground = true;
            data_thread.Start();
        }

        public void teken(object sender, PaintEventArgs pea)
        {
            if (delHover)
            {
                pea.Graphics.DrawImage(buttons, delGame, delGame, units);
            }
            if (startHover)
            {
                pea.Graphics.DrawImage(buttons, startGame, startGame, units);
            }
            if (leaveHover)
            {
                pea.Graphics.DrawImage(buttons, leaveGame, leaveGame, units);
            }
        }


        void invoer_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox a = (TextBox) sender;
            if (e.KeyChar == (char)Keys.Return)
            {
                stuur_chat(a.Text);
                a.Text = "";
            }
        }

        void schrijfUitvoer()
        {
            if (uitvoer.InvokeRequired)
            {
                uitvoer.Invoke(new MethodInvoker(schrijfUitvoer));
            }
            else
            {
                string text = "";
                if (raw_chat.Count > 0)
                {
                    for (int a = 0; a < (int)((890 * verhoudingH)/(25*verhoudingH)) && a < raw_chat.Count; a++)
                    {
                        string str = raw_chat[a];
                        text = "\n" + str + text;
                    }
                }
                uitvoer.Text = text;
            }

        }
        public void laatDNzien()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.laatDNzien));
            }
            else
            {
                foreach (Control c in deelnemerLijst)
                {
                    Controls.Remove(c);
                }


               int a = 0;
                foreach (string s in deelnemers)
                {
                    string naam = s.Split(':')[0];
                    string rank = s.Split(':')[1];

                    Label lbl1 = new Label();
                    lbl1.Font = arial;
                    lbl1.ForeColor = Color.White;
                    lbl1.BackColor = Color.Transparent;
                    lbl1.Location = new Point((int)(10*verhoudingW),(int)((190+30*a)*verhoudingH));
                    lbl1.Size = new Size((int)(600 * verhoudingW), (int)(30 * verhoudingH));
                    lbl1.Text = naam;
                    Controls.Add(lbl1);

                    Label lbl2 = new Label();
                    lbl2.Font = arial;
                    lbl2.ForeColor = Color.White;
                    lbl2.BackColor = Color.Transparent;
                    lbl2.Location = new Point((int)(617 * verhoudingW), (int)((190 + 30 * a)*verhoudingH));
                    lbl2.Size = new Size((int)(354 * verhoudingW), (int)(30 * verhoudingH));
                    lbl2.TextAlign = ContentAlignment.TopCenter;
                    lbl2.Text = rank;
                    Controls.Add(lbl2);

                    a++;
                }
            }
        }

        public void gaVerder()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(gaVerder));
            }
            else
            {
                spel = new OnlineSpeelveld(menuBack);
                spel.Show();
                spel.FormClosed += spel_FormClosed;

                this.Hide();
            }             
        }

        void spel_FormClosed(object sender, FormClosedEventArgs e)
        {
            delete_spel();
            leave_spel();
            begonnen = false;
            Online.game = -1;
            this.Close();
        }


        public void hover(object o, MouseEventArgs e)
        {
            delHover = false; startHover = false; leaveHover = false;
            if (delGame.Contains(e.Location))
            {
                delHover = true;
            }
            if (startGame.Contains(e.Location))
            {
                startHover = true;
            }
            if (leaveGame.Contains(e.Location))
            {
                leaveHover = true;
            }

            Invalidate();
        }

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        public void klik(object o, EventArgs e)
        {
            if ((delHover || leaveHover) && Online.is_host)
            {
                buttonSound();
                delete_spel();
                menuBack.Show();
                Close();

            }
            if (leaveHover && !Online.is_host)
            {
                buttonSound();
                leave_spel();
                menuBack.Show();
                Close();
            }
            if (startHover && Online.is_host && deelnemers.Count() > 1)
            {
                buttonSound();
                start_spel();
            }
        }

        public void sluitThreads(object sender, EventArgs e)
        {
            closed = true;
            try
            {
                data_thread.Abort();
            }
            catch { }
        }

        public bool start_spel()
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/start_game.php", new string[] { "name", "token","spelid" }, new string[] { Online.username, Online.token,Online.game.ToString() });
            if (raw == "Ja")
            {
                begonnen = true;
            }
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
                return true;
            }
            return false;
        }

        public bool stuur_chat(string bericht)
        {
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/add_message.php", new string[] { "name", "token", "gameid","message" }, new string[] { Online.username, Online.token, Online.game.ToString(),bericht });
            return raw == "ja";
        }

        public void dataThread()
        {
            string berichten_raw, status_raw, deelnemers_raw;
            while (begonnen == false && closed == false)
            {
                status_raw = Online.PHPrequest("http://harbingerofme.info/GnF/get_game_status.php", new string[] { "name", "token", "gameid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                if (status_raw == "2")
                {
                    begonnen = true;
                }
                if (status_raw == "Error: Game bestaat niet!")
                {
                    MessageBox.Show("Host heeft het spel verlaten.");
                    menuBack.Show();
                    Close();
                }
                deelnemers_raw = Online.PHPrequest("http://harbingerofme.info/GnF/get_names.php", new string[] { "name", "token", "gameid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                if (!deelnemers_raw.StartsWith("Error:"))
                {
                    deelnemers = deelnemers_raw.Split(',');
                    laatDNzien();
                }
                if (deelnemers_raw == "Error: Game bestaat niet!")
                {
                    MessageBox.Show("Host heeft het spel verlaten.");
                    menuBack.Show();
                    Close();
                }
                berichten_raw = Online.PHPrequest("http://harbingerofme.info/GnF/read_messages.php", new string[] { "name", "token", "gameid" }, new string[] { Online.username, Online.token, Online.game.ToString() });
                if (berichten_raw != "" && !berichten_raw.StartsWith("Error"))
                {
                    string[] temp = berichten_raw.Split(new string[] {"|"},StringSplitOptions.RemoveEmptyEntries);
                    raw_chat = new List<string>();
                    foreach (string str in temp)
                    {
                        
                        raw_chat.Add("<" + str.Split(':')[0] + ">: " + str.Split(new string[] { ":" }, 2, StringSplitOptions.None)[1]);
                    }
                    schrijfUitvoer();
                }
                else
                {
                    if (berichten_raw == "")
                    {
                        raw_chat.Clear();
                    }
                    if (berichten_raw == "Error: Game bestaat niet!")
                    {
                        MessageBox.Show("Host heeft het spel verlaten.");
                        menuBack.Show();
                        Close();
                    }
                }

                if (!begonnen)
                {
                    Thread.Sleep(1000);//slaap voor een seconde.
                }
            }
            Online.deelnemers = deelnemers;
            gaVerder();
            //begonnen is veranderd, ga naar het spel
        }
    }

class openSpellenScherm : Form
        {

    Form menuBack;
    double verhoudingW,verhoudingH;
    Font arial;
    List<Control> lijstcontrol = new List<Control>();
    bool closed = false,hostOpen = false;
    Form v;
    Thread data_thread;
    Rectangle hostgame, exit;
    bool hostHover,exitHover;
    Bitmap buttons;
    GraphicsUnit units = GraphicsUnit.Pixel;

        public openSpellenScherm(Form back)
        {
            menuBack = back;
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Spellenscherm");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            verhoudingW = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)1920;
            verhoudingH = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)1080;
            arial = new Font("Arial", (int)(15 * verhoudingH));
            this.Show();
            laatSpellenzien();
            data_thread = new Thread(data);
            data_thread.IsBackground = true;
            data_thread.Start();

            hostgame = new Rectangle((int)(1673 * verhoudingW) ,(int) (693*verhoudingH),(int) (252*verhoudingW),(int)(95*verhoudingH));
            exit = new Rectangle((int)(1673 * verhoudingW), (int)(809 * verhoudingH), (int)(252 * verhoudingH), (int)(95 * verhoudingW));

            buttons = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Spellenscherm_buttons"));

            this.FormClosing += closing;
            this.FormClosed += closing;

            this.MouseMove += this.hover;
            this.MouseClick += this.klik;

            this.Paint += this.teken;
            
            //er is een knop nodig die laat zien create game, met de volgende opties daarin:
            // spelnaam, aantal max spelers en regelset
            // stuur ze dan inclusief parameters door naar create_spel(string spelnaam, int spelers,string/int regelset)

            //als één van de knoppen true retourneert, moet er naar het volgende scherm gegaan worden: ga_verder();
        }

        public void teken(object sender, PaintEventArgs pea)
        {
            if (hostHover)
            {
                pea.Graphics.DrawImage(buttons, hostgame, hostgame, units);
            }
            if (exitHover)
            {
                pea.Graphics.DrawImage(buttons, exit, exit, units);
            }
        }

        public void laatSpellenzien()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.laatSpellenzien));
            }
            else
            {
                foreach (Control c in lijstcontrol)
                {
                    Controls.Remove(c);
                }
                online_openSpel[] list = krijgSpellen();
                if (list.Count() > 0)
                {
                    int a = 0;
                    foreach (online_openSpel oo in list)
                    {
                        Label lbl1 = new Label();
                        lbl1.Top = (int)((198 + a * 30) * verhoudingH);
                        lbl1.Left = (int)(10 * verhoudingW);
                        lbl1.Text = oo.spelnaam;
                        lbl1.Font = arial;
                        lbl1.BackColor = Color.Transparent;
                        lbl1.ForeColor = Color.White;
                        lbl1.Size = new Size((int)(500 * verhoudingW), (int)(30 * verhoudingH));
                        lijstcontrol.Add(lbl1);
                        Controls.Add(lbl1);

                        Label lbl2 = new Label();
                        lbl2.Top = (int)((198 + a * 30) * verhoudingH);
                        lbl2.Left = (int)(532 * verhoudingW);
                        lbl2.Text = oo.host.Split(':')[0];
                        lbl2.Font = arial;
                        lbl2.TextAlign = ContentAlignment.TopCenter;
                        lbl2.BackColor = Color.Transparent;
                        lbl2.ForeColor = Color.White;
                        lbl2.Size = new Size((int)(350 * verhoudingW), (int)(30 * verhoudingH));
                        lijstcontrol.Add(lbl2);
                        Controls.Add(lbl2);

                        Label lbl3 = new Label();
                        lbl3.Top = (int)((198 + a * 30) * verhoudingH);
                        lbl3.Left = (int)(881 * verhoudingW);
                        lbl3.Text = oo.spelerAantal + "/" + oo.maxSpelerAantal;
                        lbl3.Font = arial;
                        lbl3.TextAlign = ContentAlignment.TopCenter;
                        lbl3.BackColor = Color.Transparent;
                        lbl3.ForeColor = Color.White;
                        lbl3.Size = new Size((int)(270 * verhoudingW), (int)(30 * verhoudingH));
                        lijstcontrol.Add(lbl3);
                        Controls.Add(lbl3);


                        string text;
                        switch (oo.Spelregel)
                        {
                            case "1": text = "Standaard"; break;
                            case "2": text = "Familie"; break;
                            default: text = "Aangepast"; break;
                        }

                        Label lbl4 = new Label();
                        lbl4.Top = (int)((198 + a * 30) * verhoudingH);
                        lbl4.Left = (int)(1147 * verhoudingW);
                        lbl4.Text = text;
                        lbl4.Font = arial;
                        lbl4.TextAlign = ContentAlignment.TopCenter;
                        lbl4.BackColor = Color.Transparent;
                        lbl4.ForeColor = Color.White;
                        lbl4.Size = new Size((int)(276 * verhoudingW), (int)(30 * verhoudingH));
                        lijstcontrol.Add(lbl4);
                        Controls.Add(lbl4);

                        PictureBox but5 = new PictureBox();
                        but5.Name = oo.id.ToString();
                        but5.Image = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Join_knop"));
                        but5.Size = new Size((int)(192 * verhoudingW), (int)(31 * verhoudingH));
                        but5.Location = new Point((int)(1422 * verhoudingW), (int)((195 + a * 30) * verhoudingH));
                        lijstcontrol.Add(but5);
                        but5.Click += join_Click;
                        Controls.Add(but5);

                        a++;
                    }
                }
                else
                {
                    Label lbl1 = new Label();
                    lbl1.Top = (int)((198) * verhoudingH);
                    lbl1.Left = (int)(10 * verhoudingW);
                    lbl1.Text = "Geen Spellen Gevonden";
                    lbl1.Font = arial;
                    lbl1.BackColor = Color.Transparent;
                    lbl1.ForeColor = Color.White;
                    lbl1.Size = new Size((int)(500 * verhoudingW), (int)(30 * verhoudingH));
                    lijstcontrol.Add(lbl1);
                    Controls.Add(lbl1);
                }
            }
            
        }

        public void join_Click(object sender, EventArgs e)
        {
            PictureBox but = (PictureBox)sender;
            if(join_spel(int.Parse(but.Name))){
                Online.is_host = false;
                foreach (online_openSpel oo in krijgSpellen())
                {
                    if (oo.id.ToString() == but.Name)
                    {
                        if (Online.username == oo.host.Split(':')[0])
                        {
                            Online.is_host = true;
                        }
                        break;
                    }
                }
                ga_verder();
            }
        }

        public void hover(object o, MouseEventArgs e)
        {
            hostHover = false; exitHover = false;
            if (hostgame.Contains(e.Location)) { hostHover = true; }
            if (exit.Contains(e.Location)) { exitHover = true; }

            Invalidate();
        }

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        public void klik(object o, MouseEventArgs e)
        {
            if (hostHover)
            {
                if (!hostOpen)
                {
                    buttonSound();
                    hostOpen = true;
                    v = new hostGameOpties(this);
                    v.FormClosed += hostClosed;
                }
                else
                {
                    v.Focus();
                }
            }

            if (exitHover)
            {
                menuBack.Show();
                this.Close();
            }
        }

        public void hostClosed(object o, EventArgs e)
        {
            hostOpen = false;
        }

        public void closing(object o, EventArgs e)
        {
            try
            {
                v.Close();
                data_thread.Abort();
                closed = true;
            }
            catch { }
        }

        public void data()
        {
            while (!closed)
            {
                laatSpellenzien();
                Thread.Sleep(30000);
            }
        }

        public bool create_spel(string spelnaam, int spelers, string regelset)
        {
            int seed = new Random().Next(400000000);
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/new_game.php", new string[] { "name", "token", "seed", "naam", "aantal", "metadata" }, new string[] { Online.username, Online.token, seed.ToString(), spelnaam, spelers.ToString(), regelset });
            if(raw.StartsWith("Error:")){
                return false;
            }
            else{
                Online.game = int.Parse(raw);
                Online.onlineRandom = new Random(seed);
                Online.is_host = true;
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
            laatSpellenzien();
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
            Online.PHPrequest("http://harbingerofme.info/GnF/last_action.php", new string[] { "name", "token" }, new string[] { Online.username, Online.token });//we caren niet echt om de result hiervan, dat is voor de server
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/get_games.php", new string[] { "name", "token" }, new string[] { Online.username, Online.token });
            if (!raw.StartsWith("Error:"))
            {
                string copy = raw.Substring(1, raw.Length - 2);//strip begin en einde
                while(copy.Length>0)
                {
                    string substr = copy.Substring(1, copy.IndexOf('}') - 1);
                    string[] splits = substr.Split('|');
                    string[] temp = splits[2].Split(','); string temp2 = ""; List<string> temp5 = new List<string>();
                    List<string> temp4 =  new List<string>();
                    foreach (string dn in temp)
                    {
                        temp4.AddRange(dn.Split(';'));
                    }
                    foreach (string dn in temp4)
                    {
                        temp5.Add(dn.Split(':')[0]);
                    }
                    for(int i = 2; i<temp5.Count();i+=2){
                        temp2 += temp5[i]+" ";
                    }
                    temp2 = temp2.Trim();
                    returnal.Add(new online_openSpel(int.Parse(splits[0]),splits[5],temp.Count(),int.Parse(splits[1]),splits[4],splits[3].Equals("2"),temp4[0],temp2));
                    copy =  copy.Substring(copy.IndexOf('}')+1);

                }
            }
            return returnal.ToArray();
        }
    }

public class hostGameOpties : Form
    {
        openSpellenScherm oscherm;
        Bitmap achtergrond, startButton;
        Rectangle maat;
        TextBox name, spelers;
        ComboBox regels;
        Rectangle startRect;
        bool startBool;

        public hostGameOpties(Form o)
        {
            DoubleBuffered = true;
            maat = new Rectangle(0, 0, 798, 528);

            oscherm = (openSpellenScherm)o;
            this.ClientSize = maat.Size;

            achtergrond = new Bitmap(maat.Width, maat.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Spel_instellingen"), maat);
            BackgroundImage = achtergrond;

            this.Text = "Spel Instellingen";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Show();

            startButton = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Start"));
            startRect = new Rectangle(435, 425, 263, 75);

            this.MouseMove += this.hover;
            this.Paint += this.teken;
            this.MouseClick += this.klik;

            name = new TextBox();
            name.Location = new Point(468, 115);
            name.Size = new Size(180, 300);
            name.Width = 280;
            name.Font = new Font("Arial", 15);
            name.MaxLength = 40;
            Controls.Add(name);

            spelers = new TextBox();
            spelers.Location = new Point(468, 215);
            spelers.Size = new Size(180, 300);
            spelers.Width = 280;
            spelers.Font = new Font("Arial", 15);
            spelers.MaxLength = 1;
            Controls.Add(spelers);

            regels = new ComboBox();
            regels.Items.Add("Standaard");
            regels.Items.Add("Familie");
            regels.Location = new Point(468, 315);
            regels.Size = new Size(180, 300);
            regels.Width = 280;
            regels.Font = new Font("Arial", 15);
            regels.MaxLength = 2;
            Controls.Add(regels);
            regels.SelectedIndex = 0;
        }

        public void hover(object sender, MouseEventArgs mea)
        {
            if (startRect.Contains(mea.Location))
            {
                startBool = true;
            }
            else { startBool = false; }

            Invalidate();
        }

        public void teken(object sender, PaintEventArgs pea)
        {
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            pea.Graphics.DrawImage(BackgroundImage, 0, 0);
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            if (startBool)
            {
                pea.Graphics.DrawImage(startButton, maat);
            }
        }

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        public void klik(object sender, MouseEventArgs mea)
        {
            if (startBool)
            {
                buttonSound();
                start(this, mea);
            }
        }

        public void start(object o, EventArgs e)
        {
            if (Regex.Match(name.Text, @"^[ a-zA-Z0-9]+$").Success && Regex.Match(name.Text, @"[a-zA-Z0-9]").Success && Regex.Match(spelers.Text, @"^[2-8]$").Success && name.Text.Length<40)
            {
                if (oscherm.create_spel(name.Text, int.Parse(spelers.Text), (regels.SelectedIndex+1).ToString()))
                {
                    oscherm.ga_verder();
                    this.Close();
                }

            }
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
        public static bool is_host;
        public static string[] deelnemers;

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
