using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    public class Menu : Form
    {
        private NumericUpDown aantal;

        public Menu()
        {
            Text = "CyberPesten: Menu";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            Rectangle maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Size = new Size(maat.X, maat.Y);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            //Knoppen er uit gaan laten zien als kaarten?

            Button lokaal = new Button();
            lokaal.Size = new Size(200, 300);
            lokaal.Location = new Point(200, 300);
            lokaal.Text = "Lokaal";
            lokaal.Font = new Font(FontFamily.GenericSansSerif, 28);
            lokaal.MouseClick += lokaalKlik;
            Controls.Add(lokaal);

            Button online = new Button();
            online.Size = new Size(200, 300);
            online.Location = new Point(600, 300);
            online.Text = "Online";
            online.Font = new Font(FontFamily.GenericSansSerif, 28);
            online.MouseClick += onlineKlik;
            Controls.Add(online);

            aantal = new NumericUpDown();
            aantal.Size = new Size(200, 50);
            aantal.Location = new Point(200, 620);
            aantal.Value = 4;
            Controls.Add(aantal);

            Button help = new Button();
            help.Size = new Size(135, 90);
            help.Location = new Point(865, 710);
            help.Text = "Help";
            help.Font = new Font(FontFamily.GenericSansSerif, 20);
            help.MouseClick += helpKlik;
            Controls.Add(help);
        }

        private void lokaalKlik(object sender, MouseEventArgs mea)
        {
            new LokaalSpel(false, (int)aantal.Value, this);
            this.Hide();
        }

        private void onlineKlik(object sender, MouseEventArgs mea)
        {
            //veld = new Speelveld(true, aantal.Value, this);
            this.Hide();
        }

        private void helpKlik(object sender, MouseEventArgs mea)
        {
            Help help = new Help();
        }
    }
}
