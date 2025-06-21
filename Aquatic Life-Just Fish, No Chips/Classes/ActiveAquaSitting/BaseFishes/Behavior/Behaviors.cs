using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior
{
    public class SeekingFoodBehavior : TargetedBehavior
    {
        public SeekingFoodBehavior(IAquaSitting target, Point maxPosition) : base(target, maxPosition) {}

        public override void Interact(BaseFish fish)
        {
            if (Target is Food food)
            {
                fish.Hunger += food.NutritionValue;
                food.IsAlive = false;
                fish.СurrentBehavior = new IdleBehavior(MaxPosition);
            }
        }
    }
    public class HuntingBehavior : TargetedBehavior
    {
        public HuntingBehavior(IAquaSitting target, Point maxPosition) : base(target, maxPosition) { }

        public override void Interact(BaseFish fish)
        {
            var hunter = (fish as BaseFishDecorator)?.GetDecorator<HunterDecorator>();

            if (hunter != null && Target is BaseFish prey )
            {
                hunter.Bite(prey); 
                fish.СurrentBehavior = new IdleBehavior( MaxPosition);
            }
        }
    }

    public class FearsBehavior : TargetedBehavior
    {
        public FearsBehavior(IAquaSitting target, Point maxPosition) : base(target, maxPosition) { }

        public override void UpdateAngle(BaseFish fish)
        {
            if (Target != null && Target.IsAlive)
            {
                Vector direction = fish.Position - Target.Position;
                fish.CurrentAngle = Math.Atan2(direction.Y, direction.X);
            }
            else
            {
                fish.СurrentBehavior = new IdleBehavior(MaxPosition);
            }
        }

        public override void Interact(BaseFish fish)
        { }

    }


    public class SchoolingBehavior : IndependedBehavior
    {
        public SchoolingBehavior(Point maxPosition) : base(maxPosition){ }

        public override void UpdateAngle(BaseFish fish)
        {
            var schooling = (fish as BaseFishDecorator)?.GetDecorator<SchoolingDecorator>();
            if (schooling == null) return;
            if (schooling.Leader != null)
                schooling.UpdateSchoolingAngle();
            if (schooling.IsLeader)
            {
                base.UpdateAngle(fish);
            }
        }
    }

    public class PlayingBehavior : TargetedBehavior
    {
        private int playTimeRemaining;
        public PlayingBehavior(IAquaSitting target, Point maxPosition) : base(target, maxPosition) 
        {
            playTimeRemaining = 50;
        }

        public override void Interact(BaseFish fish)
        {
            playTimeRemaining -= 1;

            if (playTimeRemaining <= 0)
            {
                fish.СurrentBehavior = new IdleBehavior(MaxPosition);
            }
        }
    }

    public class IdleBehavior : IndependedBehavior
    {
        public IdleBehavior(Point maxPosition) : base(maxPosition) { }

        public override void UpdateAngle(BaseFish fish)
        {
            base.UpdateAngle(fish);
        }

    }



}
