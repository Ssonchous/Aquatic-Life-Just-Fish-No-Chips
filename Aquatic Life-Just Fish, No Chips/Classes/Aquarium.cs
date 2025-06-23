using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes
{
    public class Aquarium 
    {
        public List<IAquaSitting> Contents { get; set; }
        public Point MaxPosition { get; set; }

        public Aquarium(Point maxPosition) 
        { 
            Contents = new List<IAquaSitting>();
            MaxPosition = maxPosition;

        }
        public void AddFood()
        {
            Contents.Add(new Food(MaxPosition));
        }

        public void AddBubble()
        {
            Contents.Add(new Bubble(MaxPosition));
        }

        public void ProcessGenericFish(BaseFish fish) 
        {
            Contents.Add(fish);
        }

        public void Update()
        {
            UpdateBehavior();
            UpdatePosition();
            Contents.RemoveAll(x => !x.IsAlive);

        }

        public void UpdateBehavior()
        {
            AquariumContent content = new AquariumContent(Contents, MaxPosition);

            //BehaviorSelector.Choose((BaseFish)Contents.First(), content);
            foreach (var activeAqua in Contents.OfType<IActiveAquaSitting>())
            {
                if (activeAqua is BaseFish baseFish)
                {
                    baseFish.UpdateBehavior(content);
                }
            }
        }

        public void UpdatePosition()
        {
            foreach (var aquaSitting in Contents.OfType<IAquaSitting>())
                aquaSitting.UpdatePosition();
        }

    }
}
