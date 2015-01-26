using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Media;
using System.IO;

namespace CyberPesten
{
    class Speelveld : Form
    {
        public Form form;
        public Spel spel;
        public int kaartBreedte, kaartHoogte, afstand;
        public Point stapelPlek, potPlek;
        protected Rectangle maat, helpRect, settingsRect, homeRect, laatsteKaartRect, eindeBeurtRect;
        protected bool helpBool, settingsBool, homeBool, laatsteKaartBool, eindeBeurtBool;
        protected float verhouding;
        protected GraphicsUnit units = GraphicsUnit.Pixel;
        int chatHoogte;


        public Bitmap achterkant;

        //Voor het verslepen van een kaart
        public Kaart bewegendeKaart;
        public bool muisLaag;
        public int laagX, laagY;

        //Voor het verschuiven van de hand van de speler
        public Thread schuifAnimatie;
        public int delta;

        //Voor het verplaatsen van een kaart die gespeeld of gepakt wordt
        public Kaart verplaatsendeKaart;
        public bool zichtbaar;

        //Voor de buttons
        //Rectangle helpButton, settingsButton, homeButton, laatsteKaartButton, eindeBeurtButton;
        public Bitmap buttonsBitmap;
        //int buttonWidth;
        public Brush laatsteKaartBrush;

        //Tijdelijk
        public Button klaver, harten, ruiten, schoppen;

        public Speelveld() { }

        //chat
        public Chat chat;
        public Button vouwen, send;
        public TextBox textbox;

