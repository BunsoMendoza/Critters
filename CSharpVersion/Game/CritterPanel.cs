using System.Drawing;
using System.Windows.Forms;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.Game
{
    // Class CritterPanel displays a grid of critters
    public class CritterPanel : Panel
    {
        private CritterModel myModel;
        private Font myFont;
        private static bool created;

        public const int FontSize = 12;

        public CritterPanel(CritterModel model)
        {
            // This prevents someone from trying to create their own copy of
            // the GUI components
            if (created)
                throw new System.InvalidOperationException("Only one world allowed");
            created = true;

            myModel = model;
            // Construct font and compute char width once in constructor for efficiency
            myFont = new Font("Courier New", FontSize + 4, FontStyle.Bold);
            BackColor = Color.Cyan;
            Size = new Size(FontSize * model.Width + 20, FontSize * model.Height + 20);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            foreach (Critter next in myModel.GetCritters())
            {
                Point p = myModel.GetPoint(next);
                string appearance = myModel.GetAppearance(next);
                // Draw shadow
                using (Brush shadowBrush = new SolidBrush(Color.Black))
                {
                    g.DrawString(appearance, myFont, shadowBrush, p.X * FontSize + 11, p.Y * FontSize + 21);
                }
                // Draw critter
                using (Brush critterBrush = new SolidBrush(myModel.GetColor(next)))
                {
                    g.DrawString(appearance, myFont, critterBrush, p.X * FontSize + 10, p.Y * FontSize + 20);
                }
            }
        }
    }
}
