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
        Bitmap online, lokaal, settings, help, exit;
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

            online = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Online"));
            lokaal = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Lokaal"));
            settings = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings"));
            help = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help"));
            exit = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Exit_button"));

            int rectangleWidth = (int)(85 * verhouding);
            int rectangleHeight = (int)(254 * verhouding);
            onlineButton = new Rectangle((int)(650 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            lokaalButton = new Rectangle((int)(830 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            settingsButton = new Rectangle((int)(1007 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            helpButton = new Rectangle((int)(1189 * verhouding), (int)(648 * verhouding), rectangleWidth, rectangleHeight);
            exitButton = new Rectangle((int)(1862 * verhouding), (int)(1022 * verhouding), (int)(42 * verhouding), (int)(42 * verhouding));

            this.MouseMove += this.hover;
            this.MouseClick += this.klik;

            //Ik heb de plaatjes in verschillende methodes gezet zodat de nieuw getekende plaatjes niet de oude gingen overlappen, weet niet of dat handig is?
            this.Paint += this.buildMenuGraphics;
            this.Paint += this.selected;
            this.Paint += this.buildTitle;

            DoubleBuffered = true;

            instellingen = new Instellingen();
        }

        public void selected(object sender, PaintEventArgs pea)
        {
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
            Invalidate();

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
                Form veld = new inlogScherm();
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
                Instellingenscherm instellingenscherm = new Instellingenscherm(this);
                this.Hide();
            }
            else if (exitHover)
            {
                Application.Exit();
            }
        }

        void buildMenuGraphics(Object o, PaintEventArgs pea)
        {
            Bitmap fadedButtons = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Faded_buttons"));

            buttonWidth = (int)(fadedButtons.Width * verhouding);
            buttonHeight = (int)(fadedButtons.Height * verhouding);

            pea.Graphics.DrawImage(fadedButtons, 0, 0, fadedButtons.Width * verhouding, fadedButtons.Height * verhouding);
        }

        public void buildTitle(Object o, PaintEventArgs pea)
        {
            Bitmap menuLogo = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_logo"));
            int logoWidth = (int)(menuLogo.Width * verhouding);
            int logoHeight = (int)(menuLogo.Height * verhouding);
            pea.Graphics.DrawImage(menuLogo, 0, 0, logoWidth, logoHeight);
        }
    }
}
