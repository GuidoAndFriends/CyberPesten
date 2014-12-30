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
        TextBox regelsUitgeschakeldCon, AIUitgeschakeldCon;
        Button mensSpelendCon;
        Menu menu;

        public Instellingenscherm(Menu _menu)
        {
            menu = _menu;
            instellingen = menu.instellingen;

            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond");
            ClientSize = menu.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            Label regelsetLab = new Label();
            regelsetLab.Text = "Regelset";
            regelsetLab.BackColor = Color.Transparent;
            regelsetLab.Size = new Size(150, 40);
            regelsetLab.Location = new Point(50, 50);
            Controls.Add(regelsetLab);

            regelsetCon = new NumericUpDown();
            regelsetCon.Value = instellingen.regelset;
            regelsetCon.Location = new Point(220, 50);
            Controls.Add(regelsetCon);

            Label regelsUitgeschakeldLab = new Label();
            regelsUitgeschakeldLab.Text = "Uitgeschakelde regels (1,2,3)";
            regelsUitgeschakeldLab.BackColor = Color.Transparent;
            regelsUitgeschakeldLab.Size = new Size(150, 40);
            regelsUitgeschakeldLab.Location = new Point(50, 150);
            Controls.Add(regelsUitgeschakeldLab);

            regelsUitgeschakeldCon = new TextBox();
            regelsUitgeschakeldCon.Location = new Point(220, 150);
            regelsUitgeschakeldCon.Click += regelsUitgeschakeld;
            Controls.Add(regelsUitgeschakeldCon);

            Label aantalSpelersLab = new Label();
            aantalSpelersLab.Text = "Aantal spelers";
            aantalSpelersLab.BackColor = Color.Transparent;
            aantalSpelersLab.Size = new Size(150, 40);
            aantalSpelersLab.Location = new Point(50, 250);
            Controls.Add(aantalSpelersLab);

            aantalSpelersCon = new NumericUpDown();
            aantalSpelersCon.Location = new Point(220, 250);
            aantalSpelersCon.DecimalPlaces = 0;
            aantalSpelersCon.ValueChanged += aantalSpelers;
            Controls.Add(aantalSpelersCon);

            Label AIUitgeschakeldLab = new Label();
            AIUitgeschakeldLab.Text = "Uitgeschakelde AI (1,2,3)";
            AIUitgeschakeldLab.BackColor = Color.Transparent;
            AIUitgeschakeldLab.Size = new Size(150, 40);
            AIUitgeschakeldLab.Location = new Point(50, 350);
            Controls.Add(AIUitgeschakeldLab);

            AIUitgeschakeldCon = new TextBox();
            AIUitgeschakeldCon.Location = new Point(220, 350);
            AIUitgeschakeldCon.Click += AIUitgeschakeld;
            Controls.Add(AIUitgeschakeldCon);

            Label mensSpelendLab = new Label();
            mensSpelendLab.Text = "Menselijke speler";
            mensSpelendLab.BackColor = Color.Transparent;
            mensSpelendLab.Size = new Size(150, 40);
            mensSpelendLab.Location = new Point(50, 450);
            Controls.Add(mensSpelendLab);

            mensSpelendCon = new Button();
            mensSpelendCon.Location = new Point(220, 450);
            mensSpelendCon.Click += mensSpelend;
            Controls.Add(mensSpelendCon);

            Button resetCon = new Button();
            resetCon.Text = "Standaard instellingen";
            resetCon.Size = new Size(150, 40);
            resetCon.Location = new Point(50, 550);
            resetCon.Click += reset;
            Controls.Add(resetCon);

            Button terugCon = new Button();
            terugCon.Text = "Terug naar menu";
            terugCon.Size = new Size(150, 40);
            terugCon.Location = new Point(50, 650);
            terugCon.Click += terug;
            Controls.Add(terugCon);

            update();
            this.Show();
        }

        private void regelsUitgeschakeld(object sender, EventArgs ea)
        {
            string[] delen = regelsUitgeschakeldCon.Text.Split(new char[] { ',' });
            instellingen.regelsUitgeschakeld = new List<int>();
            if (delen[0] != "")
            {
                for (int i = 0; i < delen.Length; i++)
                {
                    instellingen.regelsUitgeschakeld.Add(Int32.Parse(delen[i]) - 1);
                }
            }
        }

        private void aantalSpelers(object sender, EventArgs ea)
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

        private void AIUitgeschakeld(object sender, EventArgs ea)
        {
            string[] delen = AIUitgeschakeldCon.Text.Split(new char[] { ',' });
            instellingen.AIUitgeschakeld = new List<int>();
            if (delen[0] != "")
            {
                for (int i = 0; i < delen.Length; i++)
                {
                    instellingen.AIUitgeschakeld.Add(Int32.Parse(delen[i]) - 1);
                }
            }
        }

        private void mensSpelend(object sender, EventArgs ea)
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

        private void reset(object sender, EventArgs ea)
        {
            instellingen.standaard();
            update();
            instellingen.schrijven();
        }


        private void update()
        {
            regelsetCon.Value = instellingen.regelset;
            aantalSpelersCon.Value = instellingen.aantalSpelers;
            if (instellingen.mensSpelend)
            {
                mensSpelendCon.Text = "Aan";
            }
            else
            {
                mensSpelendCon.Text = "Uit";
            }
        }

        private void terug(object sender, EventArgs ea)
        {
            menu.Show();
            this.Close(); 
        }
    }
}
