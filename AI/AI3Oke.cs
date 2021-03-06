﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class AI3Oke : Speler
    {
        List<Speler> spelers;
        int spelend, richting;
        bool mens;

        public AI3Oke(Spel spel, string naam)
        {
            achterkant = new Bitmap(110, 153, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            string achterkantDesign;
            if (spel.instellingen.achterkant == 0)
            {
                achterkantDesign = "Back_design_1";
            }
            else if (spel.instellingen.achterkant == 1)
            {
                achterkantDesign = "Back_design_2";
            }
            else
                achterkantDesign = "Back_design_3";
            Graphics.FromImage(achterkant).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject(achterkantDesign), 0, 0, 110, 153);

            hand = new List<Kaart>();
            this.spel = spel;
            this.naam = naam;
            blok = new System.Drawing.Bitmap(10, 10);
            spelers = spel.spelers;
            spelend = spel.spelend;
            richting = spel.richting;
            mens = spel.mens;
        }

        public override void doeZet()
        {
            bezig = true;
            spelers = spel.spelers;
            spelend = spel.spelend;
            richting = spel.richting;
            mens = spel.mens;
            

            List<Kaart> mogelijk = new List<Kaart>();
            List<Kaart> pester = new List<Kaart>();
            List<Kaart> bonus = new List<Kaart>();
            List<Kaart> saai = new List<Kaart>();

            int volgendeIndex = (spelend + richting + spelers.Count) % (spelers.Count);

            if ((!(mens)) & volgendeIndex == 0)
            {
                volgendeIndex = (volgendeIndex + richting + spelers.Count) % (spelers.Count);
            }
            Speler volgende = spelers[volgendeIndex];

            foreach (Kaart kaart in hand)
            {
                if (spel.speelbaar(kaart))
                {
                    mogelijk.Add(kaart);
                }
            }

            foreach (Kaart kaart in mogelijk)
            {
                if (kaart.Waarde == 2) 
                {
                    pester.Add(kaart);
                }

                if (spel.isDraai(kaart))
                {
                    pester.Add(kaart);
                }

                if (spel.isWacht(kaart))
                {
                    pester.Add(kaart);
                }

                if (spel.isPakkenMagAltijd(kaart))
                {
                    pester.Add(kaart);
                }
            }

            foreach (Kaart kaart in mogelijk)
            {
                if (spel.isNogmaals(kaart))
                {
                    bonus.Add(kaart);
                }
            }

            foreach (Kaart kaart in mogelijk)
            {
                if ((!(spel.isNogmaals(kaart))) && (!(spel.isPakkenMagAltijd(kaart))) && (!(spel.isDraai(kaart))) && (!(spel.isWacht(kaart))))
                {
                    saai.Add(kaart);
                }
            }

            if (mogelijk.Count > 0)
            {
                if (pester.Count > 0 && volgende.hand.Count < 3)
                {
                        spel.speelKaart(pester[0]);
                }
                else
                {
                    if (bonus.Count > 0)
                    {
                        spel.speelKaart(bonus[0]);
                    }                    
                    
                    else
                    {
                        if (saai.Count > 0)
                        {
                            spel.speelKaart(saai[0]);
                        }

                        else
                        {
                            spel.speelKaart(mogelijk[0]);
                        } 
                    }                   
                }
            }

            else
            {
                spel.pakKaart();
            }
            bezig = false;
        }
    }
}

