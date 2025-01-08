using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waveform
{
    public partial class Waveform : Form
    {
        private int val = 0;
        private const int valMax = 100;
        private const int increment = 10;
        private int x = 10;
        private bool check = true;
        public Waveform()
        {
            InitializeComponent();
        }

        private void btnUpDown_Click(object sender, EventArgs e)
        {
            if (check)
            {
                val += increment;
                if (val >= valMax)
                {
                    val = valMax;
                    check = false;
                }
            }
            else
            {
                val -= increment;
                if (val <= 0)
                {
                    val = 0;
                    check = true;
                }
            }

            labelUpDown.Text = val.ToString();
            DrawBar();
        }

        private void DrawBar()
        {
            Graphics draw = this.CreateGraphics();
            Brush barBrush = new SolidBrush(Color.Blue);
            int barWidth = 5;
            int barHeight = val + 10;
            int y = this.Height - barHeight - 50;

            draw.FillRectangle(barBrush, x, y, barWidth, barHeight);

            x += barWidth + 5;

            draw.Dispose();
        }

        private void Waveform_Load(object sender, EventArgs e)
        {
            this.Text = "L2 - Daicu Eduard - IMD1";
            this.Width = 900;
            this.Height = 300;
        }
    }
}
