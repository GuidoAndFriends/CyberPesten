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
        //NumericUpDown regelsetCon, aantalSpelersCon;
        //TextBox regelsIngeschakeldCon, AIIngeschakeldCon;
        //Button mensSpelendCon;
        Menu menu;
        Bitmap terugBitmap, standSetBitmap, achtergrond, switches, roodKnop, regenboogKnop;
        bool terugHover, standSetHover;
        Rectangle terugButton, standSetButton, maat;
        Rectangle aasDraai, tweePakken, zevenKleven, achtWacht, tienWasmachine, boerSwitch, heerNogEenKeer, jokerSwitch;
        Rectangle willekeurigSwitch, slimSwitch, slimmerSwitch, cheaterSwitch, aiModus, geluidSwitch, rood, blauw, regenboog;
        bool aasDraaiBool, tweePakkenBool, zevenKlevenBool, achtWachtBool, tienWasmachineBool, boerSwitchBool, heerNogEenKeerBool, jokerSwitchBool;
        //bool willekeurigSwitchBool, slimSwitchBool, slimmerSwitchBool, cheaterSwitchBool, aiModusBool, geluidSwitchBool;
        float verhouding;
        GraphicsUnit units = GraphicsUnit.Pixel;
        //int achterkantKleur = 2;

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

            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / terugBitmap.Width;

            terugButton = new Rectangle(0, (int)(verhouding * 53), (int)(verhouding * 234), (int)(verhouding * 74));
            standSetButton = new Rectangle(0, (int)(verhouding * 927), (int)(verhouding * 1230), (int)(verhouding * 74));

            int switchWidth = (int)(verhouding * 211);
            int switchHeight = (int)(verhouding * 65);
            int swithKolom1 = (int)(verhouding* 176);
            int swithKolom2 = (int)(verhouding* 620);
            int swithKolom3 = (int)(verhouding* 1206);
            int swithKolom4 = (int)(verhouding* 1113);

            //Switches
            aasDraai = new Rectangle(swithKolom1, (int)(verhouding * 431), switchWidth, switchHeight);
            tweePakken = new Rectangle(swithKolom1, (int)(verhouding * 505), switchWidth, switchHeight);
            zevenKleven = new Rectangle(swithKolom1, (int)(verhouding * 578), switchWidth, switchHeight);
            achtWacht = new Rectangle(swithKolom1, (int)(verhouding * 651), switchWidth, switchHeight);
            tienWasmachine = new Rectangle(swithKolom2, (int)(verhouding * 431), switchWidth, switchHeight);
            boerSwitch = new Rectangle(swithKolom2, (int)(verhouding * 505), switchWidth, switchHeight);
            heerNogEenKeer = new Rectangle(swithKolom2, (int)(verhouding * 578), switchWidth, switchHeight);
            jokerSwitch = new Rectangle(swithKolom2, (int)(verhouding * 651), switchWidth, switchHeight);
            willekeurigSwitch = new Rectangle(swithKolom3, (int)(verhouding * 439), switchWidth, switchHeight);
            slimSwitch = new Rectangle(swithKolom3, (int)(verhouding * 512), switchWidth, switchHeight);
            slimmerSwitch = new Rectangle(swithKolom3, (int)(verhouding * 585), switchWidth, switchHeight);
            cheaterSwitch = new Rectangle(swithKolom3, (int)(verhouding * 658), switchWidth, switchHeight);
            aiModus = new Rectangle(swithKolom4, (int)(verhouding * 249), switchWidth, switchHeight);
            geluidSwitch = new Rectangle(swithKolom4, (int)(verhouding * 779), switchWidth, switchHeight);

            rood = new Rectangle((int)(verhouding * 624), (int)(verhouding * 853), (int)(verhouding * 155), (int)(verhouding * 65));
            blauw = new Rectangle((int)(verhouding * 779), (int)(verhouding * 853), (int)(verhouding * 105), (int)(verhouding * 65));
            regenboog = new Rectangle((int)(verhouding * 884), (int)(verhouding * 853), (int)(verhouding * 209), (int)(verhouding * 65));

            
            //Switch booleans
            aasDraaiBool = true; tweePakkenBool = true; zevenKlevenBool = true; achtWachtBool = true;
            tienWasmachineBool = false; boerSwitchBool = true; heerNogEenKeerBool = false; jokerSwitchBool = true;
            //willekeurigSwitchBool = true; slimSwitchBool = true; slimmerSwitchBool= true; cheaterSwitchBool = false;
            //aiModusBool = false; geluidSwitchBool = true;
            

            this.Paint += this.buildAchtergrond;
            this.Paint += this.selected;
            this.MouseMove += this.hover;
            this.MouseClick += this.klik;
        }

        /*
        void regelsIngeschakeld(object sender, EventArgs ea)
        {
            string[] delen = regelsIngeschakeldCon.Text.Split(new char[] { ',' });
            instellingen.regelsIngeschakeld = new List<int>();
            if (delen[0] != "")
            {
                for (int i = 0; i < delen.Length; i++)
                {
                    instellingen.regelsIngeschakeld.Add(Int32.Parse(delen[i]));
                }
            }
        }

        void aantalSpelers(object sender, EventArgs ea)
        {
            if (aantalSpelersCon.Value < 2)
            {
                aantalSpelersCon.Value = 2;
            }
            else if (aantalSpelersCon.Value > 7)
            {
                aantalSpelersCon.Value = 7;
            }
            instellingen.aantalSpelers = (int)aantalSpelersCon.Value;
            instellingen.schrijven();
        }

        void AIIngeschakeld(object sender, EventArgs ea)
        {
            string[] delen = AIIngeschakeldCon.Text.Split(new char[] { ',' });
            instellingen.AIIngeschakeld = new List<int>();

            for (int i = 0; i < delen.Length; i++)
            {
                if (delen[i] != "")
                {
                    instellingen.AIIngeschakeld.Add(Int32.Parse(delen[i]));
                }
            }
            instellingen.schrijven();
        }

        void mensSpelend(object sender, EventArgs ea)
        {
            if (instellingen.mensSpelend)
            {
                instellingen.mensSpelend = false;
                mensSpelendCon.Text = "Uit";
            }
            else
            {
                instellingen.mensSpelend = true;
                mensSpelendCon.Text = "Aan";
            }
            instellingen.schrijven();
        }
         */

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

            if (aasDraaiBool == false)
            {
                pea.Graphics.DrawImage(switches, aasDraai, aasDraai, units);
            }
            if (tweePakkenBool == false)
            {
                pea.Graphics.DrawImage(switches, tweePakken, tweePakken, units);
            }
            if (zevenKlevenBool == false)
            {
                pea.Graphics.DrawImage(switches, zevenKleven, zevenKleven, units);
            }
            if (achtWachtBool == false)
            {
                pea.Graphics.DrawImage(switches, achtWacht, achtWacht, units);
            }
            if (tienWasmachineBool == true)
            {
                pea.Graphics.DrawImage(switches, tienWasmachine, tienWasmachine, units);
            }
            if (boerSwitchBool == false)
            {
                pea.Graphics.DrawImage(switches, boerSwitch, boerSwitch, units);
            }
            if (heerNogEenKeerBool == true)
            {
                pea.Graphics.DrawImage(switches, heerNogEenKeer, heerNogEenKeer, units);
            }
            if (jokerSwitchBool == false)
            {
                pea.Graphics.DrawImage(switches, jokerSwitch, jokerSwitch, units);
            }

            if (instellingen.AIIngeschakeld.Contains(0))
            {
                pea.Graphics.DrawImage(switches, willekeurigSwitch, willekeurigSwitch, units);
            }
            if (instellingen.AIIngeschakeld.Contains(1))
            {
                pea.Graphics.DrawImage(switches, slimSwitch, slimSwitch, units);
            }
            if (instellingen.AIIngeschakeld.Contains(2))
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
                aasDraaiBool = !aasDraaiBool;
            }
            if (tweePakken.Contains(mea.Location))
            {
                tweePakkenBool = !tweePakkenBool;
            }
            if (zevenKleven.Contains(mea.Location))
            {
                zevenKlevenBool = !zevenKlevenBool;
            }
            if (achtWacht.Contains(mea.Location))
            {
                achtWachtBool = !achtWachtBool;
            }
            if (tienWasmachine.Contains(mea.Location))
            {
                tienWasmachineBool = !tienWasmachineBool;
            }
            if (boerSwitch.Contains(mea.Location))
            {
                boerSwitchBool = !boerSwitchBool;
            }
            if (heerNogEenKeer.Contains(mea.Location))
            {
                heerNogEenKeerBool = !heerNogEenKeerBool;
            }
            if (jokerSwitch.Contains(mea.Location))
            {
                jokerSwitchBool = !jokerSwitchBool;
            }

            if (willekeurigSwitch.Contains(mea.Location))
            {
                AIschakel(0);
            } 
            if (slimSwitch.Contains(mea.Location))
            {
                AIschakel(1);
            }
            if (slimmerSwitch.Contains(mea.Location))
            {
                AIschakel(2);
            }
            if (cheaterSwitch.Contains(mea.Location))
            {
                AIschakel(3);
            }

            if (aiModus.Contains(mea.Location))
            {
                instellingen.mensSpelend = !instellingen.mensSpelend;
            }
            if (geluidSwitch.Contains(mea.Location))
            {
                instellingen.geluid = !instellingen.geluid;
            }

            //Kaart achterkant kleur
            if (rood.Contains(mea.Location))
            {
                instellingen.achterkant = 0;
            }
            if (blauw.Contains(mea.Location))
            {
                instellingen.achterkant = 1;
            }
            if (regenboog.Contains(mea.Location))
            {
                instellingen.achterkant = 2;
            }
        }

        void AIschakel(int index)
        {
            if (instellingen.AIIngeschakeld.Contains(index))
            {
                instellingen.AIIngeschakeld.Remove(index);
            }
            else
            {
                instellingen.AIIngeschakeld.Add(index);
            }
        }
    }
}
