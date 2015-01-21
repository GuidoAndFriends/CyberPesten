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
        Spel spel;

        public KleurKiezen(Spel _spel)
        {
            Text = "Kies je Kleur";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(550, 250);
            spel = _spel;

            Button klaver = new Button();
            klaver.Click += klaver_Click;
            klaver.Location = new Point(50, 50);
            klaver.Size = new Size(200, 50);
            klaver.Text = "Maak er Klaver van";

            Button harten = new Button();
            harten.Click += harten_Click;
            harten.Location = new Point(300, 50);
            harten.Size = new Size(200, 50);
            harten.Text = "Maak er Harten van";

            Button ruiten = new Button();
            ruiten.Click += ruiten_Click;
            ruiten.Location = new Point(50, 150);
            ruiten.Size = new Size(200, 50);
            ruiten.Text = "Maak er Ruiten van";

            Button schoppen = new Button();
            schoppen.Click += schoppen_Click;
            schoppen.Location = new Point(300, 150);
            schoppen.Size = new Size(200, 50);
            schoppen.Text = "Maak er Schoppen van";
            
            this.Controls.Add(klaver);
            this.Controls.Add(harten);
            this.Controls.Add(ruiten);
            this.Controls.Add(schoppen);
            this.Show();
        }


        void schoppen_Click(object sender, EventArgs e)
        {
            spel.speciaal = 3;
            spel.chat.nieuw("Je koos voor schoppen");
            spel.volgende();
            this.Close();
        }

        void ruiten_Click(object sender, EventArgs e)
        {
            spel.speciaal = 2;
            spel.chat.nieuw("Je en koos voor ruiten");
            spel.volgende();
            this.Close();
        }

        void harten_Click(object sender, EventArgs e)
        {
            spel.speciaal = 0;
            spel.chat.nieuw("Je en koos voor harten");
            spel.volgende();
            this.Close();
        }

        void klaver_Click(object sender, EventArgs e)
        {
            spel.speciaal = 1;
            spel.chat.nieuw("Je en koos voor klaver");
            spel.volgende();
            this.Close();
        }
    }
}
