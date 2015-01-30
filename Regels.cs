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
                    if (kaart.Kleur == stapel[stapel.Count - 1].Kleur || //zelfde kleur
                        kaart.Waarde == stapel[stapel.Count - 1].Waarde || //zelfde waarde
                        isMagAltijd(kaart))
                    {
                        speelbaar = true;
                    }
                    break;
                //kaart waarbij gepakt moet worden
                case 4:
                    if (isPakken(kaart) > 0)
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
                        isMagAltijd(kaart))
                    {
                        speelbaar = true;
                    }
                    break;
            }
            return speelbaar;
        }

        void kaartActie(bool volgendeAan = true)
        {
            if (instellingen.regelset == 0)
            {
                kaartActie0(volgendeAan);
            }
            else if (instellingen.regelset == 1)
            {
                kaartActie1(volgendeAan);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Regelset is nog niet geimplementeerd in kaartActie() in Regels.cs");
            }
        }

        void regelPakken(int aantal)
        {
            pakAantal += aantal;
            chat.nieuw("Het aantal kaarten dat gepakt moet worden is nu " + pakAantal);
            speciaal = 4;
            speciaalTekst = "4 alleen joker of 2";
        }

        void regelWacht()
        {
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
                List<Kaart> tijdelijk = nieuweHand(spelers[1].hand);
                for (int i = 1; i < aantalSpelers - 2; i++)
                {
                    spelers[i].hand = nieuweHand(spelers[i + 1].hand);
                }
                if (mens)
                {
                    spelers[aantalSpelers - 1].hand = nieuweHand(spelers[0].hand);
                    spelers[0].hand = nieuweHand(tijdelijk);
                }
                else
                {
                    spelers[aantalSpelers - 1].hand = nieuweHand(tijdelijk);
                }
            }
            else
            {
                //spelen tegen klok in, draaien met klok mee
                List<Kaart> tijdelijk = nieuweHand(spelers[aantalSpelers - 1].hand);
                for (int i = aantalSpelers - 1; i > 1; i--)
                {
                    spelers[i].hand = nieuweHand(spelers[i - 1].hand);
                }
                if (mens)
                {
                    spelers[1].hand = nieuweHand(spelers[0].hand);
                    spelers[0].hand = nieuweHand(tijdelijk);
                }
                else
                {
                    spelers[1].hand = nieuweHand(tijdelijk);
                }
            }
        }


        public List<Kaart> nieuweHand(List<Kaart> oude)
        {
            List<Kaart> hand = new List<Kaart>();
            foreach (Kaart kaart in oude)
            {
                hand.Add(kaart);
            }
            return hand;
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
    }
}
