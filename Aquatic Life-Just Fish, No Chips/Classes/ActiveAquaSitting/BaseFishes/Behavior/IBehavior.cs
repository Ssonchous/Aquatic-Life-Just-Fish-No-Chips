using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior
{
    public interface IBehavior
    {
        Point MaxPosition { get; set; }
        void UpdateAngle(BaseFish fish);
        void Move(BaseFish fish);
        
    }


    public abstract class TargetedBehavior : IBehavior
    {
        public Point MaxPosition { get; set; }
        protected IAquaSitting Target { get; set; }
        protected int InteractionDistance;

        public TargetedBehavior(IAquaSitting target, Point maxPosition, int interactionDistance)
        {
            Target = target;
            InteractionDistance = interactionDistance;
            MaxPosition = maxPosition;
        }

        
        public abstract void Interact(BaseFish fish);

        public virtual void UpdateAngle(BaseFish fish)
        {

            if (Target != null)
            {
                Vector direction = Target.Position - fish.Position;
                fish.CurrentAngle = Math.Atan2(direction.Y, direction.X);
            }
            else
            {
                fish.СurrentBehavior = new IdleBehavior(MaxPosition);
            }
            fish.IsHeadingToWall(MaxPosition);
        }

        public void Move(BaseFish fish)
        {
            if (Target == null || !Target.IsAlive)
            {
                fish.СurrentBehavior = new IdleBehavior(MaxPosition);
                return;
            }

            double distance = Math.Sqrt(
                Math.Pow(fish.Position.X - Target.Position.X, 2) +
                Math.Pow(fish.Position.Y - Target.Position.Y, 2));

            if (distance <= InteractionDistance)
            {
                Interact(fish);
            }
            else
            {
                fish.Position = new Point(
                    fish.Position.X + Math.Cos(fish.CurrentAngle) * fish.Speed,
                    fish.Position.Y + Math.Sin(fish.CurrentAngle) * fish.Speed
                );
            }
        }
    }

    public abstract class IndependedBehavior : IBehavior
    {
        private static Random random;
        public Point MaxPosition { get; set; }

        static IndependedBehavior()
        {
            random = new Random();
        }
        public IndependedBehavior(Point maxPosition)
        { 
            MaxPosition = maxPosition;
        }
        public virtual void UpdateAngle(BaseFish fish)
        {
            double randomAngleChange = (random.NextDouble() - 0.5) * 0.01;
            fish.CurrentAngle += randomAngleChange;
            fish.IsHeadingToWall(MaxPosition);
        }

        public void Move(BaseFish fish)
        {
            fish.Position = new Point(
                    fish.Position.X + Math.Cos(fish.CurrentAngle) * fish.Speed,
                    fish.Position.Y + Math.Sin(fish.CurrentAngle) * fish.Speed
                );
        }


    }
}
