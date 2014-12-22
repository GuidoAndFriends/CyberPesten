using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CyberPesten
{
    class Speelveld : Form
    {
        public Menu menu;
        public Spel spel;
        public int kaartBreedte, kaartHoogte, afstand;
        public Point stapelPlek, potPlek;

        public bool tekenHelemaal;

        //Voor het verslepen van een kaart
        public bool muisLaag;
        public int laagIndex, laagX, laagY;

        //Voor het verschuiven van de hand van de speler
        public Thread schuifAnimatie;
        public int muisX, delta;

        //Voor het verplaatsen van een kaart die gespeeld of gepakt wordt
        public Kaart bewegendeKaart;
        
        //public Button laatsteKaart, help;

        //Voor de buttons
        Rectangle helpButton;
        //Rectangle settingsButton;
        Rectangle homeButton;
        Rectangle laatsteKaartButton;

        public Speelveld(bool online, int aantalSpelers, Menu m)
        {
            menu = m;
            kaartBreedte = 110;
            kaartHoogte = 153;
            afstand = 10;
            
            muisLaag = false;
            tekenHelemaal = true;

            Size = menu.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            Bitmap achtergrond = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond"), 0, 0);
            BackgroundImage = achtergrond;

            stapelPlek = new Point(Width / 2 - 50 - kaartBreedte, Height / 2 - kaartHoogte / 2);
            potPlek = new Point(Width / 2 + 50, Height / 2 - kaartHoogte / 2);

            /*help = new Button();
            help.Size = new Size(kaartHoogte, kaartBreedte);
            help.Location = new Point(Width / 2 - 500 - kaartBreedte, Height / 2 - kaartBreedte / 2);
            help.Text = "Help";
            help.Font = new Font(FontFamily.GenericSansSerif, 20);
            help.MouseClick += helpKlik;
            Controls.Add(help);
            
            laatsteKaart = new Button();
            laatsteKaart.Size = new Size(kaartHoogte, kaartBreedte);
            laatsteKaart.Location = new Point(Width / 2 + 500, Height / 2 - kaartBreedte / 2);
            laatsteKaart.Text = "Laatste kaart";
            laatsteKaart.BackColor = Color.OrangeRed;
            laatsteKaart.Font = new Font(FontFamily.GenericSansSerif, 20);
            laatsteKaart.MouseClick += laatsteKaartKlick;
            Controls.Add(laatsteKaart);*/

            Paint += teken;
            MouseClick += muisKlik;
            MouseClick += buttonKlik;
            MouseMove += muisBeweeg;
            MouseDown += muisOmlaag;
            MouseUp += muisOmhoog;
            MouseLeave += muisWeg;
            MouseEnter += muisTerug;
            Scroll += scroll;

            if (online)
            {
                Text = "CyberPesten: Online spel";
                spel = new OnlineSpel(this, aantalSpelers, true);
            }
            else
            {
                Text = "CyberPesten: Lokaal spel";
                spel = new LokaalSpel(this, aantalSpelers, true);
            }

            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;

            if (tekenHelemaal)
            {
                //achtergrond
                gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                gr.DrawImage(BackgroundImage, 0, 0);
                gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                //stapel
                Bitmap plaatje = spel.stapel[spel.stapel.Count - 1].voorkant;
                gr.DrawImage(plaatje, stapelPlek);

                //pot
                plaatje = spel.pot[spel.pot.Count - 1].achterkant;
                gr.DrawImage(plaatje, potPlek);

                //hand van speler
                foreach (Kaart kaart in spel.spelers[0].hand)
                {
                    gr.DrawImage(kaart.voorkant, kaart.X, kaart.Y - 20);
                }

                //blokken van AI
                int breedte = (spel.spelers.Count - 1) * 370 - 20;
                int tussenruimte = (Width - breedte - 20) / (spel.spelers.Count - 2);
                for (int i = 1; i < spel.spelers.Count; i++)
                {
                    gr.DrawImage(spel.spelers[i].blok, 10 + (370 + tussenruimte) * (i - 1), 10);
                }

                //een eventuele bewegende kaart
                if (bewegendeKaart != null)
                {
                    gr.DrawImage(bewegendeKaart.voorkant, bewegendeKaart.X, bewegendeKaart.Y);
                }

                //status van het spel
                gr.DrawString(spel.status, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, new Point(40, 450));

                //Buttons
                Image HelpButton = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help_button");
                Image SettingsButton = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings_button");
                Image HomeButton = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Home_button");

                int buttonWidth = HelpButton.Size.Width;

                gr.DrawImage(HelpButton, 25, this.Height - 25 - HelpButton.Size.Height, buttonWidth, buttonWidth);
                gr.DrawImage(SettingsButton, 50 + buttonWidth, this.Height - 25 - buttonWidth, buttonWidth, buttonWidth);
                gr.DrawImage(HomeButton, 75 + 2 * buttonWidth, this.Height - 25 - buttonWidth, buttonWidth, buttonWidth);

                helpButton = new Rectangle(25, this.Height - 25 - buttonWidth, buttonWidth, buttonWidth);
                homeButton = new Rectangle(75 + 2 * buttonWidth, this.Height - 25 - buttonWidth, buttonWidth, buttonWidth);

                // Laatste kaart button
                Image LaatsteKaartButton = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Laatste_kaart");
                gr.DrawImage(LaatsteKaartButton, this.Width - 750, this.Height / 2 - LaatsteKaartButton.Width / 2 + 5, LaatsteKaartButton.Width, LaatsteKaartButton.Width);
                laatsteKaartButton = new Rectangle(this.Width - 750, this.Height / 2 - LaatsteKaartButton.Width / 2 + 5, LaatsteKaartButton.Width, LaatsteKaartButton.Width);
            }
        }

        private void buttonKlik(object sender, MouseEventArgs mea) //Regelt wat er gebeurt als er op de buttons wordt geklikt
        {
            if (helpButton.Contains(mea.Location))
            {
                Help help = new Help();
            }

            if (homeButton.Contains(mea.Location))
            {
                //this.Close();
                Application.Exit();
                //this.Dispose();
                //Terug naar menu
            }
            if (laatsteKaartButton.Contains(mea.Location))
            {
                spel.laatsteKaart(1);
            }
        }

        private void muisKlik(object sender, MouseEventArgs mea)
        {
            if (spel.spelend == 0)
            {
                if (mea.X >= potPlek.X && mea.X <= potPlek.X + kaartBreedte && mea.Y >= potPlek.Y && mea.Y <= potPlek.Y + kaartHoogte)
                //pot
                {
                    spel.pakKaart();
                    spel.volgende();
                    Invalidate();
                    return;
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

        /*
        private void laatsteKaartKlick(object sender, EventArgs e)
        {
            spel.laatsteKaart(1);
        }*/

        private void scroll(object sender, EventArgs ea)
        //zal een kaart spelen als er gescrolld wordt
        {
            /*
            int muisX = MousePosition.X;
            int muisY = MousePosition.Y;
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                if (muisX >= kaart.X && muisX <= kaart.X + kaartBreedte && muisY >= kaart.Y && muisY <= kaart.Y + kaartHoogte)
                {
                    if (spel.speelKaart(spel.spelers[0].hand.IndexOf(kaart)))
                    {
                        Invalidate();
                        return;
                    }
                }
            }
             */
        }

        private void muisBeweeg(object sender, MouseEventArgs mea)
        {
            //werkt de x van de muis bij om eventueel de hand van de speler te laten bewegen
            muisX = mea.X;
            if (muisLaag)
            {
                //verplaatst de kaart waarop de speler de muis ingedrukt houdt
                bewegendeKaart.X = mea.X - laagX;
                bewegendeKaart.Y = mea.Y - laagY;
                Invalidate();
            }
        }

        private void muisOmlaag(object sender, MouseEventArgs mea)
        { 
            int index = 0;
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                //kijkt of er op een kaart is geklikt
                int deltaX = mea.X - kaart.X;
                int deltaY = mea.Y - kaart.Y;
                if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                {
                    muisLaag = true;
                    bewegendeKaart = spel.spelers[0].hand[index];
                    spel.spelers[0].hand.RemoveAt(index);
                    //laagIndex = index;
                    laagX = deltaX;
                    laagY = deltaY;
                    return;
                    //als de muis op een kaart is, moet het nog duidelijk worden dat de kaart opgepakt is
                }
                index++;
            }
        }

        private void muisOmhoog(object sender, MouseEventArgs mea)
        {
            muisLaag = false;
            if (spel.spelend == 0 && spel.speelbaar(bewegendeKaart))
            {
                spel.spelers[0].hand.Add(bewegendeKaart);
                spel.speelKaart(spel.spelers[0].hand.Count - 1);
                bewegendeKaart = null;
            }
            else
            {
                spel.spelers[0].hand.Add(bewegendeKaart);
                bewegendeKaart = null;
            }
        }

        private void muisWeg(object sender, EventArgs ea)
        {
            //verschuift de hand van de speler als de muis buiten beeld gaat
            int breedte = spel.spelers[0].hand.Count * kaartBreedte - 10;
            if (breedte > Width)
            {
                delta = 10 + 10 * breedte / Width;
                if (muisX > 500)
                {
                    delta *= -1;
                }
                schuifAnimatie = new Thread(schuiven);
                schuifAnimatie.Start();
            }
        }

        private void muisTerug(object sender, EventArgs ea)
        {
            schuifAnimatie = null;
        }

        private void schuiven()
        {
            while (schuifAnimatie != null)
            {
                //als er nog een stuk van de hand buiten beeld is
                if (delta > 0 && spel.spelers[0].hand[0].X < 50 || delta < 0 && spel.spelers[0].hand[spel.spelers[0].hand.Count - 1].X + 100 > 1000 - 50)
                {
                    foreach (Kaart kaart in spel.spelers[0].hand)
                    {
                        kaart.X += delta;
                    }
                    Invalidate();
                    Update();
                    Thread.Sleep(25);
                }
            }
        }

        //hieronder pogingen tot animatie van een kaart die gespeeld of gepakt wordt

        /*
        public int verplaatsIndex, verplaatsStap;
        public Thread verplaatsAnimatie;
        public Point verplaatsPuntOud, verplaatsPunt1, verplaatsPunt2;
        
        public void verplaatsen()
        {
            int deltaX, deltaY, stappen;
            stappen = 50;
            Kaart kaart = spel.spelers[spel.spelend].hand[verplaatsIndex];

            while (verplaatsStap < stappen)
            {
                //het is iets ingewikkelder vanwege de afronding van int, waarschijnlijk is het beter om float te gebruiken
                
                deltaX = verplaatsStap * (verplaatsPunt2.X - verplaatsPunt1.X) / stappen;
                deltaY = verplaatsStap * (verplaatsPunt2.Y - verplaatsPunt1.Y) / stappen;
                kaart.X = verplaatsPuntOud.X + deltaX;
                kaart.Y = verplaatsPuntOud.Y + deltaY;
                
                //deltaX = (verplaatsPunt2.X - verplaatsPunt1.X) / stappen;
                //deltaY = (verplaatsPunt2.Y - verplaatsPunt1.Y) / stappen;
                //kaart.X += deltaX;
                //kaart.Y += deltaY;

                verplaatsStap++;
                Invalidate();
                Update
                Thread.Sleep(25);
            }

            verplaatsAnimatie = null;
        }
        */

        public void verplaatsen2(Point p1, Point p2, int index)
        {
            int deltaX, deltaY, stappen, stap;
            stappen = 20;
            stap = 0;
            Kaart kaart = spel.spelers[spel.spelend].hand[index];

            while (stap < stappen + 1)
            {
                //het is iets ingewikkelder vanwege de afronding van int, waarschijnlijk is het beter om float te gebruiken

                deltaX = stap * (p2.X - p1.X) / stappen;
                deltaY = stap * (p2.Y - p1.Y) / stappen;
                kaart.X = p1.X + deltaX;
                kaart.Y = p1.Y + deltaY;

                //deltaX = (verplaatsPunt2.X - verplaatsPunt1.X) / stappen;
                //deltaY = (verplaatsPunt2.Y - verplaatsPunt1.Y) / stappen;
                //kaart.X += deltaX;
                //kaart.Y += deltaY;

                stap++;
                Invoke(new Action(() => Invalidate()));
                Invoke(new Action(() => Update()));
                Thread.Sleep(20);
            }
        }
    }
}
