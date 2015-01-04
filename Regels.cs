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

        void kaartActie()
        {
            Kaart kaart = stapel[stapel.Count - 1];
           
            speciaal = -1;
            speciaalTekst = "-1 normaal";

        switch (kaart.Waarde)
            {
                case 0: regelPakken(5); break; //joker
                case 2: regelPakken(2); break;
                case 8: regelWacht(); break;
                case 1: regelDraai(); break;
                //case 10: regelWas(); break;
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

        void regelPakken(int aantal)
        {
            pakAantal += aantal;
            status += " en het totaal is nu " + pakAantal;
            speciaal = 4;
            speciaalTekst = "4 alleen joker of 2";
        }

        public void regelPakkenNu()
        {
            System.Diagnostics.Debug.WriteLine(pakAantal.ToString());
            speciaal = 5;
            speciaalTekst = "5 alles mag";
            pakKaart(pakAantal);
            pakAantal = 0;
            spelers[spelend].doeZet();
        }

        void regelWacht()
        {          
            Speler oud = spelers[spelend];

            spelend = (spelend + richting + spelers.Count) % (spelers.Count);

            if ((!(mens)) & spelend == 0)
            {
                spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            }

            oud.updateBlok();
            
            volgende();
        }

        void regelKleurEnVolgende()
        {
            spelers[spelend].kiesKleurEnVolgende();
        }

        void regelDraai()
        {
            richting *= -1;
        }

        void regelWas()
        {
            if (richting == 1)
            {
                //spelen met klok mee, draaien tegen klok in
                List<Kaart> tijdelijk = spelers[1].hand;
                for (int i = 1; i < aantalSpelers - 2; i++)
                {
                    spelers[i].hand = spelers[i + 1].hand;
                }
                if (mens)
                {
                    spelers[aantalSpelers - 1].hand = spelers[0].hand;
                    spelers[0].hand = tijdelijk;
                }
                else
                {
                    spelers[1].hand = tijdelijk;
                }
            }
            else
            {
                //spelen tegen klok in, draaien met klok mee
                List<Kaart> tijdelijk = spelers[aantalSpelers - 1].hand;
                for (int i = aantalSpelers - 1; i > 0; i--)
                {
                    spelers[i].hand = spelers[i - 1].hand;
                }
                if (mens)
                {
                    spelers[1].hand = spelers[0].hand;
                    spelers[0].hand = tijdelijk;
                }
                else
                {
                    spelers[1].hand = tijdelijk;
                }
            }
        }
    }
}
