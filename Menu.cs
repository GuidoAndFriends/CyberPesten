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
        private Form veld;
        private NumericUpDown aantal;
        int buttonWidth;
        int buttonHeight;
        int buttonWidthSmall;
        int buttonHeightSmall;
        int lokaalX;
        int onlineX;
        int buttonY;
        int helpX;
        int playersX;
        int buttonSmallY;

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

            aantal = new NumericUpDown();
            aantal.Size = new Size(200, 50);
            aantal.Location = new Point(200, 620);
            aantal.Value = 4;
            Controls.Add(aantal);

            Button help = new Button();
            help.Size = new Size(135, 90);
            help.Location = new Point(865, 710);
            help.Text = "Help";
            help.Font = new Font(FontFamily.GenericSansSerif, 20);
            //help.MouseClick += helpKlik;
            Controls.Add(help);

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
            Rectangle playersButton = new Rectangle(playersX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);

            if (lokaalButton.Contains(mea.Location))
            {
                veld = new Speelveld(false, (int)aantal.Value, this);
                this.Hide();
            }
            if (onlineButton.Contains(mea.Location))
            {
                veld = new openSpellenScherm();
                this.Hide();
            }
            if (helpButton.Contains(mea.Location))
            {
                Help help = new Help();
            }
        }

        void veld_FormClosed(object sender, FormClosedEventArgs e)
        {
            Online.appendFile("D:\test.txt", e.CloseReason + "\n");
            veld.Dispose();
            this.Show();

            //DEBUG:
            Application.Exit();
        }

        private void onlineKlik(object sender, MouseEventArgs mea)//Volgens mij kan deze weg - Guido
        {
            veld = new inlogScherm();
            veld.FormClosed += veld_FormClosed;
            this.Hide();
            Rectangle lokaalButton = new Rectangle(lokaalX, buttonY, buttonWidth, buttonHeight);
            Rectangle onlineButton = new Rectangle(onlineX, buttonY, buttonWidth, buttonHeight);
            Rectangle helpButton = new Rectangle(helpX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
            Rectangle playersButton = new Rectangle(playersX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);

            if (lokaalButton.Contains(mea.Location))
            {
                veld = new Speelveld(false, (int)aantal.Value, this);
                this.Hide();
            }
            if (onlineButton.Contains(mea.Location))
            {
                //veld = new Speelveld(true, aantal.Value, this);
                this.Hide();
            }
            if (helpButton.Contains(mea.Location))
            {
                Help help = new Help();
            }
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
            playersX = Screen.PrimaryScreen.Bounds.Width / 20 * 3;
            buttonSmallY = Screen.PrimaryScreen.Bounds.Height / 7 * 4;
   
            pea.Graphics.DrawImage(players, playersX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
            pea.Graphics.DrawImage(lokaal, lokaalX, buttonY, buttonWidth, buttonHeight);
            pea.Graphics.DrawImage(online, onlineX, buttonY, buttonWidth, buttonHeight);
            pea.Graphics.DrawImage(help, helpX, buttonSmallY, buttonWidthSmall, buttonHeightSmall);
        }
    }
}