        public Speelveld(Form form)
        {
            Instellingen instellingen = new Instellingen();
            this.form = form;
            kaartBreedte = 110;
            kaartHoogte = 153;
            afstand = 10;
            maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            muisLaag = false;

            Size = form.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            Bitmap achtergrond = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond"), maat);
            BackgroundImage = achtergrond;

            achterkant = new Bitmap(110, 153, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            string achterkantDesign;
            if (instellingen.achterkant == 0)
            {
                achterkantDesign = "Back_design_1";
            }
            else if (instellingen.achterkant == 1)
            {
                achterkantDesign = "Back_design_2";
            }
            else
                achterkantDesign = "Back_design_3";
            Graphics.FromImage(achterkant).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject(achterkantDesign), 0, 0, kaartBreedte, kaartHoogte);

            stapelPlek = new Point(Width / 2 - 50 - kaartBreedte, Height / 2 - kaartHoogte / 2);
            potPlek = new Point(Width / 2 + 50, Height / 2 - kaartHoogte / 2);
            laatsteKaartBrush = Brushes.Red;

            Paint += teken;
            MouseClick += muisKlik;
            MouseMove += muisBeweeg;
            MouseDown += muisOmlaag;
            MouseUp += muisOmhoog;
            MouseWheel += muisWiel;
            MouseMove += hover;
            //Scroll += scroll;

            startSpel(instellingen);
            
            /*
            helpBitmap = (Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help_button");
            settingsBitmap = (Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings_button");
            homeBitmap = (Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Home_button");
            laatsteKaartBitmap = (Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Laatste_kaart");
            eindeBeurtBitmap = (Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Einde_beurt");*/

            buttonsBitmap =((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Speelveld_buttons"));
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / buttonsBitmap.Width;

            //Buttons
            int bigButtonWidth = (int)(verhouding * 84);
            int smallButtonWidth = (int)(verhouding * 52);
            int buttonRij = (int)(verhouding * 1835);

            homeRect = new Rectangle(buttonRij, (int)(verhouding * 453), smallButtonWidth, smallButtonWidth);
            settingsRect = new Rectangle(buttonRij, (int)(verhouding * 514), smallButtonWidth, smallButtonWidth);
            helpRect = new Rectangle(buttonRij, (int)(verhouding * 575), smallButtonWidth, smallButtonWidth);
            laatsteKaartRect = new Rectangle((int)(verhouding * 1309), (int)(verhouding * 498), bigButtonWidth, bigButtonWidth);
            eindeBeurtRect = new Rectangle((int)(verhouding * 1187), (int)(verhouding * 498), bigButtonWidth, bigButtonWidth);

            /*
            buttonWidth = helpBitmap.Width;

            laatsteKaartButton = new Rectangle(Width / 2 + 100 + laatsteKaartBitmap.Width, this.Height / 2 - laatsteKaartBitmap.Width / 2 + 5, laatsteKaartBitmap.Width, laatsteKaartBitmap.Width);
            eindeBeurtButton = new Rectangle(Width / 2 + 125 + 2 * laatsteKaartBitmap.Width, this.Height / 2 - laatsteKaartBitmap.Width / 2 + 5, laatsteKaartBitmap.Width, laatsteKaartBitmap.Width);
            helpButton = new Rectangle(Width - 75 - 3 * buttonWidth, this.Height / 2 - buttonWidth / 2, buttonWidth, buttonWidth);
            settingsButton = new Rectangle(Width - 50 - 2 * buttonWidth, this.Height / 2 - buttonWidth / 2, buttonWidth, buttonWidth);
            homeButton = new Rectangle(Width - 25 - buttonWidth, this.Height / 2 - buttonWidth / 2, buttonWidth, buttonWidth);*/

            klaver = new Button();
            klaver.Click += klaver_Click;
            klaver.Location = new Point(50, 250);
            klaver.Size = new Size(200, 50);
            klaver.Text = "Maak er Klaver van";

            harten = new Button();
            harten.Click += harten_Click;
            harten.Location = new Point(300, 250);
            harten.Size = new Size(200, 50);
            harten.Text = "Maak er Harten van";

            ruiten = new Button();
            ruiten.Click += ruiten_Click;
            ruiten.Location = new Point(50, 350);
            ruiten.Size = new Size(200, 50);
            ruiten.Text = "Maak er Ruiten van";

            schoppen = new Button();
            schoppen.Click += schoppen_Click;
            schoppen.Location = new Point(300, 350);
            schoppen.Size = new Size(200, 50);
            schoppen.Text = "Maak er Schoppen van";

            chatHoogte = Height / 2;
            textbox = new TextBox();
            textbox.Size = new Size(300, 50);
            textbox.Location = new Point(40, chatHoogte - FontHeight / 2 + 75);
            textbox.Click += vouwen_Click;
            textbox.KeyDown += new KeyEventHandler(send_Click);

            this.Controls.Add(textbox);
            this.Controls.Add(klaver);
            this.Controls.Add(harten);
            this.Controls.Add(ruiten);
            this.Controls.Add(schoppen);
            verbergKleurknoppen();

            this.Show();
        }

        void send_Click(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                spel.send(textbox.Text);
                textbox.Clear();
                Invalidate();
            }
        }

        void vouwen_Click(object sender, EventArgs e)
        {
            Rectangle r = new Rectangle(textbox.Location, textbox.Size);

            if(spel.groot)
            {
                chatHoogte = Height / 2 + 5;
                spel.groot = false;
                textbox.Location = new Point(40, chatHoogte - FontHeight / 2 + 75);
            }
            else
            {
                chatHoogte = Height / 4;
                spel.groot = true;
                textbox.Location = new Point(40, Height - 250);
            }
            Invalidate();
        }

        public virtual void startSpel(Instellingen instellingen)
        {
                Text = "CyberPesten: Lokaal spel";
                spel = new Spel(this, instellingen);
        }

        protected void teken(object sender, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            //achtergrond
            gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            gr.DrawImage(BackgroundImage, 0, 0);
            gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            //stapel
            Bitmap plaatje = spel.stapel[spel.stapel.Count - 1].voorkant;
            gr.DrawImage(plaatje, stapelPlek);

            //pot
            gr.DrawImage(achterkant, potPlek);

            //hand van speler
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                //if (kaart != null)
                {
                    gr.DrawImage(kaart.voorkant, kaart.X, kaart.Y - 20);
                }
            }

            //blokken van AI
            int breedte = (spel.spelers.Count - 1) * 350;
            int tussenruimte;
            if (spel.spelers.Count > 2)
            {
                tussenruimte = (Width - breedte - 20) / (spel.spelers.Count - 2);
            }
            else
            {
                tussenruimte = 0;
            }
            for (int i = 1; i < spel.spelers.Count; i++)
            {
                gr.DrawImage(spel.spelers[i].blok, 10 + (350 + tussenruimte) * (i - 1), 10);
            }

            //een eventuele bewegende en verplaatsende kaart
            if (bewegendeKaart != null)
            {
                gr.DrawImage(bewegendeKaart.voorkant, bewegendeKaart.X, bewegendeKaart.Y);
            }
            if (verplaatsendeKaart != null)
            {
                if (zichtbaar)
                {
                    gr.DrawImage(verplaatsendeKaart.voorkant, verplaatsendeKaart.X, verplaatsendeKaart.Y);
                }
                else
                {
                    gr.DrawImage(achterkant, verplaatsendeKaart.X, verplaatsendeKaart.Y);
                }
            }

            //status van het spel
            gr.DrawImage(spel.chat.maakBitmap(spel.groot), new Point(40, chatHoogte - FontHeight / 2));
            //gr.DrawString(spel.aantalKaarten, new Font(FontFamily.GenericSansSerif, 14), Brushes.White, new Point(40, Height / 2 - 2 * FontHeight));
            //gr.DrawString(spel.speciaalTekst, new Font(FontFamily.GenericSansSerif, 14), Brushes.White, new Point(40, Height / 2 - 4 * FontHeight));

            //Buttons
            /*
            gr.DrawImage(helpBitmap, helpButton);
            gr.DrawImage(settingsBitmap, settingsButton);
            gr.DrawImage(homeBitmap, homeButton);
            gr.FillEllipse(laatsteKaartBrush, laatsteKaartButton);
            gr.DrawImage(laatsteKaartBitmap, laatsteKaartButton);
            if (spel.spelend == 0 && spel.magZet)
            {
                gr.DrawImage(eindeBeurtBitmap, eindeBeurtButton);
            }
            */
            //chat.Text = spel.geschiedenis;

            if (homeBool)
            {
                pea.Graphics.DrawImage(buttonsBitmap, homeRect, homeRect, units);
            }
            if (settingsBool)
            {
                pea.Graphics.DrawImage(buttonsBitmap, settingsRect, settingsRect, units);
            }
            if (helpBool)
            {
                pea.Graphics.DrawImage(buttonsBitmap, helpRect, helpRect, units);
            }
            if (laatsteKaartBool)
            {
                pea.Graphics.DrawImage(buttonsBitmap, laatsteKaartRect, laatsteKaartRect, units);
            }
            if (eindeBeurtBool)
            {
                pea.Graphics.DrawImage(buttonsBitmap, eindeBeurtRect, eindeBeurtRect, units);
            }
        }

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        protected void muisKlik(object sender, MouseEventArgs mea)
        {
            if (helpRect.Contains(mea.Location))
            {
                if (mea.Button == MouseButtons.Left)
                {
                    buttonSound();
                    spel.bezig = false;
                    Help help = new Help(this);
                }
                else
                {
                    //debug info
                    Kaart krt = null;
                    MessageBox.Show(krt.tekst);
                }
            }
            else if (settingsRect.Contains(mea.Location))
            {
                //het spel gaat gewoon door, dus niet handig
                //Instellingenscherm instellingenscherm = new Instellingenscherm(form);
            }
            else if (homeRect.Contains(mea.Location))
            {
                if (mea.Button == MouseButtons.Left)
                {
                    form.Show();
                    this.Close();
                }
                else
                {
                    Application.Exit();
                }
            }
            else if (laatsteKaartRect.Contains(mea.Location))
            {
                spel.laatsteKaart(true);
                Invalidate();
                Update();
            }
            else if (eindeBeurtRect.Contains(mea.Location))
            {
                if (spel.spelend == 0)
                {
                    spel.pakKaart();
                }
            }
            else if (spel.spelend == 0)
            {
                if (mea.X >= potPlek.X && mea.X <= potPlek.X + kaartBreedte && mea.Y >= potPlek.Y && mea.Y <= potPlek.Y + kaartHoogte)
                //pot
                {
                    if (spel.speciaal == 4)
                    {
                        spel.regelPakkenNu();
                        return;
                    }
                    else
                    {
                        spel.pakKaart();
                        Invalidate();
                        return;
                    }
                }
                foreach (Kaart kaart in spel.spelers[0].hand)
                {
                    if (mea.X >= kaart.X && mea.X <= kaart.X + kaartBreedte && mea.Y >= kaart.Y && mea.Y <= kaart.Y + kaartHoogte)
                    {
                        //kaart in hand van speler
                        if (spel.speelKaart(spel.spelers[0].hand.IndexOf(kaart)))
                        {
                            Invalidate();
                            return;
                        } 
                    }
                }
            } 
        }

