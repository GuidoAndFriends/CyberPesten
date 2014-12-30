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
                //alles mag
                case 5:
                    speelbaar = true;
                    break;
                //veranderde kleur
                default:
                    if (kaart.Kleur == speciaal || kaart.Kleur == 4 || kaart.Waarde == 11)
                    {
                        speelbaar = true;
                    }
                    break;
             }
             return speelbaar;
        }

        public void kaartActie()
        {
            Kaart kaart = stapel[stapel.Count - 1];
            
            if (speciaal == 5)
            {
                speciaal = -1;
            }

            if (kaart.Kleur == 4) //joker
            {
                regelPakken(5);
            }
            else
            {
                switch (kaart.Waarde)
                {
                    case 2: regelPakken(2); break;
                    case 8: regelWacht(); break;
                    case 1: regelDraai(); break;
                }
            }
            
            if (kaart.Waarde == 7 || kaart.Waarde == 13)
            {
                //7 kleven, heer nog een keer
                spelers[spelend].doeZet();
            }
            else if (kaart.Waarde == 11)
            {
                //boer
                regelKleurEnVolgende();
            }
            else if (kaart.Waarde != 8)
            {
                //8 wacht, volgende is al afgehandeld
                volgende();
            }
        }

        public void regelPakken(int aantal)
        {
            pakAantal += aantal;
            speciaal = 4;
        }

        public void regelPakkenNu()
        {
            speciaal = 5;
            pakKaart(pakAantal);
            spelers[spelend].doeZet();
        }

        public void regelWacht()
        {
            //zou eventueel ook meerdere spelers kunnen overslaan
            
            Speler oud = spelers[spelend];

            spelend = (spelend + richting + spelers.Count) % (spelers.Count);

            if ((!(mens)) & spelend == 0)
            {
                spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            }

            oud.updateBlok();
            
            volgende();
        }

        public void regelKleurEnVolgende()
        {
            spelers[spelend].kiesKleurEnVolgende();
        }

        public void regelDraai()
        {
            richting *= -1;
        }
    }
}
