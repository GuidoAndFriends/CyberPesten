using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.IO;

namespace CyberPesten
{
    class Instellingenscherm : Form
    {
        Instellingen instellingen;
        Menu menu;
        Bitmap terugBitmap, standSetBitmap, achtergrond, switches, roodKnop, regenboogKnop;
        bool terugHover, standSetHover;
        Rectangle terugButton, standSetButton, maat;
        Rectangle aasDraai, tweePakken, zevenKleven, achtWacht, tienWasmachine, boerSwitch, heerNogEenKeer, jokerSwitch;
        Rectangle willekeurigSwitch, slimSwitch, slimmerSwitch, cheaterSwitch, aiModus, geluidSwitch, rood, blauw, regenboog;
        double verhoudingH,verhoudingW;
        GraphicsUnit units = GraphicsUnit.Pixel;

        public Instellingenscherm(Menu _menu)
        {
            menu = _menu;
            instellingen = menu.instellingen;
            maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            Text = "CyberPesten: Help";
            ClientSize = menu.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            achtergrond = new Bitmap(maat.Width, maat.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings_achtergrond"), maat);
            BackgroundImage = achtergrond;

            update();
            this.Show();

            terugBitmap = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Terug_button"));
            standSetBitmap = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("StandSet_button"));
            switches = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Settings_switches"));
            roodKnop = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achterkant_rood"));
            regenboogKnop = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achterkant_regenboog"));

            verhoudingW = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)1920;
            verhoudingH = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)1080;

            terugButton = new Rectangle(0, (int)(verhoudingH * 53), (int)(verhoudingW * 234), (int)(verhoudingH * 74));
            standSetButton = new Rectangle(0, (int)(verhoudingH * 927), (int)(verhoudingW * 1230), (int)(verhoudingH * 74));

            int switchWidth = (int)(verhoudingW * 211);
            int switchHeight = (int)(verhoudingH * 65);
            int swithKolom1 = (int)(verhoudingW* 176);
            int swithKolom2 = (int)(verhoudingW* 620);
            int swithKolom3 = (int)(verhoudingW* 1206);
            int swithKolom4 = (int)(verhoudingW* 1113);

            //Switches
            aasDraai = new Rectangle(swithKolom1, (int)(verhoudingH * 431), switchWidth, switchHeight);
            tweePakken = new Rectangle(swithKolom1, (int)(verhoudingH * 505), switchWidth, switchHeight);
            zevenKleven = new Rectangle(swithKolom1, (int)(verhoudingH * 578), switchWidth, switchHeight);
            achtWacht = new Rectangle(swithKolom1, (int)(verhoudingH * 651), switchWidth, switchHeight);
            tienWasmachine = new Rectangle(swithKolom2, (int)(verhoudingH * 431), switchWidth, switchHeight);
            boerSwitch = new Rectangle(swithKolom2, (int)(verhoudingH * 505), switchWidth, switchHeight);
            heerNogEenKeer = new Rectangle(swithKolom2, (int)(verhoudingH * 578), switchWidth, switchHeight);
            jokerSwitch = new Rectangle(swithKolom2, (int)(verhoudingH * 651), switchWidth, switchHeight);
            willekeurigSwitch = new Rectangle(swithKolom3, (int)(verhoudingH * 439), switchWidth, switchHeight);
            slimSwitch = new Rectangle(swithKolom3, (int)(verhoudingH * 512), switchWidth, switchHeight);
            slimmerSwitch = new Rectangle(swithKolom3, (int)(verhoudingH * 585), switchWidth, switchHeight);
            cheaterSwitch = new Rectangle(swithKolom3, (int)(verhoudingH * 658), switchWidth, switchHeight);
            aiModus = new Rectangle(swithKolom4, (int)(verhoudingH * 249), switchWidth, switchHeight);
            geluidSwitch = new Rectangle(swithKolom4, (int)(verhoudingH * 779), switchWidth, switchHeight);

            rood = new Rectangle((int)(verhoudingW * 624), (int)(verhoudingH * 853), (int)(verhoudingW * 155), (int)(verhoudingH * 65));
            blauw = new Rectangle((int)(verhoudingW * 779), (int)(verhoudingH * 853), (int)(verhoudingW * 105), (int)(verhoudingH * 65));
            regenboog = new Rectangle((int)(verhoudingW * 884), (int)(verhoudingH * 853), (int)(verhoudingW * 209), (int)(verhoudingH * 65));

