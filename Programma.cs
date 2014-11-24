using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Programma
    {
        static void Main()
        {
            Application.Run(new Menu());
        }

        /*
        public static Image achtergrond()
        {
            
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream file = thisExe.GetManifestResourceStream("groen.png");
            return Image.FromStream(file);
            

            
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream myStream = myAssembly.GetManifestResourceStream("groen.png");
            return new Bitmap(myStream);
            

            //object resource = System.Resources.ResourceManager.GetObject("groen");
            //object resource = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            //return (Image)resource;
        }
        */
    }
}
