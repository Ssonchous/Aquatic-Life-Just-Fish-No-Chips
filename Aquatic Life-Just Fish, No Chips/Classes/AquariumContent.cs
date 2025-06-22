using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes
{
    public class AquariumContent
    {
        private List<Food> Foods { get; }
        private List<Bubble> Bubbles { get; }
        private List<BaseFish> Fishes { get; }

        public Point MaxPosition { get; }


        public AquariumContent(List<IAquaSitting> allObjects, Point maxPosition)
        {
            Foods = allObjects.OfType<Food>().ToList();
            Bubbles = allObjects.OfType<Bubble>().ToList();
            Fishes = allObjects.OfType<BaseFish>().ToList();
            MaxPosition = maxPosition;
        }

        private double GetDistanceSquare(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }

        public bool IsInRange(IActiveAquaSitting fish, IAquaSitting obj)
        {
            return GetDistanceSquare(fish.Position, obj.Position) <= (fish.VisionRange * fish.VisionRange);
        }

        private List<T> FindNearestListInRange<T>(BaseFish fish, List<T> certainOtbjects) where T : IAquaSitting
        {
            return certainOtbjects
                .Where(obj => obj.IsAlive && IsInRange(fish, obj))
                .OrderBy(obj => GetDistanceSquare(fish.Position, obj.Position))
                .ToList();
        }

        private T FindNearestInRange<T>(BaseFish fish, List<T> certainOtbjects) where T : IAquaSitting
        {
            return FindNearestListInRange(fish, certainOtbjects).FirstOrDefault();
        }

        public Food FindNearestFood(BaseFish fish)
        {
            return FindNearestInRange(fish, Foods);
        }

        public Bubble FindNearestBubble(BaseFish fish)
        {
            return FindNearestInRange(fish, Bubbles);
        }

        public BaseFish FindNearestFish(BaseFish fish)
        {
            List<BaseFish> fishes = Fishes.Where(f => f != fish).ToList();
            return FindNearestInRange(fish, fishes);
        }

        public BaseFish FindNearestPrey(BaseFish predator)
        {
            List<BaseFish> fishesPrey = Fishes.Where(f => f != predator && f.Size.Width < predator.Size.Width / 1).ToList();
            var fish=  FindNearestInRange(predator, fishesPrey);
            return fish;
        }

        public BaseFish FindNearestHunter(BaseFish predator)
        {
            List<BaseFish> hunters = Fishes
                .Where(f => f != predator && f.СurrentBehavior is HuntingBehavior && f.Size.Width > predator.Size.Width)
                .ToList();
            return FindNearestInRange(predator, hunters);
        }


        public List<BaseFish> FindNearestListFishBro(BaseFish fish)
        {
            var fishesBro = Fishes.Where(f => f.Name == fish.Name).ToList();
            return FindNearestListInRange(fish, fishesBro);
        }
    }
}