        protected void muisWiel(object sender, MouseEventArgs mea)
        {
            if (spel.spelend == 0)
            {
                int index = 0;
                foreach (Kaart kaart in spel.spelers[0].hand)
                {
                    //kijkt of er op een kaart is geklikt
                    int deltaX = mea.X - kaart.X;
                    int deltaY = mea.Y - kaart.Y;
                    if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                    {
                        spel.speelKaart(index);
                        return;
                    }
                    index++;
                }
            }
        }

        protected void muisBeweeg(object sender, MouseEventArgs mea)
        {
            if (muisLaag)
            {
                //verplaatst de kaart waarop de speler de muis ingedrukt houdt
                bewegendeKaart.X = mea.X - laagX;
                bewegendeKaart.Y = mea.Y - laagY;
                Invalidate();
            }
            if (delta == 0)
            {
                if (mea.Y > Height - 10 - kaartHoogte)
                {
                    if (mea.X <= 50)
                    {
                        int breedte = spel.spelers[0].hand.Count * kaartBreedte - 10;
                        if (breedte > Width)
                        {
                            delta = 10 + 10 * breedte / Width;
                        }
                        schuifAnimatie = new Thread(schuiven);
                        schuifAnimatie.Start();
                    }
                    else if (mea.X >= Width - 50)
                    {
                        int breedte = spel.spelers[0].hand.Count * kaartBreedte - 10;
                        if (breedte > Width)
                        {
                            delta = -10 + -10 * breedte / Width;
                        }
                        schuifAnimatie = new Thread(schuiven);
                        schuifAnimatie.Start();
                    }
                }
            } 
            else
            {
                if (mea.Y < Height - 10 - kaartHoogte | (mea.X > 50 & mea.X < Width - 50))
                {
                    delta = 0;
                }
            }
        }

