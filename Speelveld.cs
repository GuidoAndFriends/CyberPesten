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
        public HandPB handPB;
        public PictureBox stapelPB, potPB;

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
            /*

            }
            */

            if (spel.spelers.Count > 0)
            {
                handPB = null;
                handPB = new HandPB(this, spel);
                handPB.Size = new Size(1000, 250);
                handPB.Location = new Point(0, 550);
                Controls.Add(handPB);
            }

            stapelPB = new PictureBox();
            stapelPB.Image = spel.stapel[spel.stapel.Count - 1].voorkant;
            stapelPB.Size = stapelPB.Image.Size;
            stapelPB.Location = new Point(450, 300);
            Controls.Add(stapelPB);

            potPB = new PictureBox();
            potPB.Image = spel.stapel[spel.stapel.Count - 1].achterkant;
            potPB.Size = potPB.Image.Size;
            potPB.Location = new Point(450, 100);
            Controls.Add(potPB);
        }

        private void klik(object sender, MouseEventArgs mea)
        {

        }

        public void klikKaart(object sender, MouseEventArgs mea)
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
