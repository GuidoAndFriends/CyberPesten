using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Venster : Form
    {
        Speelveld veld;

        public Venster()
        {
            veld = new Speelveld();
            this.ClientSize = new Size(1000, 800);
            this.BackColor = Color.DarkGreen;
            this.Paint += this.teken;
            this.MouseClick += this.klik;
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            if (this.veld.spel.spelers.Count > 0)
            {
                List<Kaart> hand = this.veld.spel.spelers.ElementAt(0).hand;
                int index = 0;
                foreach (Kaart kaart in hand)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = kaart.bitmap;
                    pictureBox.Size = pictureBox.Image.Size;
                    pictureBox.Location = new Point(100 + index * 100, 600);
                    //pictureBox.Tag = index.ToString(); als identificatie
                    pictureBox.MouseClick += klikKaart;
                    Controls.Add(pictureBox);
                    index++;
                }
            }
        }

        private void klik(object sender, MouseEventArgs mea)
        {

        }

        private void klikKaart(object sender, MouseEventArgs mea)
        {
            //Controleren op welke kaart er is geklikt en die spelen als dat mag
        }
    }
}