            this.Paint += this.buildAchtergrond;
            this.Paint += this.selected;
            this.MouseMove += this.hover;
            this.MouseClick += this.klik;
        }

        void update()
        {
            Invalidate();
            Update();
        }

        private void buildAchtergrond(object sender, PaintEventArgs pea)
        {
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            pea.Graphics.DrawImage(BackgroundImage, 0, 0);
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }

        private void selected(object sender, PaintEventArgs pea)
        {
            if (terugHover)
            {
                pea.Graphics.DrawImage(terugBitmap, maat);
            }
            if (standSetHover)
            {
                pea.Graphics.DrawImage(standSetBitmap, maat);
            }

            if (!instellingen.regelsIngeschakeld.Contains(0))
            {
                pea.Graphics.DrawImage(switches, aasDraai, aasDraai, units);
            }
            if (!instellingen.regelsIngeschakeld.Contains(1))
            {
                pea.Graphics.DrawImage(switches, tweePakken, tweePakken, units);
            }
            if (!instellingen.regelsIngeschakeld.Contains(2))
            {
                pea.Graphics.DrawImage(switches, zevenKleven, zevenKleven, units);
            }
            if (!instellingen.regelsIngeschakeld.Contains(3))
            {
                pea.Graphics.DrawImage(switches, achtWacht, achtWacht, units);
            }
            if (instellingen.regelsIngeschakeld.Contains(7))
            {
                pea.Graphics.DrawImage(switches, tienWasmachine, tienWasmachine, units);
            }
            if (!instellingen.regelsIngeschakeld.Contains(4))
            {
                pea.Graphics.DrawImage(switches, boerSwitch, boerSwitch, units);
            }
            if (instellingen.regelsIngeschakeld.Contains(6))
            {
                pea.Graphics.DrawImage(switches, heerNogEenKeer, heerNogEenKeer, units);
            }
            if (!instellingen.regelsIngeschakeld.Contains(5))
            {
                pea.Graphics.DrawImage(switches, jokerSwitch, jokerSwitch, units);
            }

            if (!instellingen.AIIngeschakeld.Contains(0))
            {
                pea.Graphics.DrawImage(switches, willekeurigSwitch, willekeurigSwitch, units);
            }
            if (!instellingen.AIIngeschakeld.Contains(1))
            {
                pea.Graphics.DrawImage(switches, slimSwitch, slimSwitch, units);
            }
            if (!instellingen.AIIngeschakeld.Contains(2))
            {
                pea.Graphics.DrawImage(switches, slimmerSwitch, slimmerSwitch, units);
            }
            if (instellingen.AIIngeschakeld.Contains(3))
            {
                pea.Graphics.DrawImage(switches, cheaterSwitch, cheaterSwitch, units);
            }

            if (instellingen.mensSpelend == true)
            {
                pea.Graphics.DrawImage(switches, aiModus, aiModus, units);
            }
            if (instellingen.geluid == false)
            {
                pea.Graphics.DrawImage(switches, geluidSwitch, geluidSwitch, units);
            }

            if (instellingen.achterkant == 0)
            {
                pea.Graphics.DrawImage(roodKnop, maat);
            }
            if (instellingen.achterkant == 2)
            {
                pea.Graphics.DrawImage(regenboogKnop, maat);
            }

            Invalidate();
        }

        private void hover(object sender, MouseEventArgs mea)
        {
            if (terugButton.Contains(mea.Location))
            {
                terugHover = true;
            }
            else
            {
                terugHover = false;
            }

            if (standSetButton.Contains(mea.Location))
            {
                standSetHover = true;
            }
            else
            {
                standSetHover = false;
            }
            Invalidate();
        }

        private void buttonSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (terugHover)
            {
                instellingen.schrijven();
                buttonSound();
                menu.Show();
                this.Close();
            }

            if (standSetHover)
            {
                buttonSound();
                instellingen.standaard();
                update();
                instellingen.schrijven();
            }

            //Switches
            if (aasDraai.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(0);
            }
            if (tweePakken.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(1);
            }
            if (zevenKleven.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(2);
            }
            if (achtWacht.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(3);
            }
            if (tienWasmachine.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(7);
            }
            if (boerSwitch.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(4);
            }
            if (heerNogEenKeer.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(6);
            }
            if (jokerSwitch.Contains(mea.Location))
            {
                buttonSound();
                regelSchakel(5);
            }

            if (willekeurigSwitch.Contains(mea.Location))
            {
                buttonSound();
                AIschakel(0);
            }
            if (slimSwitch.Contains(mea.Location))
            {
                buttonSound();
                AIschakel(1);
            }
            if (slimmerSwitch.Contains(mea.Location))
            {
                buttonSound();
                AIschakel(2);
            }
            if (cheaterSwitch.Contains(mea.Location))
            {
                buttonSound();
                AIschakel(3);
            }

            if (aiModus.Contains(mea.Location))
            {
                buttonSound();
                instellingen.mensSpelend = !instellingen.mensSpelend;
            }
            if (geluidSwitch.Contains(mea.Location))
            {
                buttonSound();
                instellingen.geluid = !instellingen.geluid;
            }

            //Kaart achterkant kleur
            if (rood.Contains(mea.Location))
            {
                buttonSound();
                instellingen.achterkant = 0;
            }
            if (blauw.Contains(mea.Location))
            {
                buttonSound();
                instellingen.achterkant = 1;
            }
            if (regenboog.Contains(mea.Location))
            {
                buttonSound();
                instellingen.achterkant = 2;
            }
        }

        void regelSchakel(int index)
        {
            if (instellingen.regelsIngeschakeld.Contains(index))
            {
                instellingen.regelsIngeschakeld.Remove(index);
            }
            else
            {
                instellingen.regelsIngeschakeld.Add(index);
            }
        }

        void AIschakel(int index)
        {
            if (instellingen.AIIngeschakeld.Contains(index))
            {
                buttonSound();
                instellingen.AIIngeschakeld.Remove(index);
            }
            else
            {
                buttonSound();
                instellingen.AIIngeschakeld.Add(index);
            }
        }
    }
}
