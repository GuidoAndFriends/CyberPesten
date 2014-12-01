using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Help : Form
    {
        int y;
        List<string> tekst;

        public Help()
        {
            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);
            Paint += teken;
            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            Graphics graphics = pea.Graphics;
            tekst = new List<string>();

            tekst.Add("Eerste regel");
            tekst.Add("Tweede regel");
            tekst.Add("Derde regel");

            y = 10;
            foreach(string stuk in tekst)
            {
                graphics.DrawString(stuk, new Font(FontFamily.GenericSansSerif, 16), Brushes.Black, new Point(10, y));
                y += 16 + 20;
            }
        }
    }
}
