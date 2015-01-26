using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Media;
using System.IO;

namespace CyberPesten
{
    class Menu : Form
    {
        bool onlineHover, lokaalHover, settingsHover, helpHover, exitHover;
        public Instellingen instellingen;
        double verhoudingW,verhoudingH; //De grootte van de rectangles voor de knoppen worden allemaal gebaseerd op de verhoudingWen van de achtergrond
        Bitmap online, lokaal, settings, help, exit, menuLogo, achtergrond;
        Rectangle maat, onlineButton, lokaalButton, settingsButton, helpButton, exitButton;
        
        public Menu()
        {
            Text = "CyberPesten: Menu";
            maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Size = new Size(maat.X, maat.Y);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            achtergrond = new Bitmap(maat.Width, maat.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond"), maat);
            BackgroundImage = achtergrond;
            
            online = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Online"));
            lokaal = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Lokaal"));
            settings = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings"));
            help = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help"));
            exit = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Exit_button"));
            menuLogo = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_logo"));
            verhoudingW = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)1920;
            verhoudingH = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)1080;

            int rectangleWidth = (int)(85 * verhoudingW);
            int rectangleHeight = (int)(254 * verhoudingH);
            onlineButton = new Rectangle((int)(650 * verhoudingW), (int)(648 * verhoudingH), rectangleWidth, rectangleHeight);
            lokaalButton = new Rectangle((int)(830 * verhoudingW), (int)(648 * verhoudingH), rectangleWidth, rectangleHeight);
            settingsButton = new Rectangle((int)(1007 * verhoudingW), (int)(648 * verhoudingH), rectangleWidth, rectangleHeight);
            helpButton = new Rectangle((int)(1189 * verhoudingW), (int)(648 * verhoudingH), rectangleWidth, rectangleHeight);
            exitButton = new Rectangle((int)(1862 * verhoudingW), (int)(1022 * verhoudingH), (int)(42 * verhoudingW), (int)(42 * verhoudingH));


            this.MouseMove += this.hover;
            this.MouseClick += this.klik;

            this.Paint += this.teken;

            DoubleBuffered = true;

            instellingen = new Instellingen();
        }

        void teken(object sender, PaintEventArgs pea)
        {
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            pea.Graphics.DrawImage(BackgroundImage, 0, 0);
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            //selected
            if (onlineHover)
            {
                pea.Graphics.DrawImage(online, maat);
            }
            if (lokaalHover)
            {
                pea.Graphics.DrawImage(lokaal, maat);
            }
            if (settingsHover)
            {
                pea.Graphics.DrawImage(settings, maat);
            }
            if (helpHover)
            {
                pea.Graphics.DrawImage(help, maat);
            }
            if (exitHover)
            {
                pea.Graphics.DrawImage(exit, maat);
            }

            //buildTitle
            pea.Graphics.DrawImage(menuLogo, maat);
        }


        public void hover(object sender, MouseEventArgs mea)
        {
            if (onlineButton.Contains(mea.Location))
            {
                onlineHover = true;
            }
            else
            {
                onlineHover = false;
            }

            if (lokaalButton.Contains(mea.Location))
            {
                lokaalHover = true;
            }
            else
            {
                lokaalHover = false;
            }

            if (settingsButton.Contains(mea.Location))
            {
                settingsHover = true;

            }
            else
            {
                settingsHover = false;
            }

            if (helpButton.Contains(mea.Location))
            {
                helpHover = true;
            }
            else
            {
                helpHover = false;
            }
            if (exitButton.Contains(mea.Location))
            {
                exitHover = true;
            }
            else
            {
                exitHover = false;
            }

            Invalidate();
        }

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (onlineHover)
            {
                buttonSound();
                Form veld = new inlogScherm(this);
                veld.FormClosed += veld_FormClosed;
                this.Hide();
            }
            else if (lokaalHover)
            {
                buttonSound();
                Form veld = new Speelveld(this);
                veld.FormClosed += veld_FormClosed;
                this.Hide();
            }
            else if (helpHover)
            {
                buttonSound();
                Help help = new Help(this);
                help.FormClosed += veld_FormClosed;
                this.Hide();
            }
            else if (settingsHover)
            {
                buttonSound();
                if (mea.Button == MouseButtons.Right)
                {
                    InstellingenschermOud instellingenschermOud = new InstellingenschermOud(this);
                    instellingenschermOud.FormClosed += veld_FormClosed;
                    this.Hide();
                }
                else
                {
                    Instellingenscherm instellingenscherm = new Instellingenscherm(this);
                    instellingenscherm.FormClosed += veld_FormClosed;
                    this.Hide();
                }
            }
            else if (exitHover)
            {
                Application.Exit();
            }
        }

        public void veld_FormClosed(object o, EventArgs e)
        {
            if (this.Visible) { }
            else
            {
                Application.Exit();
            }
        }
    }
}