        protected void muisOmlaag(object sender, MouseEventArgs mea)
        {
            spel.checkNullKaart();
            int index = 0;
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                if (kaart != null)
                {
                    //kijkt of er op een kaart is geklikt
                    int deltaX = mea.X - kaart.X;
                    int deltaY = mea.Y - kaart.Y;
                    if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                    {
                        muisLaag = true;
                        bewegendeKaart = spel.spelers[0].hand[index];
                        spel.checkNullKaart();
                        spel.spelers[0].hand.RemoveAt(index);
                        //laagIndex = index;
                        laagX = deltaX;
                        laagY = deltaY;
                        zichtbaar = true;
                        return;
                        //als de muis op een kaart is, moet het nog duidelijk worden dat de kaart opgepakt is
                    }
                    index++;
                }
            }
            spel.checkNullKaart();
        }

        protected void muisOmhoog(object sender, MouseEventArgs mea)
        {
            spel.checkNullKaart();
            muisLaag = false;
            if (bewegendeKaart != null)
            {
                spel.spelers[0].hand.Add(bewegendeKaart);
                
                if (spel.spelend == 0 && spel.speelbaar(bewegendeKaart))
                {
                    spel.speelKaart(spel.spelers[0].hand.Count - 1);
                }

                bewegendeKaart = null;
                if (this.IsHandleCreated)
                {
                    Invoke(new Action(() => Invalidate()));
                    Invoke(new Action(() => Update()));
                }
            }
            spel.checkNullKaart();
        }

        protected void schuiven()
        {
            while (delta != 0)
            {
                //als er nog een stuk van de hand buiten beeld is
                if (delta > 0 && spel.spelers[0].hand[0].X < 50 || delta < 0 && spel.spelers[0].hand[spel.spelers[0].hand.Count - 1].X + kaartBreedte > Width - 50)
                {
                    foreach (Kaart kaart in spel.spelers[0].hand)
                    {
                        kaart.X += delta;
                    }
                    if (this.IsHandleCreated)
                    {
                        Invoke(new Action(() => Invalidate()));
                        Invoke(new Action(() => Update()));
                        Thread.Sleep(20);
                    }
                    else
                    {
                        delta = 0;
                    }
                }
                else
                {
                    delta = 0; 
                }
            }
            //MessageBox.Show("klaar");
           
            if (this.IsHandleCreated)
            {
                Invoke(new Action(() => Invalidate()));
                Invoke(new Action(() => Update()));
            }
            schuifAnimatie = null;
        }

