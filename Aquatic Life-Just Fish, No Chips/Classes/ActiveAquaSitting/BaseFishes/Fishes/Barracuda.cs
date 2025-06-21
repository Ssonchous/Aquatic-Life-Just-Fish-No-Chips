using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
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
    public class Barracuda : BaseFish
    {
        private Barracuda(Point position) : base(position)
        {
            Name = "barracuda";
            Size = new Size(400, 200);
            Health = 10;
            Hunger = 0;
            VisionRange = 500;
            Speed = 3;
            Image = LoadImage("barracuda.png");
        }

        public static SchoolingDecorator Create(Point position, int biteStrength = 45, int maxSchoolSize = 6)
        {
            return new SchoolingDecorator(
                new HunterDecorator(
                    new Barracuda(position),
                    biteStrength),
                maxSchoolSize);
        }
    }
}
