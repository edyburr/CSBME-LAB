using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSBME_LAB_L5_Ornaments
{
    public partial class Ornament : Form
    {
        private readonly List<Ornaments> ornament = new List<Ornaments>();
        private readonly Random random = new Random();
        private const int BaseShapeSize = 30;
        private const int BaseSpacing = 50;

        public Ornament()
        {
            InitializeComponent();
            this.Paint += Ornament_Paint;
            this.Resize += Ornament_Resize;
            InitializeOrnaments();
        }

        public void InitializeOrnaments()
        {
            ornament.Add(new Diamond());
            ornament.Add(new Star());
            ornament.Add(new Hexagon());
            ornament.Add(new Triangle());
        }

        //Ornament Generator
        private void Ornament_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            float scaleFactor = this.ClientSize.Height / 150f;
            int currentShapeSize = (int)(BaseShapeSize * scaleFactor);
            int currentSpacing = (int)(BaseSpacing * scaleFactor);
            int y = this.Size.Height / 2;
            int availableWidth = this.ClientSize.Width - currentShapeSize;

            for (int x = currentShapeSize; x < availableWidth; x += currentSpacing)
            {
                var shape = ornament[random.Next(ornament.Count)];

                //Color randomizer
                var color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                shape.Draw(e.Graphics, x, y, currentShapeSize, color);

                //Draw horizontal connector line
                using (Pen connector = new Pen(Color.Blue, 1))
                {
                    e.Graphics.DrawLine(connector, currentShapeSize, y - currentSpacing, x, y - currentSpacing);
                }

                //Draw ornament connector line
                using (Pen linePen = new Pen(Color.Blue, 1))
                {
                    e.Graphics.DrawLine(linePen, x, y - currentSpacing, x, y - currentSpacing / 2);
                }
            }
        }

        private void Ornament_Resize(object sender, EventArgs e)
        {
            //This redraws the shapes when the form is resized
            this.Invalidate();
        }
    }

    //Abstract class to make the code a bit more modular
    public abstract class Ornaments
    {
        //Main Draw method
        public abstract void Draw(Graphics ornament, int x, int y, int size, Color color);

        protected void FillShape(Graphics deco, Point[] points, Color color)
        {
            using (SolidBrush solid = new SolidBrush(Color.FromArgb(50, color)))
            using (Pen pen = new Pen(color, 2))
            {
                deco.FillPolygon(solid, points);
                deco.DrawPolygon(pen, points);
            }
        }
    }


    public class Diamond : Ornaments
    {
        public override void Draw(Graphics deco, int x, int y, int size, Color color)
        {
            Point[] outerPoints = {
                new Point(x, y - size/2),
                new Point(x + size/2, y),
                new Point(x, y + size/2),
                new Point(x - size/2, y)
            };

            Point[] innerPoints = {
                new Point(x, y - size/4),
                new Point(x + size/4, y),
                new Point(x, y + size/4),
                new Point(x - size/4, y)
            };

            FillShape(deco, outerPoints, color);
            FillShape(deco, innerPoints, Color.FromArgb(100, color));
        }
    }

    public class Star : Ornaments
    {
        public override void Draw(Graphics deco, int x, int y, int size, Color color)
        {
            using (Pen pen = new Pen(color, 2))
            {
                Point[] points = new Point[16];
                double angleStep = Math.PI / 8;

                for (int i = 0; i < 16; i++)
                {
                    int radius = (i % 2 == 0) ? size / 2 : size / 4;
                    points[i] = new Point(
                        x + (int)(radius * Math.Cos(i * angleStep)),
                        y + (int)(radius * Math.Sin(i * angleStep))
                    );
                }

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, color)))
                {
                    deco.FillPolygon(brush, points);
                    deco.DrawPolygon(pen, points);
                }
            }
        }
    }

    public class Hexagon : Ornaments
    {
        public override void Draw(Graphics deco, int x, int y, int size, Color color)
        {
            Point[] points = new Point[6];
            Point[] innerPoints = new Point[6];
            double angleStep = Math.PI / 3;
            int radius = size / 2;
            int innerRadius = radius / 2;

            for (int i = 0; i < 6; i++)
            {
                points[i] = new Point(
                    x + (int)(radius * Math.Cos(i * angleStep)),
                    y + (int)(radius * Math.Sin(i * angleStep))
                );
                innerPoints[i] = new Point(
                    x + (int)(innerRadius * Math.Cos(i * angleStep)),
                    y + (int)(innerRadius * Math.Sin(i * angleStep))
                );
            }

            FillShape(deco, points, color);
            FillShape(deco, innerPoints, Color.FromArgb(200, color));
        }
    }

    public class Triangle : Ornaments
    {
        public override void Draw(Graphics deco, int x, int y, int size, Color color)
        {
            Point[] points = {
                new Point(x, y - size/2),
                new Point(x + size/2, y + size/2),
                new Point(x - size/2, y + size/2)
            };

            Point[] innerPoints = {
                new Point(x, y - size/6),
                new Point(x + size/4, y + size/3),
                new Point(x - size/4, y + size/3)
            };

            FillShape(deco, points, color);
            FillShape(deco, innerPoints, Color.FromArgb(200, color));
        }
    }
}