using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace CyberPesten
{
    class Menu : Form
    {
        private Speelveld veld;

        public Menu()
        {
            BackColor = Color.DarkGreen;
            //BackgroundImage = System.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);

            //Knoppen er uit gaan laten zien als kaarten?

            Button lokaal = new Button();
            lokaal.Size = new Size(200, 280);
            lokaal.Location = new Point(200, 400);
            lokaal.Text = "Lokaal";
            lokaal.Font = new Font(FontFamily.GenericSansSerif, 28);
            lokaal.MouseClick += lokaalKlik;
            Controls.Add(lokaal);

            Button online = new Button();
            online.Size = new Size(200, 280);
            online.Location = new Point(600, 400);
            online.Text = "Online";
            online.Font = new Font(FontFamily.GenericSansSerif, 28);
            online.MouseClick += onlineKlik;
            Controls.Add(online);       
        }

        private void lokaalKlik(object sender, MouseEventArgs mea)
        {
            veld = new Speelveld(this);
            this.Hide();
        }

        private void onlineKlik(object sender, MouseEventArgs mea)
        {
            //Online spel starten
            this.Close();
        }
    }
}
