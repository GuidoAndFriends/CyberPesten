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
        public int muisX, delta, laagIndex, laagX, laagY, kaartBreedte, kaartHoogte, afstand;
        public Kaart bewegendeKaart;
        
        public Thread schuifAnimatie;
        public bool muisLaag;

        public Button laatsteKaart;

        

        public Speelveld(bool online, int aantalSpelers, Menu m)
        {
            menu = m;
            muisLaag = false;
            kaartBreedte = 90;
            kaartHoogte = 135;
            afstand = 10;

            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond");
            Size = menu.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            /*
            Button help = new Button();
            ... als in Menu.cs ...
            Controls.Add(help);
            //Er zal vanalles over de helpknop heen getekend worden
             */

            laatsteKaart = new Button();
            laatsteKaart.Size = new Size(135, 90);
            laatsteKaart.Location = new Point(Width / 2 + 500, Height / 2 - kaartBreedte / 2);
            laatsteKaart.Text = "Laatste kaart";
            laatsteKaart.BackColor = Color.OrangeRed;
            laatsteKaart.Font = new Font(FontFamily.GenericSansSerif, 20);
            laatsteKaart.MouseClick += laatsteKaart_Click;
            Controls.Add(laatsteKaart);

            Paint += teken;
            MouseClick += klik;
            MouseMove += beweeg;
            MouseDown += muisOmlaag;
            MouseUp += muisOmhoog;
            MouseLeave += muisWeg;
            MouseEnter += muisTerug;
            Scroll += scroll;
            FormClosed += afgesloten;

            if (online)
            {
                Text = "CyberPesten: Online spel";
                spel = new OnlineSpel(this, aantalSpelers);
            }
            else
            {
                Text = "CyberPesten: Lokaal spel";
                spel = new LokaalSpel(this, aantalSpelers);
            }
            
            

            this.Show();
        }

        private void laatsteKaart_Click(object sender, EventArgs e)
        {
            spel.laatsteKaart(1);
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            gr.FillRectangle(new TextureBrush(BackgroundImage), 0, 0, Width, Height);
            //gr.FillRectangle(Brushes.DarkGreen, 0, 0, Width, Height);

            Bitmap plaatje = spel.stapel[spel.stapel.Count - 1].voorkant;
            gr.DrawImage(plaatje, Width / 2 - 50 - kaartBreedte, Height / 2 - kaartHoogte / 2);
            plaatje = spel.pot[spel.pot.Count - 1].achterkant;
            gr.DrawImage(plaatje, Width / 2 + 50, Height / 2 - kaartHoogte / 2);

            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                gr.DrawImage(kaart.voorkant, kaart.X, kaart.Y);
            }

            int breedte = (spel.spelers.Count - 1) * (290 + 40);
            int over = (breedte - 20) / (spel.spelers.Count - 2);

            for (int i = 1; i < spel.spelers.Count; i++)
            {
                gr.DrawImage(spel.spelers[i].blok, 10 + (290 + over) * (i - 1), 10);
            }

            if (bewegendeKaart != null)
            {
                gr.DrawImage(bewegendeKaart.voorkant, bewegendeKaart.X, bewegendeKaart.Y);
            }

            gr.DrawString(spel.status, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, new Point(40, 450));
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (spel.spelend == 0)
            {
                if (mea.X >= 550 && mea.X <= 550 + kaartBreedte && mea.Y >= 300 && mea.Y <= 300 + kaartHoogte)
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
                        if (spel.speelKaart(spel.spelers[0].hand.IndexOf(kaart)))
                        {
                            Invalidate();
                            return;
                        } 
                    }
                }
            } 
        }

        private void scroll(object sender, EventArgs ea)
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

        private void beweeg(object sender, MouseEventArgs mea)
        {
            muisX = mea.X;
            if (muisLaag)
            {
                spel.spelers[0].hand[laagIndex].X = mea.X - laagX;
                spel.spelers[0].hand[laagIndex].Y = mea.Y - laagY;
                Invalidate();
            }
        }

        private void muisOmlaag(object sender, MouseEventArgs mea)
        {
            muisLaag = true;
            int index = 0;
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                int deltaX = mea.X - kaart.X;
                int deltaY = mea.Y - kaart.Y;
                if (deltaX >= 0 && deltaX <= 100 && deltaY >= 0 && deltaY <= 140)
                {
                    laagIndex = index;
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
        }

        private void muisWeg(object sender, EventArgs ea)
        {
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
                if (delta > 0 && spel.spelers[0].hand[0].X < 50 || delta < 0 && spel.spelers[0].hand[spel.spelers[0].hand.Count - 1].X + 100 > 1000 - 50)
                {
                    foreach (Kaart kaart in spel.spelers[0].hand)
                    {
                        kaart.X += delta;
                    }
                    Invalidate();
                    Thread.Sleep(25);
                }
            }
        }

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
                Application.DoEvents();
                Thread.Sleep(1);
            }

            verplaatsAnimatie = null;
        }
        */

        public void verplaatsen2(Point p1, Point p2, int index)
        {
            int deltaX, deltaY, stappen, stap;
            Point pOud;
            stappen = 20;
            stap = 0;
            Kaart kaart = spel.spelers[spel.spelend].hand[index];
            pOud = p1;

            while (stap < stappen)
            {
                //het is iets ingewikkelder vanwege de afronding van int, waarschijnlijk is het beter om float te gebruiken

                deltaX = stap * (p2.X - p1.X) / stappen;
                deltaY = stap * (p2.Y - p1.Y) / stappen;
                kaart.X = pOud.X + deltaX;
                kaart.Y = pOud.Y + deltaY;

                //deltaX = (verplaatsPunt2.X - verplaatsPunt1.X) / stappen;
                //deltaY = (verplaatsPunt2.Y - verplaatsPunt1.Y) / stappen;
                //kaart.X += deltaX;
                //kaart.Y += deltaY;

                stap++;
                Invalidate();
                Application.DoEvents();
                Thread.Sleep(1);
            }
        }

        private void afgesloten(object sender, EventArgs ea)
        {
            Application.Exit();
        }
    }
}
