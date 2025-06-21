using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting
{
    public class Food : IAquaSitting
    {
        private static Random random = new Random();
        public bool IsAlive { get; set; } = true;
        public Point Position { get; set; }
        public Size Size { get; set; }
        public ImageSource Image { get; set; }
        public int NutritionValue { get; }
        public Point MaxPosition { get; set; }

        public Food(Point position, Point MaxPosition)
        {
            Position = position;
            Size = new Size( random.Next(5, 15), random.Next(5, 15));
            NutritionValue = random.Next(30, 90);
        }

        public void UpdatePosition()
        {
            Position = new Point(Position.X, Position.Y + Size.Width * 0.1f);
            if (Position.Y > MaxPosition.Y) IsAlive = false;
        }
    }
}
