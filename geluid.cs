using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace CyberPesten
{
    new /*jemoeder*/ public static class geluid
    {



        public static void schudSound()
        {
            geluid(CyberPesten.Properties.Resources.schudden);
        }

        public static void winSound()
        {
            geluid(CyberPesten.Properties.Resources.win);
        }

        public static void pechSound()
        {
            geluid(CyberPesten.Properties.Resources.helaas)
        }

        public static void errorSound()
        {
            geluid(CyberPesten.Properties.Resources.fout);
        }

        public static void klikSound()
        {
            geluid(CyberPesten.Properties.Resources.button);
        }

        public static void kaartSound()
        {
            geluid(CyberPesten.Properties.Resources.playcard);
        }


        private static void geluid(Stream s)
        {
            if (new Instellingen().geluid)
            {
                SoundPlayer geil = new SoundPlayer(s);
                geil.Play();
            }
        }
    }
}
