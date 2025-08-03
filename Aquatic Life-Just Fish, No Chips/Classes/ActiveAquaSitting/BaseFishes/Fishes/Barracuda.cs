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
            Size = new Size(350, 200);
            Health = 60;
            Hunger = 50;
            VisionRange = 400;
            Speed = 3;
            Image = LoadImage("barracuda.png");
        }

        public static HunterDecorator Create(Point position, int biteStrength = 45, int maxSchoolSize = 2)
        {
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            var barracuda = new Barracuda(position);
            var schoolingDecorator = new SchoolingDecorator(barracuda, maxSchoolSize);
            var hunterDecorator = new HunterDecorator(schoolingDecorator, biteStrength);

            return hunterDecorator;
        }
    }
}
