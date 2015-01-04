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
        public Help(Form form)
        {
            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            Size = form.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            Paint += teken;
            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            // voorlopige tekst, is nog lang niet af
            string tekst = "Cyberpesten is een kaartspel.";
            tekst += "\nDe bedoeling van het spel is om zo snel mogelijk de kaarten in je hand kwijt te raken. Dit kun je doen door op een geldige kaart in je hand te klikken, jouw beurt is dan voorbij en de andere spelers leggen nu hun kaarten op. Iedereen krijgt om en om een beurt, maar pas op voor pestkaarten, deze kunnen gevolgen hebben op jouw spel";
            tekst += "\n\nWat is een geldige zet?";
            tekst += "\nEen kaart van dezelfde soort als de bovenste kaart op de oplegstapel is altijd een geldige kaart, maar je kunt ook het type van de kaarten die gespeeld worden veranderen door een kaart met hetzelfde nummer als de bovenste kaart van speelstapel op de speelstapel te leggen";
            tekst += "\n\nWelke pestkaarten zijn er?";
            tekst += "\nnog uit te werken";
            tekst += "\n...";
            tekst += "0 = harten, 1 = klaver, 2 = ruiten, 3 = schoppen (alfabetische volgorde) en 4 = joker (kleurloos)";
            tekst += "0 doet niet mee, 1 is een aas, 2-10 komen overeen met de nummers zelf, 11 boer, 12 vrouw, 13 heer";

            Label label = new Label();
            label.BackColor = Color.Transparent;
            label.Text = tekst;
            label.Font = new Font(FontFamily.GenericSansSerif, 16);
            label.Size = Size;
            label.Location = new Point(10, 10);
            Controls.Add(label);

          
        }
    }
}
