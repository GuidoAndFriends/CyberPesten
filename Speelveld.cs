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
    partial class LokaalSpel : Form
    {
        public Menu menu;
        public int muisX, delta, laagIndex, laagX, laagY, kaartBreedte, kaartHoogte, afstand;
        public Kaart bewegendeKaart;
        public Thread schuifAnimatie;
        public bool muisLaag;
        public Button laatsteKaartKnop;
        Graphics gr;
        Bitmap achtergrond;

        public List<Kaart> pot, stapel;
        public List<Speler> spelers;
        public int spelend, richting, speciaal, pakAantal;
        public string status;
        public bool laatsteKaartAangegeven = false;

        public LokaalSpel(bool online, int aantalSpelers, Menu m)
        {
            menu = m;

            achtergrond = (Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            BackgroundImage = achtergrond;
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

            laatsteKaartKnop = new Button();
            laatsteKaartKnop.Size = new Size(135, 90);
            laatsteKaartKnop.Location = new Point(865, 710);
            laatsteKaartKnop.Text = "Laatste kaart";
            laatsteKaartKnop.BackColor = Color.Red;
            laatsteKaartKnop.Font = new Font(FontFamily.GenericSansSerif, 20);
            laatsteKaartKnop.MouseClick += laatsteKaartKnop_Click;
            Controls.Add(laatsteKaartKnop);

            
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
            }
            else
            {
                Text = "CyberPesten: Lokaal spel";
            }
            
            muisLaag = false;
            kaartBreedte = 90;
            kaartHoogte = 135;
            afstand = 10;

            spelers = new List<Speler>();//moet aangeroepen worden met de Spel() functie
            stapel = new List<Kaart>();
            pot = new List<Kaart>();
            int kaartspellen = (aantalSpelers) / 4 + 1; //hoeveel kaartspellen gebruikt worden
            int startkaarten = 7; //hoeveel kaarten de spelers in het begin krijgen
            spelend = 0; //welke speler aan de beurt is
            richting = 1; //welke kant er op gespeeld word
            speciaal = -1; //of er een speciale kaart gespeeld is
            pakAantal = 0;

            //Spelers toevoegen
            spelers.Add(new Mens());
            for (int i = 1; i < aantalSpelers; i++)
            {
                spelers.Add(new Guido(this));
            }

            //Kaarten toevoegen
            for (int i = 0; i < kaartspellen; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 1; k < 14; k++)
                    {
                        pot.Add(new Kaart(j, k));
                    }
                }
                for (int j = 0; j < 2; j++)
                {
                    pot.Add(new Kaart(4));
                }
            }
            pot = schud(pot);

            foreach(Kaart kaart in pot)
            {
                Controls.Add(kaart);
            }


            //Kaarten delen
            for (int i = 0; i < startkaarten; i++)
            {
                foreach (Speler speler in spelers)
                {
                    verplaatsKaart(pot, speler.hand);
                }
            }

            spelers[0].maakXY();
            verplaatsKaart(pot, 0, stapel);
            Invalidate();

            this.Show();
            
        }

        private void laatsteKaartKnop_Click(object sender, EventArgs e)
        {
            laatsteKaart(1);
        }

        private void tekenHandler(object sender, PaintEventArgs pea)
        {
            gr = pea.Graphics; //!!!
            teken();
        }

        private void teken()
        {
            //if ( spelend != -1)
            //gr.FillRectangle(new TextureBrush(BackgroundImage), 0, 0, Width, Height);
            gr.FillRectangle(new TextureBrush(achtergrond), 0, 0, Width, Height);

            Bitmap plaatje = stapel[stapel.Count - 1].voorkant;
            gr.DrawImage(plaatje, 350, 300);
            plaatje = pot[pot.Count - 1].achterkant;
            gr.DrawImage(plaatje, 550, 300);

            foreach (Kaart kaart in spelers[0].hand)
            {
                gr.DrawImage(kaart.voorkant, kaart.X, kaart.Y);
            }

            for (int i = 1; i < spelers.Count; i++)
            {
                gr.DrawImage(spelers[i].blok, 10 + (290 + 40) * (i - 1), 10);
            }

            if (bewegendeKaart != null)
            {
                gr.DrawImage(bewegendeKaart.voorkant, bewegendeKaart.X, bewegendeKaart.Y);
            }

            gr.DrawString(status, new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, new Point(40, 450));
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (spelend == 0)
            {
                if (mea.X >= 550 && mea.X <= 550 + kaartBreedte && mea.Y >= 300 && mea.Y <= 300 + kaartHoogte)
                {
                    pakKaart();
                    volgende();
                    Invalidate();
                    return;
                }
                foreach (Kaart kaart in spelers[0].hand)
                {
                    if (mea.X >= kaart.X && mea.X <= kaart.X + kaartBreedte && mea.Y >= kaart.Y && mea.Y <= kaart.Y + kaartHoogte)
                    {
                        if (speelKaart(spelers[0].hand.IndexOf(kaart)))
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
            foreach (Kaart kaart in spelers[0].hand)
            {
                if (muisX >= kaart.X && muisX <= kaart.X + kaartBreedte && muisY >= kaart.Y && muisY <= kaart.Y + kaartHoogte)
                {
                    if (speelKaart(spelers[0].hand.IndexOf(kaart)))
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
                spelers[0].hand[laagIndex].X = mea.X - laagX;
                spelers[0].hand[laagIndex].Y = mea.Y - laagY;
                Invalidate();
            }
        }

        private void muisOmlaag(object sender, MouseEventArgs mea)
        {
            muisLaag = true;
            int index = 0;
            foreach (Kaart kaart in spelers[0].hand)
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
            int breedte = spelers[0].hand.Count * kaartBreedte - 10;
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
                if (delta > 0 && spelers[0].hand[0].X < 50 || delta < 0 && spelers[0].hand[spelers[0].hand.Count - 1].X + 100 > 1000 - 50)
                {
                    foreach (Kaart kaart in spelers[0].hand)
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
            Kaart kaart = spelers[spelend].hand[verplaatsIndex];

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
            //int stappen, stap;
            //float deltaX, deltaY;
            int deltaX, deltaY, stappen, stap;
            Point pOud;
            stappen = 20;
            stap = 0;
            Kaart kaart = spelers[spelend].hand[index];
            pOud = p1;

            while (stap < stappen)
            {
                //het is iets ingewikkelder vanwege de afronding van int, waarschijnlijk is het beter om float te gebruiken

                deltaX = stap * (p2.X - p1.X) / stappen;
                deltaY = stap * (p2.Y - p1.Y) / stappen;
                kaart.X = pOud.X + deltaX;
                kaart.Y = pOud.Y + deltaY;

                //deltaX = (p2.X - p1.X) / stappen;
                //deltaY = (p2.Y - p1.Y) / stappen;
                //kaart.X = (int)(kaart.X + deltaX);
                //kaart.Y = (int)(kaart.Y + deltaY);

                stap++;
                teken();
                //Invalidate();
                //Application.DoEvents();
                Thread.Sleep(1);
            }

        }

        private void afgesloten(object sender, EventArgs ea)
        {
            Application.Exit();
        }
    }
}
