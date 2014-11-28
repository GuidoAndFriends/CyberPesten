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
        public Thread animatie;
        public bool muisLaag;

        public Speelveld(Menu m)
        {
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);
            DoubleBuffered = true;

            Paint += teken;
            MouseClick += klik;
            MouseMove += beweeg;
            MouseDown += muisOmlaag;
            MouseUp += muisOmhoog;
            MouseLeave += muisWeg;
            MouseEnter += muisTerug;
            Scroll += scroll;

            int aantalAI = 3;
            spel = new Spel(this, aantalAI);
            menu = m;
            muisLaag = false;
            kaartBreedte = 90;
            kaartHoogte = 135;
            afstand = 10;

            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            gr.FillRectangle(new TextureBrush(BackgroundImage), 0, 0, Width, Height);
            //gr.FillRectangle(Brushes.DarkGreen, 0, 0, Width, Height);

            Bitmap plaatje = spel.stapel[spel.stapel.Count - 1].voorkant;
            gr.DrawImage(plaatje, 350, 300);
            plaatje = spel.stapel[spel.stapel.Count - 1].achterkant;
            gr.DrawImage(plaatje, 550, 300);

            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                gr.DrawImage(kaart.voorkant, kaart.X, kaart.Y);
            }

            for (int i = 1; i < spel.spelers.Count; i++)
            {
                gr.DrawImage(spel.spelers[i].blok, 10 + (290 + 40) * (i - 1), 10);
            }

        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (spel.spelend == 0)
            {
                if (mea.X >= 550 && mea.X <= 550 + kaartBreedte && mea.Y >= 300 && mea.Y <= 300 + kaartHoogte)
                {
                    spel.pakKaart();
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

        /*
        public void klikKaart(object sender, MouseEventArgs mea)
        {
            //Controleren op welke kaart er is geklikt en die spelen als dat mag
            PictureBox a = (PictureBox)sender;
            int index = a.TabIndex;
            bool kon = spel.speelKaart(index);
            if (kon)
            {
                System.Diagnostics.Debug.WriteLine("Kaart met index " + index + " is correct gespeeld.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Kaart met index " + index + " is foutief gespeeld.");
            }
            //Control moet verwijderd worden
            a.Hide();
            Invalidate();
        }
         */

        private void scroll(object sender, EventArgs ea)
        {
            //Kaart spelen
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
                animatie = new Thread(schuiven);
                animatie.Start();
            }
            
        }

        private void muisTerug(object sender, EventArgs ea)
        {
            animatie = null;
        }

        private void schuiven()
        {
            while (animatie != null)
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

        private void afsluiten(object sender, FormClosedEventArgs fcea)
        {

        }
    }
}
