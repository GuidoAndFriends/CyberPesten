using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Menu : Form
    {
        private Speelveld veld;

        public Menu()
        {
            BackColor = Color.DarkGreen;
            ClientSize = new Size(1000, 800);

            //Knoppen er uit gaan laten zien als kaarten?

            Button lokaal = new Button();
            lokaal.Size = new Size(200, 300);
            lokaal.Location = new Point(100, 400);
            lokaal.MouseClick += lokaalKlik;
            Controls.Add(lokaal);

            Button online = new Button();
            online.Size = new Size(200, 300);
            online.Location = new Point(400, 400);
            online.MouseClick += onlineKlik;
            Controls.Add(online);       
        }

        private void lokaalKlik(object sender, MouseEventArgs mea)
        {
            veld = new Speelveld();
            this.Hide();
        }

        private void onlineKlik(object sender, MouseEventArgs mea)
        {
            //Online spel starten
            this.Hide();
        }
    }
}
