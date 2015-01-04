﻿using System;
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
        int buttonWidth, buttonHeight, buttonWidthSmall, buttonHeightSmall;
        int lokaalX, onlineX, buttonY, helpX, instellingenX, buttonSmallY;
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

            instellingen = new Instellingen();
        }

        void klik(object sender, MouseEventArgs mea)
        {
            Rectangle lokaalButton = new Rectangle(lokaalX, buttonY, buttonWidth, buttonHeight);
            Rectangle onlineButton = new Rectangle(onlineX, buttonY, buttonWidth, buttonHeight);
            Rectangle helpButton = new Rectangle(helpX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
            Rectangle instellingenButton = new Rectangle(instellingenX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
            Rectangle exitButton = new Rectangle(Width - 100, Height - 100, 100, 100);

            if (lokaalButton.Contains(mea.Location))
            {
                Form veld = new Speelveld(this, instellingen, false);
                this.Hide();
            }
            else if (onlineButton.Contains(mea.Location))
            {
                Form veld = new inlogScherm();
                this.Hide();
            }
            else if (helpButton.Contains(mea.Location))
            {
                Help help = new Help(this);
                this.Hide();
            }
            else if (instellingenButton.Contains(mea.Location))
            {
                Instellingenscherm instellingenscherm = new Instellingenscherm(this);
                this.Hide();
            }
            else if (exitButton.Contains(mea.Location))
            {
                Application.Exit();
            }
        }

        void buildMenuGraphics(Object o, PaintEventArgs pea)
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
            Bitmap exit = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Home_button"));
            //design van exit knop moet nog gemaakt worden

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
            pea.Graphics.DrawImage(exit, new Point(Width - 100, Height - 100));
        }
    }
}
