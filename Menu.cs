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
        private Form veld;
        int buttonWidth, buttonHeight, buttonWidthSmall, buttonHeightSmall;
        int lokaalX, onlineX, buttonY, helpX, instellingenX, buttonSmallY;
        int aantalSpelers;
        public Instellingen instellingen;

        public Menu()
        {
            Text = "CyberPesten: Menu";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_achtergrond");
            Rectangle maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Size = new Size(maat.X, maat.Y);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            this.Paint += this.buildMenuGraphics;
            this.MouseClick += this.klik;
            DoubleBuffered = true;

            aantalSpelers = 4;

            this.FormClosed += Menu_FormClosed;
        }

        void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            //Controls.Add(aantal);
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            Rectangle lokaalButton = new Rectangle(lokaalX, buttonY, buttonWidth, buttonHeight);
            Rectangle onlineButton = new Rectangle(onlineX, buttonY, buttonWidth, buttonHeight);
            Rectangle helpButton = new Rectangle(helpX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
            Rectangle instellingenButton = new Rectangle(instellingenX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);

            if (lokaalButton.Contains(mea.Location))
            {
                veld = new Speelveld(false, aantalSpelers, this, instellingen);
                this.Hide();
            }
            if (onlineButton.Contains(mea.Location))
            {
                veld = new inlogScherm();
                this.Hide();
            }
            if (helpButton.Contains(mea.Location))
            {
                Help help = new Help(this);
                this.Hide();
            }
            if (instellingenButton.Contains(mea.Location))
            {
                Instellingenscherm isntellingenscherm = new Instellingenscherm(this);
                this.Hide();
            }
        }

        void veld_FormClosed(object sender, FormClosedEventArgs e)
        {
            veld.Dispose();
            this.Show();

            //DEBUG:
            Application.Exit();
        }

        public void buildMenuGraphics(Object o, PaintEventArgs pea)
        {
            Bitmap menuLogo = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Menu_logo"));
            int logoWidth = Screen.PrimaryScreen.Bounds.Width * 3/5;
            int logoHeight = logoWidth * menuLogo.Height / menuLogo.Width;
            int logoCenter = Screen.PrimaryScreen.Bounds.Width / 2 - logoWidth / 2;
            pea.Graphics.DrawImage(menuLogo, logoCenter, Screen.PrimaryScreen.Bounds.Height / 9, logoWidth, logoHeight);

            Bitmap lokaal = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Lokaal"));
            Bitmap online = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Online"));
            Bitmap help = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help"));
            Bitmap players = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Players"));
            buttonWidth = Screen.PrimaryScreen.Bounds.Width / 8;
            buttonHeight = buttonWidth * lokaal.Height / lokaal.Width;
            buttonWidthSmall = Screen.PrimaryScreen.Bounds.Width / 10;
            buttonHeightSmall = buttonWidthSmall * help.Height / help.Width;
            lokaalX = Screen.PrimaryScreen.Bounds.Width / 16 * 5;
            onlineX = Screen.PrimaryScreen.Bounds.Width / 16 * 9;
            buttonY = Screen.PrimaryScreen.Bounds.Height / 2;
            helpX = Screen.PrimaryScreen.Bounds.Width / 20 * 15;
            instellingenX = Screen.PrimaryScreen.Bounds.Width / 20 * 3;
            buttonSmallY = Screen.PrimaryScreen.Bounds.Height / 7 * 4;
   
            pea.Graphics.DrawImage(players, instellingenX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
            pea.Graphics.DrawImage(lokaal, lokaalX, buttonY, buttonWidth, buttonHeight);
            pea.Graphics.DrawImage(online, onlineX, buttonY, buttonWidth, buttonHeight);
            pea.Graphics.DrawImage(help, helpX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
        }
    }
}
