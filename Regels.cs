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
            if (kaart != null)
            {

                /*
                if (stapel[stapel.Count - 1].Waarde == 2 || stapel[stapel.Count - 1].Kleur == 4)
                {
                    if (pakAantal != 0)
                    {
                        if (kaart.Waarde == 2 || kaart.Kleur == 4)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return true;
                }*/
                if (kaart.Kleur == stapel[stapel.Count - 1].Kleur ||
                    kaart.Waarde == stapel[stapel.Count - 1].Waarde ||
                    kaart.Kleur == 4 || //joker
                    kaart.Waarde == 11) //boer
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /*
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
         */

        public void kaartActie()
        {
            Kaart kaart = stapel[stapel.Count - 1];
            
            if (kaart.Kleur == 4) //joker
            {
                regelPakken(5);
                speciaal = 4;
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
                spelers[spelend].doeZet();
            }
            else if (kaart.Waarde == 11)
            {
                regelKleur();
            }
            else if (kaart.Waarde != 8)
            {
                volgende();
            }

            if (stapel[stapel.Count - 2].Waarde == 2 || stapel[stapel.Count - 2].Kleur == 4 && kaart.Waarde != 2 || kaart.Kleur != 4)
            {
                    regelEchtPakken(pakAantal);
                    pakAantal = 0;
            }
        }

        public void regelPakken(int aantal)
        {
            pakAantal += aantal;
        }

        public void regelEchtPakken(int pakAantal)
        {
            pakKaart(pakAantal);
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

        public void regelKleur()
        {
            if (spelend == 0)
            {
                kleur = new KleurKiezen();
                kleur.FormClosed += kleur_FormClosed;
            }
            else
            {
                volgende();
            }
        }

        void kleur_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            volgende();
        }

        public void regelDraai()
        {
            richting *= -1;
        }
    }
}
