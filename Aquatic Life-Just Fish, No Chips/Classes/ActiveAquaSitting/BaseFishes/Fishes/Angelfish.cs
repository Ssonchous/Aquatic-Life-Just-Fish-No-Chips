using Aquatic_Life_Just_Fish__No_Chips.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Fishes
{
    public class Angelfish : BaseFish
    {
        private Angelfish(Point position) : base(position)
        {
            Name = "angelfish";
            Size = new Size(200, 200); 
            Health = 85;
            Hunger = 30;
            VisionRange = 500;
            Speed = 1.5f;
            Image = LoadImage("angelfish.png");
        }

        public static BaseFish Create(Point position)
        {
            return new Angelfish(position); 
        }
    }
}
