using System;
using System.Drawing;
using System.Windows.Forms;

public class Form1 : Form
{
    private const int GridSize = 24; // 24x24 grid
    private int cellSize;
    private Random random;
    private bool[,] grid;

    public Form1()
    {
        // Initialize the form
        this.Text = "24x24 Grid";
        this.FormBorderStyle = FormBorderStyle.Sizable;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.random = new Random();
        this.grid = new bool[GridSize, GridSize];

        // Set up the resizing behavior
        this.Resize += (sender, e) => Invalidate();

        // Set a fixed minimum size to avoid window too small
        this.MinimumSize = new Size(300, 300);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Calculate the cell size based on the smaller window dimension
        int size = Math.Min(this.ClientSize.Width, this.ClientSize.Height);
        cellSize = size / GridSize;

        // Create the grid layout
        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                // Determine if the cell should be filled with red
                bool isFilled = random.Next(0, 2) == 1; // 50% chance

                // Define the rectangle for the current cell
                Rectangle cellRect = new Rectangle(col * cellSize, row * cellSize, cellSize, cellSize);

                // Draw the cell
                if (isFilled)
                {
                    e.Graphics.FillRectangle(Brushes.Red, cellRect);
                }
                e.Graphics.DrawRectangle(Pens.Black, cellRect); // Draw grid line
            }
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}
