using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class KleurKiezen : Form
    {
        int kleur;
        Button klaver, harten, ruiten, schoppen;

        public KleurKiezen()
        {
            klaver = new Button();
            klaver.Click += klaver_Click;
            klaver.Location = new Point(100, 100);
            klaver.Size = new Size(200, 50);
            klaver.Text = "Maak er Klaver van";

            harten = new Button();
            harten.Click += harten_Click;
            harten.Location = new Point(500, 100);
            harten.Size = new Size(200, 50);
            harten.Text = "Maak er Harten van";

            ruiten = new Button();
            ruiten.Click += ruiten_Click;
            ruiten.Location = new Point(100, 500);
            ruiten.Size = new Size(200, 50);
            ruiten.Text = "Maak er Ruiten van";

            schoppen = new Button();
            schoppen.Click += schoppen_Click;
            schoppen.Location = new Point(500, 500);
            schoppen.Size = new Size(200, 50);
            schoppen.Text = "Maak er Schoppen van";
            
            Text = "Kies je Kleur";
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);
            this.Controls.Add(klaver);
            this.Controls.Add(harten);
            this.Controls.Add(ruiten);
            this.Controls.Add(schoppen);
            this.Show();
        }


        public void schoppen_Click(object sender, EventArgs e)
        {
            kleur = 3;
            this.Close();
        }

        public void ruiten_Click(object sender, EventArgs e)
        {
            kleur = 2;
            this.Close();
        }

        public void harten_Click(object sender, EventArgs e)
        {
            kleur = 0;
            this.Close();
        }

        public void klaver_Click(object sender, EventArgs e)
        {
            kleur = 1;
            this.Close();
        }
    }
}
