using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CyberPesten
{
    class inlogScherm :  Form
    {
        public inlogScherm()
        {
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);
            DoubleBuffered = true;

            this.Show();
        }
    }
    class Online
    {
        

    }
}
