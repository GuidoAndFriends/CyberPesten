using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    partial class Spel
    {
        public bool speelbaar(Kaart kaart)
        {
            bool speelbaar = false;
            switch (speciaal)
            {
                //geen speciale kaart
                case -1:
                    if (kaart.Kleur == stapel[stapel.Count - 1].Kleur ||
                        kaart.Waarde == stapel[stapel.Count - 1].Waarde ||
                        kaart.Kleur == 4 || //joker
                        kaart.Waarde == 11) //boer
                    {
                        speelbaar = true;
                    }
                    break;
                //2 of joker
                case 4:
                    if (kaart.Waarde == 2 || kaart.Kleur == 4)
                    {
                        speelbaar = true;
                    }
                    break;
                //veranderde kleur
                default:
                    if (kaart.Kleur == speciaal || kaart.Kleur == 4 || kaart.Waarde == 11)
                    {
                        speelbaar = true;
                        speciaal = -1; //moet waarschijnlijk pas tijdens het spelen van de kaart
                    }
                    break;
             }
             return speelbaar;
        }

        public void kaartActie()
        {
            Kaart kaart = stapel[stapel.Count - 1];
            if (kaart.Kleur == 4) //joker
            {
                regelPakken(5);
            }
            else
            {
                switch (kaart.Waarde)
                {
                    case 2: regelPakken(2); break;
                    case 7: regelKleven(); break;
                    case 8: regelWacht(); break;
                    case 11: regelKleur(); break;
                    case 1: regelDraai(); break;
                }
            }
        }

        public void regelPakken(int aantal)
        {
            pakAantal += aantal;
            speciaal = 4;
        }

        public void regelKleven()
        {
            spelend -= richting;
        }

        public void regelWacht()
        {
            //zou eventueel ook meerdere spelers kunnen overslaan
            volgende();
        }

        public void regelKleur()
        {
            //speler een kleur laten kiezen
            //speciaal = gekozen kleur
        }

        public void regelDraai()
        {
            richting *= -1;
        }
    }
}
