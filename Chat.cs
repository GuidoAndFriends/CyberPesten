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

        public Chat()
        {

        }

        public void nieuw(string regel)
        {
            regels.Add(regel);
            if (regels.Count > 1000)
            {
                regels.RemoveAt(0);
            }
        }

        public Bitmap maakBitmap(bool groot)
        {
            Bitmap bitmap;
            if (groot)
            {
                bitmap = new Bitmap(500, 300, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                //teken invouwknopje
                //teken meer regels
                //teken typveld
            }
            else
            {
                bitmap = new Bitmap(500, 100, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                //teken uitvouwknopje
                if (regels.Count > 0)
                {
                    Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 1], new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 0);
                }
                //teken drie regels
                //teken typveld
            }
            return bitmap;
        }
    }
}
