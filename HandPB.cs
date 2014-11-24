using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class HandPB : Panel
    {
        public HandPB(Speelveld speelveld, Spel spel)
        {
            BackColor = Color.LightGreen; //Tijdelijk om te zien waar de speelhand is
            List<PictureBox> handKaarten = new List<PictureBox>();
            List<Kaart> hand = spel.spelers.ElementAt(0).hand;
            int index = 0;
            foreach (Kaart kaart in hand)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = kaart.voorkant;
                pictureBox.Size = pictureBox.Image.Size;
                pictureBox.Location = new Point(index * 100, 0);
                pictureBox.TabIndex = index;//identificatie
                //pictureBox.Tag = index.ToString(); als identificatie
                pictureBox.MouseClick += speelveld.klikKaart;
                Controls.Add(pictureBox);
                index++;
            }
        }
    }
}
