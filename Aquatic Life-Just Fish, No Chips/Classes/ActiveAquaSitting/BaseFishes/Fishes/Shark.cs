using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Fishes
{
    public class Shark : BaseFish
    {
        private Shark(Point position) : base(position)
        {
            Name = "shark";
            Size = new Size(400, 200);
            Health = 150;
            Hunger = 60;
            VisionRange = 300;
            Speed = 4.0f;
            Image = LoadImage("shark.png");
        }

        public static BaseFish Create(Point position, int biteStrength = 60)
        {
            return new HunterDecorator(
                new Shark(position),
                biteStrength);
        }
    }
}
