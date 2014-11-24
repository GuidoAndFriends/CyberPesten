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
        public Menu menu;
        public Spel spel;

        public Speelveld(Menu m)
        {
            BackColor = Color.DarkGreen;
            Size = new Size(1000, 800);
            Paint += teken;
            MouseClick += klik;
            Scroll += scroll;
            spel = new Spel(this, 5);
            menu = m;
            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            gr.FillRectangle(Brushes.DarkGreen, 0, 0, Width, Height);
            spel.spelers[0].maakXY();
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                gr.DrawImage(kaart.voorkant, kaart.X, kaart.Y);
            }
            Bitmap plaatje = spel.stapel[spel.stapel.Count - 1].voorkant;
            gr.DrawImage(plaatje, 450, 300);
            plaatje = spel.stapel[spel.stapel.Count - 1].achterkant;
            gr.DrawImage(plaatje, 450, 100);
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

        private void scroll(object sender, EventArgs ea)
        {
            //Kaart spelen
        }

        private void afsluiten(object sender, FormClosedEventArgs fcea)
        {

        }
    }
}
