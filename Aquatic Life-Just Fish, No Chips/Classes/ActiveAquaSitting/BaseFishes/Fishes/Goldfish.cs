using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Fishes
{
    public class Goldfish : BaseFish
    {
        private Goldfish(Point position) : base(position)
        {
            Name = "goldfish";
            Size = new Size(120, 90);
            Health = 90;
            Hunger = 40;
            VisionRange = 180;
            Speed = 1.8f;
            Image = LoadImage("goldfish.png");
        }

        public static BaseFish Create(Point position, int maxSchoolSize = 10)
        {
            return new SchoolingDecorator(
                new Goldfish(position),
                maxSchoolSize);
        }
    }
}
