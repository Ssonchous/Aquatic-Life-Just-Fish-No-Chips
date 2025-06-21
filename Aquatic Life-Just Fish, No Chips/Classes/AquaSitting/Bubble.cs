using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting
{
    public class Bubble : IAquaSitting
    {
        private static Random random = new Random();
        public bool IsAlive { get; set; } = true;
        public Point Position { get; set; }
        public Size Size { get; set; }
        public ImageSource Image { get; set; }

        public Bubble(Point position)
        {
            Position = position;
            Size = new Size(random.Next(3, 20), random.Next(3, 20));
        }

        public void UpdatePosition()
        {
            Position = new Point(Position.X, Position.Y - Size.Width * 0.05f);
            if (Position.Y < 0) IsAlive = false;
        }
    }
}
