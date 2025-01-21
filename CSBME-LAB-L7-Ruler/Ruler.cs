using System;
using System.Drawing;
using System.Windows.Forms;

namespace L7_Ruler
{
    public partial class Ruler : Form
    {
        // Constants
        private const int RULER_START_X = 30;
        private const int RULER_START_Y = 50;
        private const int RULER_HEIGHT = 80;
        private const int TICK_BASE_Y = 120;
        private const int LABEL_Y = 75;
        private readonly Font labelFont = new Font("Arial", 8);
        private readonly Pen blackPen = new Pen(Color.Black, 1);
        private readonly Pen bluePen = new Pen(Color.Blue, 1);
        private readonly Brush redBrush = new SolidBrush(Color.Red);
        private readonly Brush whiteBrush = new SolidBrush(Color.White);

        public Ruler()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawRuler((float)vmax.Value, (float)ppm.Value);
        }

        private void DrawRuler(float vmax, float ppm)
        {
                Graphics g = CreateGraphics();
                g.Clear(BackColor);
                DrawRulerBox(g, vmax, ppm);
                DrawStartCircle(g);
                DrawTicksAndLabels(g, vmax, ppm);

        }

        private void DrawRulerBox(Graphics g, float vmax, float ppm)
        {
            int rulerWidth = 70 + (int)(vmax * 10 * (ppm + 1));
            Rectangle rulerRect = new Rectangle(RULER_START_X, RULER_START_Y, rulerWidth, RULER_HEIGHT);

            g.DrawRectangle(bluePen, rulerRect);
            g.FillRectangle(whiteBrush, rulerRect.X + 1, rulerRect.Y + 1,
                           rulerRect.Width - 1, rulerRect.Height - 1);
        }

        private void DrawStartCircle(Graphics g)
        {
            Rectangle circleRect = new Rectangle(35, 87, 10, 10);
            g.DrawEllipse(bluePen, circleRect);
            g.FillEllipse(whiteBrush, 36, 88, 8, 8);
        }

        private void DrawTicksAndLabels(Graphics g, float vmax, float ppm)
        {
            int xPosition = 65;
            int pixelsPerTick = (int)ppm + 1;

            for (int majorTick = 0; majorTick <= vmax; majorTick++)
            {
            
                g.DrawString(majorTick.ToString(), labelFont, redBrush,
                           xPosition - 4, LABEL_Y);

                if (majorTick < vmax)
                {
                    for (int minorTick = 0; minorTick <= 9; minorTick++)
                    {
                        int tickStartY = GetTickStartY(minorTick);
                        g.DrawLine(blackPen, xPosition, tickStartY,
                                 xPosition, TICK_BASE_Y);
                        xPosition += pixelsPerTick;
                    }
                }
                else
                {
                    g.DrawLine(blackPen, xPosition, 90, xPosition, TICK_BASE_Y);
                }
            }
        }

        private int GetTickStartY(int tickPosition)
        {
            switch (tickPosition)
            {
                case 0:
                    return 90;
                case 5:
                    return 100;
                default:
                    return 110;
            }
        }

        private void UpdateRuler(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            labelFont.Dispose();
            blackPen.Dispose();
            bluePen.Dispose();
            redBrush.Dispose();
            whiteBrush.Dispose();
        }

        private void vmax_ValueChanged(object sender, EventArgs e) => UpdateRuler(sender, e);
        private void ppm_ValueChanged(object sender, EventArgs e) => UpdateRuler(sender, e);
    }
}