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
        Button mensSpelendCon;
        Menu menu;

        public Instellingenscherm(Menu _menu)
        {
            menu = _menu;
            instellingen = menu.instellingen;

            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
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
            regelsetCon.Location = new Point(220, 50);
            Controls.Add(regelsetCon);

            //regelsUitgeschakeld

            Label aantalSpelersLab = new Label();
            aantalSpelersLab.Text = "Aantal spelers";
            aantalSpelersLab.BackColor = Color.Transparent;
            aantalSpelersLab.Size = new Size(150, 40);
            aantalSpelersLab.Location = new Point(50, 150);
            Controls.Add(aantalSpelersLab);

            aantalSpelersCon = new NumericUpDown();
            aantalSpelersCon.Value = instellingen.aantalSpelers;
            aantalSpelersCon.Location = new Point(220, 150);
            aantalSpelersCon.DecimalPlaces = 0;
            aantalSpelersCon.ValueChanged += aantalSpelers;
            Controls.Add(aantalSpelersCon);

            Label mensSpelendLab = new Label();
            mensSpelendLab.Text = "Menselijke speler";
            mensSpelendLab.BackColor = Color.Transparent;
            mensSpelendLab.Size = new Size(150, 40);
            mensSpelendLab.Location = new Point(50, 250);
            Controls.Add(mensSpelendLab);

            mensSpelendCon = new Button();
            if (instellingen.mensSpelend)
            {
                mensSpelendCon.Text = "Aan";
            }
            else
            {
                mensSpelendCon.Text = "Uit";
            }
            mensSpelendCon.Location = new Point(220, 250);
            mensSpelendCon.Click += mensSpelend;
            Controls.Add(mensSpelendCon);

            Button terugCon = new Button();
            terugCon.Text = "Terug naar menu";
            terugCon.Size = new Size(150, 40);
            terugCon.Location = new Point(50, 350);
            terugCon.Click += terug;
            Controls.Add(terugCon);

            this.Show();
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
        }

        private void terug(object sender, EventArgs ea)
        {
            menu.Show();
            this.Close(); 
        }
    }
}
