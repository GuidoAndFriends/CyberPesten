using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CyberPesten
{
    class Help : Form
    {
        Bitmap terugBitmap, achtergrond;
        float verhouding;
        bool terugHover;
        Rectangle terugButton, maat, bounds;
        public Spel spel;
        Form menuBack;
        string tekst;

        public Help(Form form)
        {
            Text = "CyberPesten: Help";
            maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            this.Show();
            menuBack = form;

            achtergrond = new Bitmap(maat.Width, maat.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics.FromImage(achtergrond).DrawImage((Bitmap)CyberPesten.Properties.Resources.ResourceManager.GetObject("Help_achtergrond"), maat);
            BackgroundImage = achtergrond;

            terugBitmap = new Bitmap((Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("Terug_button"));
            verhouding = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / terugBitmap.Width;
            terugButton = new Rectangle(0, (int)(verhouding * 53), (int)(verhouding * 234), (int)(verhouding * 74));
            bounds = new Rectangle((int)(400 * verhouding), (int)(280 * verhouding), (int)(1120 * verhouding), (int)(600 * verhouding));

            this.Paint += this.buildAchtergrond;
            this.Paint += this.selected;
            this.Paint += this.tekenTekst;
            this.MouseMove += this.hover;
            this.MouseClick += this.klik;
        }

        private void buildAchtergrond(object sender, PaintEventArgs pea)
        {
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            pea.Graphics.DrawImage(BackgroundImage, 0, 0);
            pea.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }

        void tekenTekst(object sender, PaintEventArgs pea)
        {
            // voorlopige tekst, is nog lang niet af
            
            tekst = "Cyberpesten is een kaartspel.";
            tekst += "\nDe bedoeling van het spel is om zo snel mogelijk de kaarten in je hand kwijt te raken. Dit kun je doen door op een geldige kaart in je hand te klikken, jouw beurt is dan voorbij en de andere spelers leggen nu hun kaarten op. Iedereen krijgt om en om een beurt, maar pas op voor pestkaarten, deze kunnen gevolgen hebben op jouw spel";
            tekst += "\n\nWat is een geldige zet?";
            tekst += "\nEen kaart van dezelfde soort als de bovenste kaart op de oplegstapel is altijd een geldige kaart, maar je kunt ook het type van de kaarten die gespeeld worden veranderen door een kaart met hetzelfde nummer als de bovenste kaart van speelstapel op de speelstapel te leggen";
            tekst += "\n\nWelke pestkaarten zijn er?";
            tekst += "\nnog uit te werken";
            tekst += "\n...";
            tekst += "0 = harten, 1 = klaver, 2 = ruiten, 3 = schoppen (alfabetische volgorde) en 4 = joker (kleurloos)";
            tekst += "0 doet niet mee, 1 is een aas, 2-10 komen overeen met de nummers zelf, 11 boer, 12 vrouw, 13 heer";

            pea.Graphics.DrawString(tekst, new Font("Arial", 12), new SolidBrush(Color.White), bounds);
            
        }

        private void selected(object sender, PaintEventArgs pea)
        {
            if (terugHover)
            {
                pea.Graphics.DrawImage(terugBitmap, maat);
            }

            Invalidate();
        }

        private void hover(object sender, MouseEventArgs mea)
        {
            if (terugButton.Contains(mea.Location))
            {
                terugHover = true;
            }
            else
            {
                terugHover = false;
            }
            Invalidate();
        }

        private void klik(object sender, MouseEventArgs mea)
        {
            if (terugHover)
            {
                menuBack.Show();
                this.Close();
                if (spel != null)
                {
                    if (!spel.spelers[spel.spelend].bezig)
                    {
                        spel.spelers[spel.spelend].doeZet();
                    }
                }
            }
        }

    }
}
