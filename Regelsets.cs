using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPesten
{
    partial class Spel
    {
        bool isMagAltijd(Kaart kaart)
        {
            if (isPakkenMagAltijd(kaart) ||
                isKleurKiezenMagAltijd(kaart))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isPakkenMagAltijd(Kaart kaart)
        {
            if (kaart.Kleur == 4 && instellingen.regelsIngeschakeld.Contains(5))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        int isPakken(Kaart kaart)
        {
            if (kaart.Waarde == 2 && instellingen.regelsIngeschakeld.Contains(1))
            {
                return 2;
            }
            else if (kaart.Kleur == 4 && ((instellingen.regelset == 0 && instellingen.regelsIngeschakeld.Contains(6)) || (instellingen.regelset == 1 && instellingen.regelsIngeschakeld.Contains(5))))
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        bool isKleurKiezenMagAltijd(Kaart kaart)
        {
            if (kaart.Waarde == 11 && instellingen.regelsIngeschakeld.Contains(4))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool isWacht(Kaart kaart)
        {
            if ((kaart.Waarde == 8 && instellingen.regelsIngeschakeld.Contains(3)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isDraai(Kaart kaart)
        {
            if (kaart.Waarde == 1 && instellingen.regelsIngeschakeld.Contains(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool isWas(Kaart kaart)
        {
            if (kaart.Waarde == 10 && instellingen.regelsIngeschakeld.Contains(7))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isNogmaals(Kaart kaart)
        {
            if ((kaart.Waarde == 13 && instellingen.regelsIngeschakeld.Contains(6)) || (kaart.Waarde == 7 && instellingen.regelsIngeschakeld.Contains(2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool isOverslaan(Kaart kaart)
        {
            if (kaart.Waarde == 8 && instellingen.regelsIngeschakeld.Contains(3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void kaartActie0(bool volgendeAan)
        {
            Kaart kaart = stapel[stapel.Count - 1];
            //speciaal wordt op -1 gezet en daarna als dat nodig is (bij joker of 2) weer veranderd
            speciaal = -1;
            speciaalTekst = "-1 normaal";

            if (isPakken(kaart) > 0)
            {
                regelPakken(isPakken(kaart));
            }
            else if (isWacht(kaart))
            {
                regelWacht();
            }
            else if (isDraai(kaart))
            {
                regelDraai();
            }
            else if (isWas(kaart))
            {
                regelWas();
            }

            if (isNogmaals(kaart))
            {
                spelers[spelend].doeZet();
            }
            else if (isKleurKiezenMagAltijd(kaart))
            {
                regelKleur();
            }
            else if (!isOverslaan(kaart))
            {
                if (volgendeAan)
                {
                    volgende();
                }
            }
        }

        void kaartActie1(bool volgendeAan)
        {
            Kaart kaart = stapel[stapel.Count - 1];
            //speciaal wordt op -1 gezet en daarna als dat nodig is (bij joker of 2) weer veranderd
            speciaal = -1;
            speciaalTekst = "-1 normaal";

            if (isPakken(kaart) > 0)
            {
                regelPakken(isPakken(kaart));
            }
            else if (isWacht(kaart))
            {
                regelWacht();
            }
            else if (isDraai(kaart))
            {
                regelDraai();
            }
            else if (isWas(kaart))
            {
                regelWas();
            }

            if (isNogmaals(kaart))
            {
                spelers[spelend].doeZet();
            }
            else if (isKleurKiezenMagAltijd(kaart))
            {
                regelKleur();
            }
            else if (!isOverslaan(kaart))
            {
                if (volgendeAan)
                {
                    volgende();
                }
            }
        }

        /* REGELSET 0: standaardset
         * Regel 0
         * Aas (waarde 1)
         * Spelrichting draait om
         *
         * Regel 1
         * 2
         * Aantal te pakken kaarten wordt verhoogd met 2, volgende speler moet een pakkenkaart spelen of het aantal pakken waarna gespeeld mag worden
         *
         * Regel 2
         * 7
         * Huidige speler mag nog een keer spelen
         *
         * Regel 3
         * 8
         * Volgende speler wordt overgeslagen
         * 
         * Regel 4
         * Boer (J, waarde 11)
         * Huidige speler mag kiezen welke kleur bovenop deze kaart mag worden gelegd
         * 
         * Regel 6!
         * Heer
         * Huidige speler mag nog een keer spelen
         * 
         * Regel 5!
         * Joker (waarde 0, kleur 4)
         * Aantal te pakken kaarten wordt verhoogd met 5, volgende speler moet een pakkenkaart spelen of het aantal pakken waarna gespeeld mag worden
         * 
         * Regel 7 (standaard uitgeschakeld)
         * 10
         * Alle handen worden doorgegeven tegen de speelrichting in
         * 
         * Overig
         * Speler kan niet
         * 1 kaart pakken, mag gespeeld worden
         * 
         * Laatste kaart niet aangegeven
         * 5 kaarten pakken
         */

        /* REGELSET 1: Guido familie set
         * Regel 0
         * Aas (waarde 1)
         * Spelrichting draait om
         *
         * Regel 1
         * 2
         * Aantal te pakken kaarten wordt verhoogd met 2, volgende speler moet het aantal pakken en mag een pakkenkaart spelen
         *
          * Regel 2
         * 7
         * Huidige speler mag nog een keer spelen
         *
         * Regel 3
         * 8
         * Volgende speler wordt overgeslagen
         * 
         * Regel 4
         * Boer (J, waarde 11)
         * Huidige speler mag kiezen welke kleur bovenop deze kaart mag worden gelegd. Waarde wordt gelijk aan de waarde van de kaart eronder (is dat een boer of joker dan is er geen waarde)
         * 
         * Regel 5
         * Joker (waarde 0, kleur 4)
         * Aantal te pakken kaarten wordt verhoogd met 5, volgende speler moet het aantal pakken en mag een pakkenkaart spelen
         * 
         * Regel 6 (standaard uitgeschakeld)
         * Heer
         * Huidige speler mag nog een keer spelen
         * 
         * Regel 7 (standaard uitgeschakeld)
         * 10
         * Alle handen worden doorgegeven tegen de speelrichting in
         * 
         * Overig
         * Speler kan niet
         * 1 kaart pakken, als niet kan dan nog 1, mag daarna ook meer
         * 
         * Laatste kaart niet aangegeven
         * 5 kaarten pakken
         * 
         * Eerste kaart heeft wel effect
         * 
         * Uitkomen: Dit mag niet met een pestkaart: heb je op een gegeven moment 0 kaarten in je hand, zonder dat je uitbent (2 enige kaart in je hand, je voorganger legt een 2 op, jij legt jouw 2 op, vervolgens pak je 2) moet je tien kaarten pakken. Dus je kan niet een 2 opleggen op een andere 2 als dit je laatste kaart is.
         */
    }
}
