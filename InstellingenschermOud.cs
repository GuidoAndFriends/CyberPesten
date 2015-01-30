using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.IO;

namespace CyberPesten
{
    class InstellingenschermOud : Form
    {
        Instellingen instellingen;
        NumericUpDown regelsetCon, aantalSpelersCon, achterkantCon;
        TextBox regelsIngeschakeldCon, AIIngeschakeldCon;
        Button mensSpelendCon, geluidCon;
        Label regelsIngeschakeldLab2;
        Menu menu;

        public InstellingenschermOud(Menu menu)
        {
            this.menu = menu;
            instellingen = menu.instellingen;

            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Achtergrond");
            ClientSize = menu.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            Label regelsetLab = new Label();
            regelsetLab.Text = "Regelset";
            regelsetLab.BackColor = Color.Transparent;
            regelsetLab.Size = new Size(150, 40);
            regelsetLab.Location = new Point(50, 50);
            Controls.Add(regelsetLab);

            regelsetCon = new NumericUpDown();
            regelsetCon.Value = instellingen.regelset;
            regelsetCon.Location = new Point(220, 50);
            regelsetCon.ValueChanged += regelset;
            Controls.Add(regelsetCon);

            Label regelsIngeschakeldLab = new Label();
            regelsIngeschakeldLab.Text = "Ingeschakelde regels (0,1,2,3)";
            regelsIngeschakeldLab.BackColor = Color.Transparent;
            regelsIngeschakeldLab.Size = new Size(150, 40);
            regelsIngeschakeldLab.Location = new Point(50, 150);
            Controls.Add(regelsIngeschakeldLab);

            regelsIngeschakeldCon = new TextBox();
            regelsIngeschakeldCon.Location = new Point(220, 150);
            regelsIngeschakeldCon.TextChanged += regelsIngeschakeld;
            Controls.Add(regelsIngeschakeldCon);

            regelsIngeschakeldLab2 = new Label();
            regelsIngeschakeldLab2.BackColor = Color.Transparent;
            regelsIngeschakeldLab2.Size = new Size(400, 40);
            regelsIngeschakeldLab2.Location = new Point(400, 150);
            Controls.Add(regelsIngeschakeldLab2);

            Label aantalSpelersLab = new Label();
            aantalSpelersLab.Text = "Aantal spelers";
            aantalSpelersLab.BackColor = Color.Transparent;
            aantalSpelersLab.Size = new Size(150, 40);
            aantalSpelersLab.Location = new Point(50, 250);
            Controls.Add(aantalSpelersLab);

            aantalSpelersCon = new NumericUpDown();
            aantalSpelersCon.Location = new Point(220, 250);
            aantalSpelersCon.DecimalPlaces = 0;
            aantalSpelersCon.ValueChanged += aantalSpelers;
            Controls.Add(aantalSpelersCon);

            Label AIIngeschakeldLab = new Label();
            AIIngeschakeldLab.Text = "Ingeschakelde AI (0,2,3)";
            AIIngeschakeldLab.BackColor = Color.Transparent;
            AIIngeschakeldLab.Size = new Size(150, 40);
            AIIngeschakeldLab.Location = new Point(50, 350);
            Controls.Add(AIIngeschakeldLab);

            AIIngeschakeldCon = new TextBox();
            AIIngeschakeldCon.Location = new Point(220, 350);
            AIIngeschakeldCon.TextChanged += AIIngeschakeld;
            Controls.Add(AIIngeschakeldCon);

            Label mensSpelendLab = new Label();
            mensSpelendLab.Text = "Menselijke speler";
            mensSpelendLab.BackColor = Color.Transparent;
            mensSpelendLab.Size = new Size(150, 40);
            mensSpelendLab.Location = new Point(50, 450);
            Controls.Add(mensSpelendLab);

            mensSpelendCon = new Button();
            mensSpelendCon.Location = new Point(220, 450);
            mensSpelendCon.Click += mensSpelend;
            Controls.Add(mensSpelendCon);

            Label geluidLab = new Label();
            geluidLab.Text = "Geluid";
            geluidLab.BackColor = Color.Transparent;
            geluidLab.Size = new Size(150, 40);
            geluidLab.Location = new Point(50, 550);
            Controls.Add(geluidLab);

            geluidCon = new Button();
            geluidCon.Location = new Point(220, 550);
            geluidCon.Click += geluid;
            Controls.Add(geluidCon);

            Label achterkantLab = new Label();
            achterkantLab.Text = "Achterkant design";
            achterkantLab.BackColor = Color.Transparent;
            achterkantLab.Size = new Size(150, 40);
            achterkantLab.Location = new Point(50, 650);
            Controls.Add(achterkantLab);

            achterkantCon = new NumericUpDown();
            achterkantCon.Location = new Point(220, 650);
            achterkantCon.DecimalPlaces = 0;
            achterkantCon.ValueChanged += achterkant;
            Controls.Add(achterkantCon);

            Button resetCon = new Button();
            resetCon.Text = "Standaard instellingen";
            resetCon.Size = new Size(150, 40);
            resetCon.Location = new Point(50, 750);
            resetCon.Click += reset;
            Controls.Add(resetCon);

            Button terugCon = new Button();
            terugCon.Text = "Terug naar menu";
            terugCon.Size = new Size(150, 40);
            terugCon.Location = new Point(50, 850);
            terugCon.Click += terug;
            Controls.Add(terugCon);

            update();
            this.Show();
        }

