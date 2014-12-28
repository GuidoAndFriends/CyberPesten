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

        public Instellingenscherm(Menu menu)
        {
            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = menu.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            instellingen = menu.instellingen;
            instellingen = new Instellingen();

            Label regelsetLabel = new Label();
            regelsetLabel.Text = "Regelset";
            regelsetLabel.Size = new Size(150, 40);
            regelsetLabel.Location = new Point(50, 50);
            Controls.Add(regelsetLabel);

            NumericUpDown regelsetNumericUpDown = new NumericUpDown();
            regelsetNumericUpDown.Location = new Point(220, 50);
            Controls.Add(regelsetNumericUpDown);

            //regelsUitgeschakeld

            Label aantalSpelersLabel = new Label();
            aantalSpelersLabel.Text = "Aantal spelers";
            aantalSpelersLabel.Size = new Size(150, 40);
            aantalSpelersLabel.Location = new Point(50, 150);
            Controls.Add(aantalSpelersLabel);

            NumericUpDown aantalSpelersNumericUpDown = new NumericUpDown();
            aantalSpelersNumericUpDown.Value = instellingen.aantalSpelers;
            aantalSpelersNumericUpDown.Location = new Point(220, 150);
            Controls.Add(aantalSpelersNumericUpDown);

            Label mensSpelendLabel = new Label();
            mensSpelendLabel.Text = "Menselijke speler";
            mensSpelendLabel.Size = new Size(150, 40);
            mensSpelendLabel.Location = new Point(50, 250);
            Controls.Add(mensSpelendLabel);

            Button mensSpelendButton = new Button();
            if (instellingen.mensSpelend)
            {
                mensSpelendButton.Text = "Aan";
            }
            else
            {
                mensSpelendButton.Text = "Uit";
            }
            mensSpelendButton.Location = new Point(220, 250);
            Controls.Add(mensSpelendButton);

            /*
            regelset = 0;
             = null;
            aantalSpelers = 4;
            mensSpelend = true;
            */

            this.Show();
        }
    }
}
