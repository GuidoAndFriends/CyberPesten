using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Instellingenscherm : Form
    {
        Instellingen instellingen;
        NumericUpDown regelsetCon, aantalSpelersCon;
        TextBox regelsIngeschakeldCon, AIIngeschakeldCon;
        Button mensSpelendCon;
        Menu menu;
        Bitmap terugBitmap, standSetBitmap, achtergrond;
        bool terugHover, standSetHover;
        Rectangle terugButton, standSetButton, maat;
        float verhouding;

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

            /*Label regelsetLab = new Label();
            regelsetLab.Text = "Regelset";
            regelsetLab.BackColor = Color.Transparent;
            regelsetLab.Size = new Size(150, 40);
            regelsetLab.Location = new Point(50, 50);
            Controls.Add(regelsetLab);*/

            regelsetCon = new NumericUpDown();
            regelsetCon.Value = instellingen.regelset;
            regelsetCon.Location = new Point(220, 50);
            Controls.Add(regelsetCon);

            /*Label regelsIngeschakeldLab = new Label();
            regelsIngeschakeldLab.Text = "Ingeschakelde regels (1,2,3)";
            regelsIngeschakeldLab.BackColor = Color.Transparent;
            regelsIngeschakeldLab.Size = new Size(150, 40);
            regelsIngeschakeldLab.Location = new Point(50, 150);
            Controls.Add(regelsIngeschakeldLab);*/

            regelsIngeschakeldCon = new TextBox();
            regelsIngeschakeldCon.Location = new Point(220, 150);
            Controls.Add(regelsIngeschakeldCon);

            /*Label aantalSpelersLab = new Label();
            aantalSpelersLab.Text = "Aantal spelers";
            aantalSpelersLab.BackColor = Color.Transparent;
            aantalSpelersLab.Size = new Size(150, 40);
            aantalSpelersLab.Location = new Point(50, 250);
            Controls.Add(aantalSpelersLab);*/

            aantalSpelersCon = new NumericUpDown();
            aantalSpelersCon.Location = new Point(220, 250);
            aantalSpelersCon.DecimalPlaces = 0;
            aantalSpelersCon.ValueChanged += aantalSpelers;
            Controls.Add(aantalSpelersCon);

            /*Label AIIngeschakeldLab = new Label();
            AIIngeschakeldLab.Text = "Ingeschakelde AI (0,2,3)";
            AIIngeschakeldLab.BackColor = Color.Transparent;
            AIIngeschakeldLab.Size = new Size(150, 40);
            AIIngeschakeldLab.Location = new Point(50, 350);
            Controls.Add(AIIngeschakeldLab);*/

            AIIngeschakeldCon = new TextBox();
            AIIngeschakeldCon.Location = new Point(220, 350);
            AIIngeschakeldCon.TextChanged += AIIngeschakeld;
            Controls.Add(AIIngeschakeldCon);

            /*Label mensSpelendLab = new Label();
            mensSpelendLab.Text = "Menselijke speler";
            mensSpelendLab.BackColor = Color.Transparent;
            mensSpelendLab.Size = new Size(150, 40);
            mensSpelendLab.Location = new Point(50, 450);
            Controls.Add(mensSpelendLab);*/

            mensSpelendCon = new Button();
            mensSpelendCon.Location = new Point(220, 450);
            mensSpelendCon.Click += mensSpelend;
            Controls.Add(mensSpelendCon);

            /*Button resetCon = new Button();
            resetCon.Text = "Standaard instellingen";
            resetCon.Size = new Size(150, 40);
            resetCon.Location = new Point(50, 550);
            resetCon.Click += reset;
            Controls.Add(resetCon);*/

            /*Button terugCon = new Button();
            terugCon.Text = "Terug naar menu";
            terugCon.Size = new Size(150, 40);
            terugCon.Location = new Point(50, 650);
            terugCon.Click += terug;
            Controls.Add(terugCon);*/

            update();
            this.Show();

            terugBitmap = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Terug_button"));
            standSetBitmap = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("StandSet_button"));

            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / terugBitmap.Width;

            terugButton = new Rectangle(0, (int)(verhouding * 53), (int)(verhouding * 234), (int)(verhouding * 74));
            standSetButton = new Rectangle(0, (int)(verhouding * 927), (int)(verhouding * 1230), (int)(verhouding * 74));

            this.Paint += this.buildAchtergrond;
            this.Paint += this.selected;
            this.MouseMove += this.hover;
            this.MouseClick += this.klik;
        }

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

        void update()
        {
            regelsetCon.Value = instellingen.regelset;
            aantalSpelersCon.Value = instellingen.aantalSpelers;

            string regel = "";
            if (instellingen.AIIngeschakeld != null)
            {
                if (instellingen.AIIngeschakeld.Count != 0)
                {
                    regel += instellingen.AIIngeschakeld[0].ToString();
                    for (int i = 1; i < instellingen.AIIngeschakeld.Count; i++)
                    {
                        regel += "," + instellingen.AIIngeschakeld[i].ToString();
                    }
                }
            }
            AIIngeschakeldCon.Text = regel;

            if (instellingen.mensSpelend)
            {
                mensSpelendCon.Text = "Aan";
            }
            else
            {
                mensSpelendCon.Text = "Uit";
            }
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

        private void klik(object sender, MouseEventArgs mea)
        {
            if (terugHover)
            {
                menu.Show();
                this.Close();
            }

            if (standSetHover)
            {
                instellingen.standaard();
                update();
                instellingen.schrijven();
            }
        }
    }
}
