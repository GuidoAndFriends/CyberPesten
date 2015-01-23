using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CyberPesten
{
    class OnlineSpeelveld : Speelveld
    {
        public OnlineSpeelveld(Form form) { }

        new void startSpel(Instellingen instellingen)
        {
            Text = "CyberPesten: Online spel";
            spel = new OnlineSpel(this);
        }

        new void muisKlik(object sender, MouseEventArgs mea)
        {
            bool actie = false;
            if (laatsteKaartRect.Contains(mea.Location))
            {
                stuurActie("lK");
            }
            else if (eindeBeurtRect.Contains(mea.Location))
            {
                stuurActie("eV");
            }

            else if (spel.spelend == 0)
            {
                bool success = false;
                if (mea.X >= potPlek.X && mea.X <= potPlek.X + kaartBreedte && mea.Y >= potPlek.Y && mea.Y <= potPlek.Y + kaartHoogte)
                //pot
                {
                    if (spel.speciaal == 4)
                    {
                        stuurActie("rPN");
                    }
                    else
                    {
                        stuurActie("pK");
                        
                    }
                    success = true;
                }
                if (!success)
                {
                    foreach (Kaart kaart in spel.spelers[0].hand)
                    {
                        if (mea.X >= kaart.X && mea.X <= kaart.X + kaartBreedte && mea.Y >= kaart.Y && mea.Y <= kaart.Y + kaartHoogte)
                        {
                            //kaart in hand van speler
                            actie = true;
                            base.muisKlik(sender, mea);
                            if (kaart == spel.stapel[spel.stapel.Count - 1])
                            {
                                stuurActie("sK:" + spel.spelers[0].hand.IndexOf(kaart));
                                return;
                            }
                        }
                    }
                }
            }

            if (!actie) { base.muisKlik(sender, mea); }

            
        }

        new void muisWiel(object sender, MouseEventArgs mea)
        {
            int oldIndex = 0; Kaart k = spel.stapel[spel.stapel.Count - 1];
            if (spel.spelend == 0)
            {
                foreach (Kaart kaart in spel.spelers[0].hand)
                {
                    //kijkt of er op een kaart is geklikt
                    int deltaX = mea.X - kaart.X;
                    int deltaY = mea.Y - kaart.Y;
                    if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                    {
                        return;
                    }
                    oldIndex++;
                }
            }
            base.muisWiel(sender,mea);
            if (k == spel.stapel[spel.stapel.Count - 1])
            {
                stuurActie("sK:" + oldIndex);
            }
        }

        new void muisOmlaag(object sender, MouseEventArgs mea)
        {
            int index = 0;
            foreach (Kaart kaart in spel.spelers[0].hand)
            {
                if (kaart != null)
                {
                    //kijkt of er op een kaart is geklikt
                    int deltaX = mea.X - kaart.X;
                    int deltaY = mea.Y - kaart.Y;
                    if (deltaX >= 0 && deltaX <= kaartBreedte && deltaY >= 0 && deltaY <= kaartHoogte)
                    {
                        stuurActie("bK:" + index);
                        return;
                    }
                    index++;
                }
            }
            base.muisOmlaag(sender, mea);
        }


        new void muisOmHoog(object sender, MouseEventArgs mea)
        {
            Kaart k = spel.stapel[spel.stapel.Count - 1];
            base.muisOmhoog(sender, mea);
            if (k != spel.stapel[spel.stapel.Count - 1])
            {
                stuurActie("sK:"+spel.spelers[0].hand.Count);//omdat de hand 1 kleiner is geworden, en de kaart van count -1 kwam, klopt dit.
            }
        }

        new void schoppen_Click(object sender, EventArgs e)
        {
            stuurActie("kV:3");
            base.schoppen_Click(sender, e);
        }
        new void ruiten_Click(object sender, EventArgs e)
        {
            stuurActie("kV:2");
            base.ruiten_Click(sender, e);
        }
        new void harten_Click(object sender, EventArgs e)
        {
            stuurActie("kV:0");
            base.harten_Click(sender, e);
        }
        new void klaver_Click(object sender, EventArgs e)
        {
            stuurActie("kV:1");
            base.klaver_Click(sender, e);
        }



        bool stuurActie(string actie)
        {
            OnlineSpel os = (OnlineSpel)spel;
            os.actieCount++;
            string raw = Online.PHPrequest("http://harbingerofme.info/GnF/new_action.php", new string[] { "name", "token", "gameid","actie"}, new string[] { Online.username, Online.token, Online.game.ToString(), actie});
            return raw == "ja";
        }
    }
}
