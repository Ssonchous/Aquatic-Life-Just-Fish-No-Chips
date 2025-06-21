using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators
{
    public class HunterDecorator: BaseFishDecorator
    {
        public int BiteStrength { get; set; } = 20;

        public HunterDecorator(BaseFish fish, int biteStrength) : base(fish) 
        {
            BiteStrength = biteStrength;
        }

        public void Bite(BaseFish prey)
        {
            prey.Health -= BiteStrength;
            this.Hunger += 30;
        }

    }
}
