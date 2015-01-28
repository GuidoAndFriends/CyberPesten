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
    class OnlineSpeelveld : Speelveld
    {
        public OnlineSpeelveld(Form form)
        {
            Instellingen instellingen = new Instellingen();
            this.form = form;
            kaartBreedte = 110;
            kaartHoogte = 153;
            afstand = 10;
            maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            muisLaag = false;

            Size = form.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            Bitmap achtergrond = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond"), maat);
            BackgroundImage = achtergrond;

            achterkant = new Bitmap(110, 153, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            string achterkantDesign;
            if (instellingen.achterkant == 0)
            {
                achterkantDesign = "Back_design_1";
            }
            else
            {
                achterkantDesign = "Back_design_2";
            }
            Graphics.FromImage(achterkant).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject(achterkantDesign), 0, 0, kaartBreedte, kaartHoogte);

            stapelPlek = new Point(Width / 2 - 50 - kaartBreedte, Height / 2 - kaartHoogte / 2);
            potPlek = new Point(Width / 2 + 50, Height / 2 - kaartHoogte / 2);
            laatsteKaartBrush = Brushes.Red;

            Paint += teken;
            MouseClick += muisKlik;
            MouseMove += muisBeweeg;
            MouseDown += muisOmlaag;
            MouseUp += muisOmhoog;
            MouseWheel += muisWiel;
            MouseMove += hover;

            startSpel(instellingen);

            buttonsBitmap = ((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Speelveld_buttons"));
            verhoudingW = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)1920;
            verhoudingH = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)1080;

            //Buttons
            int bigButtonWidth = (int)(verhoudingW * 84);
            int smallButtonWidth = (int)(verhoudingW * 52);
            int buttonRij = (int)(verhoudingW * 1835);

            homeRect = new Rectangle(buttonRij, (int)(verhoudingH * 453), smallButtonWidth, smallButtonWidth);
            settingsRect = new Rectangle(buttonRij, (int)(verhoudingH * 514), smallButtonWidth, smallButtonWidth);
            helpRect = new Rectangle(buttonRij, (int)(verhoudingH * 575), smallButtonWidth, smallButtonWidth);
            laatsteKaartRect = new Rectangle((int)(verhoudingW * 1309), (int)(verhoudingH * 498), bigButtonWidth, bigButtonWidth);
            eindeBeurtRect = new Rectangle((int)(verhoudingW * 1187), (int)(verhoudingH * 498), bigButtonWidth, bigButtonWidth);

            klaver = new Button();
            klaver.Click += klaver_Click;
            klaver.Location = new Point(50, 250);
            klaver.Size = new Size(200, 50);
            klaver.Text = "Maak er Klaver van";

            harten = new Button();
            harten.Click += harten_Click;
            harten.Location = new Point(300, 250);
            harten.Size = new Size(200, 50);
            harten.Text = "Maak er Harten van";

            ruiten = new Button();
            ruiten.Click += ruiten_Click;
            ruiten.Location = new Point(50, 350);
            ruiten.Size = new Size(200, 50);
            ruiten.Text = "Maak er Ruiten van";

            schoppen = new Button();
            schoppen.Click += schoppen_Click;
            schoppen.Location = new Point(300, 350);
            schoppen.Size = new Size(200, 50);
            schoppen.Text = "Maak er Schoppen van";

            this.Controls.Add(klaver);
            this.Controls.Add(harten);
            this.Controls.Add(ruiten);
            this.Controls.Add(schoppen);
            verbergKleurknoppen();
            
            //HIER CHAT

            this.Show();
        }

        public override void startSpel(Instellingen instellingen)
        {
            Text = "CyberPesten: Online spel";
            spel = new OnlineSpel(this);
        }

        public void stop()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(stop));
            }
            else
            {
                this.Close();
            }
        }

        new void muisKlik(object sender, MouseEventArgs mea)
        {
            bool actie = false;
            if (laatsteKaartRect.Contains(mea.Location))
            {
                stuurActie("lK");
            }
            else if (eindeBeurtRect.Contains(mea.Location))
            {
                stuurActie("eV");
            }

            else if (spel.spelend == 0)
            {
                bool success = false;
                if (mea.X >= potPlek.X && mea.X <= potPlek.X + kaartBreedte && mea.Y >= potPlek.Y && mea.Y <= potPlek.Y + kaartHoogte)
                //pot
                {
                    if (spel.speciaal == 4)
                    {
                        stuurActie("rPN");
                    }
                    else
                    {
                        stuurActie("pK");
                        
                    }
                    success = true;
                }
                if (!success)
                {
                    foreach (Kaart kaart in spel.spelers[0].hand)
                    {
                        if (mea.X >= kaart.X && mea.X <= kaart.X + kaartBreedte && mea.Y >= kaart.Y && mea.Y <= kaart.Y + kaartHoogte)
                        {
                            //kaart in hand van speler
                            actie = true;
                            base.muisKlik(sender, mea);
                            if (kaart == spel.stapel[spel.stapel.Count - 1])
                            {
                                stuurActie("sK:" + spel.spelers[0].hand.IndexOf(kaart));
                                return;
                            }
                        }
                    }
                }
            }

            if (!actie) { base.muisKlik(sender, mea); }

            
        }

        new void muisWiel(object sender, MouseEventArgs mea)
        {
            int oldIndex = 0; Kaart k = spel.stapel[spel.stapel.Count - 1];
            if (spel.spelend == 0)
            {
                foreach (Kaart kaart in spel.spelers[0].hand)
                {
                    //kijkt of er op een kaart is geklikt
                    int deltaX = mea.X - kaart.X;
                    int deltaY = mea.Y - kaart.Y;
                    if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                    {
                        return;
                    }
                    oldIndex++;
                }
            }
            base.muisWiel(sender,mea);
            if (k == spel.stapel[spel.stapel.Count - 1])
            {
                stuurActie("sK:" + oldIndex);
            }
        }

        new void muisOmlaag(object sender, MouseEventArgs mea)
        {
            int index = 0;
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                if (kaart != null)
                {
                    //kijkt of er op een kaart is geklikt
                    int deltaX = mea.X - kaart.X;
                    int deltaY = mea.Y - kaart.Y;
                    if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                    {
                        //stuurActie("bK:" + index);
                        return;
                    }
                    index++;
                }
            }
            base.muisOmlaag(sender, mea);
        }


        new void muisOmHoog(object sender, MouseEventArgs mea)
        {
            Kaart k = spel.stapel[spel.stapel.Count - 1];
            base.muisOmhoog(sender, mea);
            if (k != spel.stapel[spel.stapel.Count - 1])
            {
                stuurActie("sK:"+spel.spelers[0].hand.Count);//omdat de hand 1 kleiner is geworden, en de kaart van count -1 kwam, klopt dit.
            }
        }

        new void schoppen_Click(object sender, EventArgs e)
        {
            stuurActie("kV:3");
            base.schoppen_Click(sender, e);
        }
        new void ruiten_Click(object sender, EventArgs e)
        {
            stuurActie("kV:2");
            base.ruiten_Click(sender, e);
        }
        new void harten_Click(object sender, EventArgs e)
        {
            stuurActie("kV:0");
            base.harten_Click(sender, e);
        }
        new void klaver_Click(object sender, EventArgs e)
        {
            stuurActie("kV:1");
            base.klaver_Click(sender, e);
        }



        bool stuurActie(string actie)
        {
            OnlineSpel os = (OnlineSpel)spel;
            os.actieCount++;
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/new_action.php", new string[] { "name", "token", "gameid","laatsteactie"}, new string[] { Online.username, Online.token, Online.game.ToString(), actie});
            return raw == "ja";
        }
    }
}
