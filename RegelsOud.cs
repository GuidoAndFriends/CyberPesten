using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    partial class Spel
    {
        /*
        public bool speelbaar(Kaart kaart)
        {
            bool speelbaar = false;
            switch (speciaal)
            {
                //geen speciale kaart
                case -1:
                    if (kaart.Kleur == stapel[stapel.Count - 1].Kleur || //zelfde kleur
                        kaart.Waarde == stapel[stapel.Count - 1].Waarde || //zelfde waarde
                        kaart.Kleur == 4 || //joker
                        kaart.Waarde == 11) //boer
                    {
                        speelbaar = true;
                    }
                    break;
                //2 of joker
                case 4:
                    if (kaart.Waarde == 2 || //2
                        kaart.Kleur == 4) //joker
                    {
                        speelbaar = true;
                    }
                    break;
                //alles mag
                case 5:
                    speelbaar = true;
                    break;
                //veranderde kleur (0, 1, 2 of 3)
                default:
                    if (kaart.Kleur == speciaal || //de gekozen kleur
                        kaart.Kleur == 4 || //joker
                        kaart.Waarde == 11) //boer
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
            //speciaal wordt op -1 gezet en daarna als dat nodig is (bij joker of 2) weer veranderd
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
                regelKleur();
            }
            else if (kaart.Waarde != 8)
            {
                //geen 7 of heer
                //geen boer, want daarbij wordt volgende al afgehandeld
                //geen 8 wacht, want daarbij wordt volgende al afgehandeld
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

        void regelWacht()
        {
            status += ". Acht wacht!";
            //Spelend wordt een speler verder gemaakt, maar zonder doeZet() van die speler aan te roepen
            Speler oud = spelers[spelend];
            spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            if ((!(mens)) & spelend == 0)
            {
                spelend = (spelend + richting + spelers.Count) % (spelers.Count);
            }
            oud.updateBlok();

            //Spelend wordt een speler verder gemaakt op de normale manier, dus met het aanroepen van doeZet() van die speler
            volgende();
        }

        void regelKleur()
        {
            spelers[spelend].kiesKleur();
            if (spelend != 0)
            {
                volgende();
            }
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

        public void regelPakkenNu()
        {
            System.Diagnostics.Debug.WriteLine(pakAantal.ToString());
            speciaal = 5;
            speciaalTekst = "5 alles mag";
            pakKaart(pakAantal);
            pakAantal = 0;
            spelers[spelend].doeZet();
        }
         */
    }
}
