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
        int y;
        List<string> tekst;

        public Help()
        {
            Text = "CyberPesten: Help";
            BackgroundImage = (Image)CyberPesten.Properties.Resources.ResourceManager.GetObject("groen");
            ClientSize = new Size(1000, 800);
            Paint += teken;
            this.Show();
        }

        private void teken(object sender, PaintEventArgs pea)
        {
            Graphics graphics = pea.Graphics;
            tekst = new List<string>();
            // voorlopige tekst, is nog lang niet af
            tekst.Add("Help");
            tekst.Add("");
            tekst.Add("Cyberpesten is een kaartspel.");
            tekst.Add("De bedoeling van het spel is om zo snel mogelijk de kaarten in je hand kwijt te raken. Dit kan je doen door op een geldige kaart in je hand te klikken, jouw beurt is dan voorbij en de computer legt nu zijn kaart op. Iedereen krijgt om en om een beurt, maar pas op voor spelkaarten, deze kunnen gevolgen hebben op jou spel");
            tekst.Add("");
            tekst.Add("Wat is een geldige zet?");
            tekst.Add("Een kaart van dezelfde soort als de bovenwste kaart op de speelstapel is altijd een geldige kaart, maar je kan ook het type an de kaarten die gespeeld worden veranderen door een kaart met hetzelfde nummer als de bovenste kaart van speelstapel op de speelstapel te leggen");
            tekst.Add("");
            tekst.Add("Welke pestkaarten zijn er?");
            tekst.Add("nog uit te werken");

            y = 10;
            foreach(string stuk in tekst)
            {
                graphics.DrawString(stuk, new Font(FontFamily.GenericSansSerif, 16), Brushes.Black, new Point(10, y));
                y += 16 + 20;
            }
        }
    }
}
