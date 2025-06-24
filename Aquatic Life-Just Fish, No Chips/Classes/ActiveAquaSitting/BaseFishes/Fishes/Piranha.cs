using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Properties; // Для доступа к Resources
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Fishes
{
    public class Piranha : BaseFish
    {
        private Piranha(Point position)
            : base(position)
        {
            Name = "piranha";
            Size = new Size(180, 100);
            Health = 80;
            Hunger = 70;
            VisionRange = 200;
            Speed = 3;

            Image = LoadImage("piranha.png");
            if (Image == null) IsAlive = false;
        }

        public static HunterDecorator Create(Point position, 
                                    int biteStrength = 40,
                                    int maxSchoolSize = 3)
        {
            return new HunterDecorator(
                new SchoolingDecorator(
                    new Piranha(position),
                 maxSchoolSize ),
                 biteStrength);
        }
    }
}