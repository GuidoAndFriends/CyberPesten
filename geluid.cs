using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace CyberPesten
{
    public static class geluid
    {



        public static void schudSound()
        {
            play(CyberPesten.Properties.Resources.schudden);
        }

        public static void winSound()
        {
            play(CyberPesten.Properties.Resources.win);
        }

        public static void pechSound()
        {
            play(CyberPesten.Properties.Resources.helaas);
        }

        public static void errorSound()
        {
            play(CyberPesten.Properties.Resources.fout);
        }

        public static void klikSound()
        {
            play(CyberPesten.Properties.Resources.button);
        }

        public static void kaartSound()
        {
            play(CyberPesten.Properties.Resources.playcard);
        }


        private static void play(Stream s)
        {
            if (new Instellingen().geluid)
            {
                SoundPlayer geil = new SoundPlayer(s);
                geil.Play();
            }
        }
    }
}
