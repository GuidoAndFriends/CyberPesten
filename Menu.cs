using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Menu : Form
    {
        int buttonWidth, buttonHeight;
        bool onlineHover, lokaalHover, settingsHover, helpHover, exitHover;
        public Instellingen instellingen;
        float verhouding; //De grootte van de plaatjes worden allemaal gebaseerd op de verhoudingen van de achtergrond
        Bitmap online, lokaal, settings, help, exit, menuLogo;
        Rectangle onlineButton, lokaalButton, settingsButton, helpButton, exitButton;
        
        public Menu()
        {
            Text = "CyberPesten: Menu";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            Rectangle maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Size = new Size(maat.X, maat.Y);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / BackgroundImage.Width;

            /*nog meer snelheidsverbeteringen: 
             * - PixelFormat Format32bppPArgb en schaling maar 1 keer
             * Bitmap achtergrond = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
             * Graphics.FromImage(plaatje).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond"), 0, 0, breedte, hoogte);
             * - Zoveel mogelijk op 1 niet transparante bitmap en die tekenen met CompositingMode SourceCopy
             * gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
             * gr.DrawImage(plaatje, 0, 0);
             * gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
             *
             */

            online = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Online"));
            lokaal = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Lokaal"));
            settings = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings"));
            help = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help"));
            exit = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Exit_button"));
            menuLogo = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_logo"));

            int rectangleWidth = (int)(85 * verhouding);
            int rectangleHeight = (int)(254 * verhouding);
            onlineButton = new Rectangle((int)(650 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            lokaalButton = new Rectangle((int)(830 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            settingsButton = new Rectangle((int)(1007 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            helpButton = new Rectangle((int)(1189 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            exitButton = new Rectangle((int)(1862 * verhouding), (int)(1022 * verhouding), (int)(42 * verhouding), (int)(42 * verhouding));

            this.MouseMove += this.hover;
            this.MouseClick += this.klik;

            this.Paint += this.teken;

            DoubleBuffered = true;

            instellingen = new Instellingen();
        }

        void teken(object sender, PaintEventArgs pea)
        {
            buttonWidth = (int)(online.Width * verhouding);
            buttonHeight = (int)(online.Height * verhouding);

            //selected
            if (onlineHover)
            {
                pea.Graphics.DrawImage(online, 0, 0, buttonWidth, buttonHeight);
            }
            if (lokaalHover)
            {
                pea.Graphics.DrawImage(lokaal, 0, 0, buttonWidth, buttonHeight);
            }
            if (settingsHover)
            {
                pea.Graphics.DrawImage(settings, 0, 0, buttonWidth, buttonHeight);
            }
            if (helpHover)
            {
                pea.Graphics.DrawImage(help, 0, 0, buttonWidth, buttonHeight);
            }
            if (exitHover)
            {
                pea.Graphics.DrawImage(exit, 0, 0, buttonWidth, buttonHeight);
            }

            //buildTitle
            int logoWidth = (int)(menuLogo.Width * verhouding);
            int logoHeight = (int)(menuLogo.Height * verhouding);
            pea.Graphics.DrawImage(menuLogo, 0, 0, logoWidth, logoHeight);
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

        private void klik(object sender, MouseEventArgs mea)
        {
            if (onlineHover)
            {
                Form veld = new inlogScherm(this);
                this.Hide();
            }
            else if (lokaalHover)
            {
                Form veld = new Speelveld(this, instellingen, false);
                this.Hide();
            }
            else if (helpHover)
            {
                Help help = new Help(this);
                this.Hide();
            }
            else if (settingsHover)
            {
                if (mea.Button == MouseButtons.Right)
                {
                    InstellingenschermOud instellingenschermOud = new InstellingenschermOud(this);
                    this.Hide();
                }
                else
                {
                    Instellingenscherm instellingenscherm = new Instellingenscherm(this);
                    this.Hide();
                }
            }
            else if (exitHover)
            {
                Application.Exit();
            }
        }
    }
}
