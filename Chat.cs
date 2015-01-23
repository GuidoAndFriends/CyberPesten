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
                bitmap = new Bitmap(500, 500, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                int i = 0;
                foreach (String regel in regels)
                {
                    i++;
                    Graphics.FromImage(bitmap).DrawString(regels[regels.Count - i], new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 0 + i*20);
                }

            }
            else
            {
                bitmap = new Bitmap(500, 100, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                if (regels.Count > 0)
                {
                    Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 1], new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 0);
                    if (regels.Count > 1)
                    {
                        Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 2], new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 20);
                        if (regels.Count > 2)
                        {
                            Graphics.FromImage(bitmap).DrawString(regels[regels.Count - 3], new Font(FontFamily.GenericSansSerif, 14), Brushes.Black, 0, 40);
                        }
                    }
                }
            }
            return bitmap;
        }

    }
}
