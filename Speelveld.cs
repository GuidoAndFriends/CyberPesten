using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Speelveld : Form
    {
        public Spel spel;

        public Speelveld()
        {
            BackColor = Color.DarkGreen;
            Size = new Size(1000, 800);
            Paint += teken;
            MouseClick += klik;
            spel = new Spel(5);
            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            PictureBox pictureBox;
            if (spel.spelers.Count > 0)
            {
                List<Kaart> hand = spel.spelers.ElementAt(0).hand;
                int index = 0;
                foreach (Kaart kaart in hand)
                {
                    pictureBox = new PictureBox();
                    pictureBox.Image = kaart.bitmap;
                    pictureBox.Size = pictureBox.Image.Size;
                    pictureBox.Location = new Point(100 + index * 100, 600);
                    pictureBox.TabIndex = index;//identificatie
                    //pictureBox.Tag = index.ToString(); als identificatie
                    pictureBox.MouseClick += klikKaart;
                    Controls.Add(pictureBox);
                    index++;
                }
            }

            pictureBox = new PictureBox();
            pictureBox.Image = spel.stapel[spel.stapel.Count - 1].bitmap;
            pictureBox.Size = pictureBox.Image.Size;
            pictureBox.Location = new Point(450, 300);
            Controls.Add(pictureBox);
        }

        private void klik(object sender, MouseEventArgs mea)
        {

        }

        private void klikKaart(object sender, MouseEventArgs mea)
        {
            //Controleren op welke kaart er is geklikt en die spelen als dat mag
            PictureBox a = (PictureBox)sender;
            int index = a.TabIndex;
            bool kon = spel.speelKaart(index);
            if (kon)
            {
                System.Diagnostics.Debug.WriteLine("Kaart met index " + index + " is correct gespeeld.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Kaart met index " + index + " is foutief gespeeld.");
            }
            //Control moet verwijderd worden
            a.Hide();
            Invalidate();
        }
    }
}
