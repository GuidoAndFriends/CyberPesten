using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CyberPesten
{
    public class Kaart
    {
        private int kleur; //integere getal van de kleur, 0 = harten, 1 = klaver, 2 = ruiten, 3= schoppen (alfabetische volgorde) en 4 = joker (kleurloos)
        private int waarde; //waarde van de kaart, 0 doet niet mee, 1 is een aas, 2-10 komen overeen met de nummers zelf, 11 boer, 12 vrouw, 13 heer.
        public int X, Y;
        public Size kaartGrootte = new Size(110, 153);

        public Kaart(int k, int w)//maakt een nieuwe kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        {
            X = 0;
            Y = 600;
            if (k > -1 && k < 5 && w > 0 && w < 14)
            {
                kleur = k;
                waarde = w;
            }
            else //test van kaj
            {
                throw new ArgumentOutOfRangeException("Kaart: argument out of range. k was: " + k + " and w was: " + w + ".");
            }
        }

        public Kaart(int k)//maakt een nieuwe joker
        {
            if (k == 4)
            {
                kleur = 4;
                waarde = 0;
            }
            else
            {
                throw new ArgumentException("Ongeldige waarde, deze methode accepteerd alleen maar 4 als parameter");
            }
        }

        public int Kleur //maak of verkrijgt de kleur van een kaart, bij een ongeldige waarde wordt er een ArgumentOutOfRangeException gegooid
        {
            get { return kleur; }
            set
            {
                if (value > -1 && value < 5)
                {
                    kleur = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Kleur niet binnen accepteerbare normen.");
                }
            }
        }

        public int Waarde
        {
            get { return waarde; }
            set
            {
                if (value > 0 && value < 14)
                {
                    waarde = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Waarde niet binnen accepteerbare normen.");
                }
            }
        }

        public string tekst//geeft een geschreven versie van een kaart (niet dat we die nodig hebben, maar het is fijn om te hebben)
        {
            get
            {
                string a="";
                switch (kleur)
                {
                    case 0: a = "Harten ";   break;
                    case 1: a = "Klaver ";   break;
                    case 2: a = "Ruiten ";   break;
                    case 3: a = "Schoppen "; break;
                    case 4: a = "Joker";     break;
                }
                if (kleur != 4)
                {
                    switch (waarde)
                    {
                        case 1: a += "Aas";         break;
                        case 11: a += "Boer";       break;
                        case 12: a += "Vrouw";      break;
                        case 13: a += "Heer";       break;
                        default: a += "" + waarde;  break;
                    }
                }
                return a;
            }
        }

        public string tekst2 //geeft een tweede geschreven versie van een kaart
        {
            get
            {
                string a = "";
                if (kleur != 4)
                {
                    switch (waarde)
                    {
                        case 1: a = "Aas"; break;
                        case 11: a = "Boer"; break;
                        case 12: a += "Vrouw"; break;
                        case 13: a += "Heer"; break;
                        default: a += "" + waarde; break;
                    }
                }
                switch (kleur)
                {
                    case 0: a = "Harten "; break;
                    case 1: a = "Klaver "; break;
                    case 2: a = "Ruiten "; break;
                    case 3: a = "Schoppen "; break;
                    case 4: a = "Joker"; break;
                }
                return a;
            }
        }

        public Bitmap voorkant
        {
            get
            {
                Bitmap b = new Bitmap(110, 153);
                Graphics gr = Graphics.FromImage(b);
                if (kleur == 4) //joker
                {
                    b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Joker"), kaartGrootte);
                }
                else
                {
                    switch (waarde)
                    {
                        case 1: //Azen
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("HA"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("CA"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("DA"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("SA"), kaartGrootte); break;
                            } break;

                        case 2:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H2"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C2"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D2"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S2"), kaartGrootte); break;
                            } break;

                        case 3:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H3"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C3"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D3"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S3"), kaartGrootte); break;
                            } break;

                        case 4:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H4"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C4"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D4"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S4"), kaartGrootte); break;
                            } break;

                        case 5:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H5"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C5"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D5"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S5"), kaartGrootte); break;
                            } break;

                        case 6:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H6"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C6"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D6"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S6"), kaartGrootte); break;
                            } break;

                        case 7:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H7"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C7"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D7"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S7"), kaartGrootte); break;
                            } break;

                        case 8:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H8"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C8"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D8"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S8"), kaartGrootte); break;
                            } break;

                        case 9:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H9"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C9"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D9"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S9"), kaartGrootte); break;
                            } break;

                        case 10:
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("H10"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("C10"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("D10"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("S10"), kaartGrootte); break;
                            } break;

                        case 11: //Boeren
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("HJ"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("CJ"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("DJ"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("SJ"), kaartGrootte); break;
                            } break;

                        case 12: //Vrouwen
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("HQ"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("CQ"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("DQ"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("SQ"), kaartGrootte); break;
                            } break;

                        case 13: //Heren
                            switch (kleur)
                            {
                                case 0: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("HK"), kaartGrootte); break;
                                case 1: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("CK"), kaartGrootte); break;
                                case 2: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("DK"), kaartGrootte); break;
                                case 3: b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("SK"), kaartGrootte); break;
                            } break;
                    }
                }
                return b;
            }
        }

        public Bitmap achterkant
        {
            get
            {
                Bitmap b = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Back_design_2"), kaartGrootte);
                Graphics gr = Graphics.FromImage(b);
                return b;
            }
        }
    }
}
