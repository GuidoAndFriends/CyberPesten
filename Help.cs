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
    class Help : Form
    {
        Bitmap terugBitmap, achtergrond;
        float verhouding;
        bool terugHover;
        Rectangle terugButton, maat, bounds;
        public Spel spel;
        Form vorige;
        string tekst;

        public Help(Form form, Spel _spel = null)
        {
            Text = "CyberPesten: Help";
            maat = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            this.Show();
            vorige = form;

            if (_spel != null)
            {
                spel = _spel;
            }
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

            tekst = "DOEL VAN HET SPEL";
            tekst += "\nHet doel van het spel is om zo snel mogelijk al de kaarten in je hand weg te spelen. De persoon die als eerst al zijn kaarten weg te spelen, heeft wint het spel.";
            tekst += "\n";
            tekst += "\nHOE SPEEL JE HET SPEL?";
            tekst += "\nKlik op een kaart in je om deze kaart te spelen. Indien de kaart gespeeld kan worden, dan wordt de kaart opgelegd.";
            tekst += "\nAls je geen kaart kunt spelen dan moet je een kaart pakken van de pot. Als de kaart die je hebt gepakt gespeeld kan worden, mag je die als nog spelen. Je mag deze kaart ook bewaren.";
            tekst += "\nJe hoeft niet te spelen als ja dat wel kan, daarvoor in de plaats moet je dan een kaart pakken. Dit doe je om op “einde beurt” te klikken.";
            tekst += "\nNadat jouw beurt afgelopen is dan de speler naast je aan de beurt. In normale gevallen speelt elke speler een kaart per beurt, alleen door een pestkaart (zie functie van kaarten) te spelen kan een speler meerdere kaarten opleggen.";
            tekst += "\nAls je nog maar één kaart over hebt dan moet je dit melden door op de 'laatste kaart' knop te drukken, als dit niet gedaan wordt dan moet je voor straf vijf kaarten pakken.";
            tekst += "\nWanneer je laatste kaart een pestkaart is, moet je ook voor straf vijf kaarten pakken.";
            tekst += "\nEen kaart is speelbaar als deze qua waarde of kleur gelijk is aan de vorig gespeelde kaart op de stapel.";
            tekst += "\n";
            tekst += "\nIn het spel zitten kaarten die geen functie hebben (normale kaarten) en pestkaarten. Pestkaarten zijn:";
            tekst += "\n- Aas: De speelrichting wordt omgedraaid";
            tekst += "\n- Twee: De volgende speler moet twee kaarten trekken, of ook een 2 of een joker opleggen. De kaarten die moeten worden getrokken worden bij elkaar opgeteld.";
            tekst += "\n- Zeven: De huidige speler mag nog een kaart spelen";
            tekst += "\n- Acht: De eerstvolgende speler moet een beurt overslaan";
            tekst += "\n- Boer: De speler mag kleur van speelstapel veranderen. De eerstvolgende speler moet een kaart van de gekozen kleur opleggen. Deze kaart mag altijd gespeeld worden.";
            tekst += "\n- Heer: De huidige speler mag nog een kaart spelen";
            tekst += "\n- Joker: De volgende speler moet vijf kaarten pakken, of ook een 2 of een joker opleggen. De kaarten die moeten worden getrokken worden bij elkaar opgeteld. De speler die moest pakken mag hierna een willekeurige kaart naar keuze opleggen om de kleur van het verdere spel te bepalen";



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
                klikSound();
                vorige.Show();
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

        private void klikSound()
        {
            Stream s = CyberPesten.Properties.Resources.button;
            SoundPlayer sound = new SoundPlayer(s);
            sound.Play();
        }

    }
}
