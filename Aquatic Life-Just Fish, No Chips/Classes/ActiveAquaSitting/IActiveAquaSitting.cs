using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting
{
    public interface IActiveAquaSitting : IAquaSitting
    {
        int Health { get; set; }
        int Hunger { get; set; }
        float VisionRange { get; set; }
        double CurrentAngle { get; set; }
        void UpdateBehavior(AquariumContent content);
    }
}
