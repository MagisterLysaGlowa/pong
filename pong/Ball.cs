using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace pong
{
    public class Ball : ICanvasObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Canvas Canvas { get; set; }
        public double Angle { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        public double Speed { get; set; }
        public Ellipse Shape { get; set; }

        public Ball(int width, int height, Canvas canvas)
        {
            Width = width;
            Height = height;
            Canvas = canvas;
            Shape = new()
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(Colors.White)
            };
            Reset();
            Canvas.Children.Add(Shape);
        }

        public void Draw()
        {
            Canvas.SetLeft(Shape, X);
            Canvas.SetTop(Shape, Y);
        }
        public void Move()
        {
            X += Math.Cos(Angle) * Speed * DirectionX;
            Y += Math.Cos(Angle) * Speed * DirectionY;

            if (Y - Height <= 0 || Y + Height >= Canvas.Height)
            {
                DirectionY *= -1;
                Speed += 0.5f;
            }

            Draw();
        }
        public void Reset()
        {
            X = Canvas.Width / 2 - Width / 2;
            Y = Canvas.Height / 2 - Height / 2;
            Random random = new();
            Speed = 3f;
            DirectionX = random.Next(1, 30) % 2 == 0 ? 1 : -1;
            DirectionY = random.Next(1, 30) % 2 == 0 ? 1 : -1;
            Random r = new();
            Angle = r.Next(30, 60) * Math.PI / 180;
        }
    }
}
