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
            if (veld.spel.bezig) //misschien niet meer nodig
            {
                //Tekent de stapel
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = veld.spel.stapel.ElementAt(veld.spel.stapel.Count - 1).bitmap;
                pictureBox.Size = pictureBox.Image.Size;
                pictureBox.Location = new Point(200, 200);
                Controls.Add(pictureBox);

                //Tekent de hand van de speler
                List<Kaart> hand = this.veld.spel.spelers.ElementAt(0).hand;
                int index = 0;
                foreach (Kaart kaart in hand)
                {
                    pictureBox = new PictureBox();
                    pictureBox.Image = kaart.bitmap;
                    pictureBox.Size = pictureBox.Image.Size;
                    pictureBox.Location = new Point(100 + index * 100, 600);
                    pictureBox.Tag = index;
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
            PictureBox geklikt = sender as PictureBox;
            if (geklikt.Tag != null)
            {
                int index = (int)geklikt.Tag;
                System.Diagnostics.Debug.WriteLine(veld.spel.spelers.ElementAt(0).naam);
                //veld.spel.verplaatsKaart(veld.spel.spelers.ElementAt(0).hand, index, veld.spel.stapel); //Moet natuurlijk korter
                //PictureBox moet weer weg
                this.Invalidate();
            }
        }
    }
}