        void regelset(object sender, EventArgs ea)
        {
            if (regelsetCon.Value < 0)
            {
                regelsetCon.Value = 0;
            }
            else if (regelsetCon.Value > 1)
            {
                regelsetCon.Value = 1;
            }
            instellingen.regelset = (int)regelsetCon.Value;
            instellingen.schrijven();
            update();
        }

        void regelsIngeschakeld(object sender, EventArgs ea)
        {
            string[] delen = regelsIngeschakeldCon.Text.Split(new char[] { ',' });
            instellingen.regelsIngeschakeld = new List<int>();
            if (delen[0] != "")
            {
                for (int i = 0; i < delen.Length; i++)
                {
                    instellingen.regelsIngeschakeld.Add(Int32.Parse(delen[i]));
                }
            }
            instellingen.schrijven();
        }

        void aantalSpelers(object sender, EventArgs ea)
        {
            if (aantalSpelersCon.Value < 2)
            {
                aantalSpelersCon.Value = 2;
            }
            else if (aantalSpelersCon.Value > 7)
            {
                aantalSpelersCon.Value = 7;
            }
            instellingen.aantalSpelers = (int)aantalSpelersCon.Value;
            instellingen.schrijven();
        }

        void AIIngeschakeld(object sender, EventArgs ea)
        {
            string[] delen = AIIngeschakeldCon.Text.Split(new char[] { ',' });
            instellingen.AIIngeschakeld = new List<int>();

            for (int i = 0; i < delen.Length; i++)
            {
                if (delen[i] != "")
                {
                    instellingen.AIIngeschakeld.Add(Int32.Parse(delen[i]));
                }
            }
            instellingen.schrijven();
        }

        private void buttonSound()
        {
geluid.klikSound()
        }

        void mensSpelend(object sender, EventArgs ea)
        {
            buttonSound();
            if (instellingen.mensSpelend)
            {
                instellingen.mensSpelend = false;
                mensSpelendCon.Text = "Uit";
            }
            else
            {
                instellingen.mensSpelend = true;
                mensSpelendCon.Text = "Aan";
            }
            instellingen.schrijven();
        }

        void geluid(object sender, EventArgs ea)
        {
	    buttonSound();
            if (instellingen.geluid)
            {
                instellingen.geluid = false;
                geluidCon.Text = "Uit";
            }
            else
            {
                instellingen.geluid = true;
                geluidCon.Text = "Aan";
            }
            instellingen.schrijven();
        }

        void achterkant(object sender, EventArgs ea)
        {
            if (achterkantCon.Value < 0)
            {
                achterkantCon.Value = 0;
            }
            else if (achterkantCon.Value > 2)
            {
                achterkantCon.Value = 2;
            }
            instellingen.achterkant = (int)achterkantCon.Value;
            instellingen.schrijven();
        }

        void reset(object sender, EventArgs ea)
        {
            buttonSound();
            instellingen.standaard();
            update();
        }


        void update()
        {
            regelsetCon.Value = instellingen.regelset;

            string regel = "";
            if (instellingen.regelsIngeschakeld != null)
            {
                if (instellingen.regelsIngeschakeld.Count != 0)
                {
                    regel += instellingen.regelsIngeschakeld[0].ToString();
                    for (int i = 1; i < instellingen.regelsIngeschakeld.Count; i++)
                    {
                        regel += "," + instellingen.regelsIngeschakeld[i].ToString();
                    }
                }
            }
            regelsIngeschakeldCon.Text = regel;

            if (instellingen.regelset == 0)
            {
                regelsIngeschakeldLab2.Text = "0 = Aas draai, 1 = 2 2 pakken, 2 = 7 kleven, 3 = 8 wacht, 4 = boer kleur kiezen, 5 = heer nog een keer, 6 = joker 5 pakken, 7 = 10 wasmaschien";
            }
            else if (instellingen.regelset == 1)
            {
                regelsIngeschakeldLab2.Text = "0 = Aas draai, 1 = 2 2 pakken, 2 = 7 wacht, 3 = 8 kleven, 4 = boer kleur kiezen, 5 = joker 5 pakken";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("De gekozen regelset is nog niet geimplementeerd in update() in InstellingenschermOud.cs");
            }

            aantalSpelersCon.Value = instellingen.aantalSpelers;

            regel = "";
            if (instellingen.AIIngeschakeld != null)
            {
                if (instellingen.AIIngeschakeld.Count != 0)
                {
                    regel += instellingen.AIIngeschakeld[0].ToString();
                    for (int i = 1; i < instellingen.AIIngeschakeld.Count; i++)
                    {
                        regel += "," + instellingen.AIIngeschakeld[i].ToString();
                    }
                }
            }
            AIIngeschakeldCon.Text = regel;

            if (instellingen.mensSpelend)
            {
                mensSpelendCon.Text = "Aan";
            }
            else
            {
                mensSpelendCon.Text = "Uit";
            }

            if (instellingen.geluid)
            {
                geluidCon.Text = "Aan";
            }
            else
            {
                geluidCon.Text = "Uit";
            }

            achterkantCon.Value = instellingen.achterkant;
        }

        void terug(object sender, EventArgs ea)
        {
            buttonSound();
            menu.Show();
            this.Close();
        }
    }
}