        public void verplaatsen(Point p1, Point p2, bool _zichtbaar)
        {
            zichtbaar = _zichtbaar;
            int deltaX, deltaY, stappen, stap;
            deltaX = p2.X - p1.X;
            deltaY = p2.Y - p1.Y;
            stappen = 2 + (int)(Math.Sqrt(deltaX * deltaX + deltaY * deltaY) / 50);
            stap = 0;
            //Kaart kaart = spel.spelers[spel.spelend].hand[index];

            while (stap < stappen + 1)
            {
                //het is iets ingewikkelder vanwege de afronding van int, waarschijnlijk is het beter om float te gebruiken
                
                deltaX = stap * (p2.X - p1.X) / stappen;
                deltaY = stap * (p2.Y - p1.Y) / stappen;
                verplaatsendeKaart.X = p1.X + deltaX;
                verplaatsendeKaart.Y = p1.Y + deltaY;

                //deltaX = (verplaatsPunt2.X - verplaatsPunt1.X) / stappen;
                //deltaY = (verplaatsPunt2.Y - verplaatsPunt1.Y) / stappen;
                //kaart.X += deltaX;
                //kaart.Y += deltaY;

                stap++;

                if (this.IsHandleCreated)
                {
                    Invoke(new Action(() => Invalidate()));
                    Invoke(new Action(() => Update()));
                    Thread.Sleep(20);
                }
                else
                {
                    stap = stappen + 1;
                }
            }
            if (this.IsHandleCreated)
            {
                Invoke(new Action(() => Invalidate()));
                Invoke(new Action(() => Update()));
            }
        }

        public void verbergKleurknoppen()
        {
            harten.Hide();
            klaver.Hide();
            ruiten.Hide();
            schoppen.Hide();
        }

        public void toonKleurknoppen()
        {
            harten.Show();
            klaver.Show();
            ruiten.Show();
            schoppen.Show();
        }

        private void kaartSound()
        {
            Stream s = CyberPesten.Properties.Resources.playcard;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        protected void schoppen_Click(object sender, EventArgs e)
        {
            kaartSound();
            spel.speciaal = 3;
            spel.chat.nieuw(" en koos voor schoppen");
            spel.volgende();
            verbergKleurknoppen();
        }

        protected void ruiten_Click(object sender, EventArgs e)
        {
            kaartSound();
            spel.speciaal = 2;
            spel.chat.nieuw(" en koos voor ruiten");
            spel.volgende();
            verbergKleurknoppen();
        }

        protected void harten_Click(object sender, EventArgs e)
        {
            kaartSound();
            spel.speciaal = 0;
            spel.chat.nieuw(" en koos voor harten");
            spel.volgende();
            verbergKleurknoppen();
        }

        protected void klaver_Click(object sender, EventArgs e)
        {
            kaartSound();
            spel.speciaal = 1;
            spel.chat.nieuw(" en koos voor klaver");
            spel.volgende();
            verbergKleurknoppen();
        }

        protected void hover(object sender, MouseEventArgs mea)
        {
            if (homeRect.Contains(mea.Location))
            {
                homeBool = true;
            }
            else { homeBool = false; }
            if (settingsRect.Contains(mea.Location))
            {
                settingsBool = true;
            }
            else { settingsBool = false; }
            if (helpRect.Contains(mea.Location))
            {
                helpBool = true;
            }
            else { helpBool = false; }
            if (laatsteKaartRect.Contains(mea.Location))
            {
                laatsteKaartBool = true;
            }
            else { laatsteKaartBool = false; }
            if (eindeBeurtRect.Contains(mea.Location))
            {
                eindeBeurtBool = true;
            }
            else { eindeBeurtBool = false; }

            Invalidate();
        }
    }
}
