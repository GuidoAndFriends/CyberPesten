using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Chat
    {
        List<string> regels = new List<string>();
        Font font;
        int fontHeight, breedte;

        public Chat(Speelveld speelveld)
        {
            font = new Font(FontFamily.GenericSansSerif, 14);
            fontHeight = 45;
            Point stapelPlek = speelveld.stapelPlek;
            this.breedte = stapelPlek.X - 20;
        }

        public void nieuw(string regel)
        {
            regels.Add(regel);
            if (regels.Count > 100)
            {
                regels.RemoveAt(0);
            }
        }

        public Bitmap maakBitmap(bool groot)
        {  
            Bitmap bitmap;
            if (groot)
            {
                bitmap = new Bitmap(breedte, 595, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                int i = 0;
                foreach (String regel in regels)
                {
                    i++;
                    Graphics.FromImage(bitmap).DrawString(regels[regels.Count - i], font, Brushes.White, 0, 595 - fontHeight - i*25);
                }

            }
            else
            {
                bitmap = new Bitmap(breedte, 100, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                if (regels.Count > 0)
                {
                    Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 1], new Font(FontFamily.GenericSansSerif, 14), Brushes.White, 0, 90 - fontHeight);
                    if (regels.Count > 1)
                    {
                        Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 2], new Font(FontFamily.GenericSansSerif, 14), Brushes.White, 0, 65 - fontHeight);
                        if (regels.Count > 2)
                        {
                            Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 3], new Font(FontFamily.GenericSansSerif, 14), Brushes.White, 0, 40 - fontHeight);
                        }
                    }
                }
            }
            return bitmap;
        }

    }
}
