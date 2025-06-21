using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators
{
    public class BaseFishDecorator : BaseFish
    {
        public BaseFish WrappedFish;

        public BaseFishDecorator(BaseFish fish) : base(fish.Position)
        {
            WrappedFish = fish;
            // Копируем только начальные значения
            CopyFishState(fish, this);
        }

        // Метод для копирования состояния между рыбами
        private void CopyFishState(BaseFish source, BaseFish target)
        {
            target.Name = source.Name;
            target.Image = source.Image;
            target.Size = source.Size;
            target.VisionRange = source.VisionRange;
            target.Speed = source.Speed;
            target.IsAlive = source.IsAlive;
            target.Health = source.Health;
            target.Hunger = source.Hunger;
            target.CurrentAngle = source.CurrentAngle;
            target.Position = source.Position;
            target.СurrentBehavior = source.СurrentBehavior;
        }

        public override T GetDecorator<T>() 
        {
            if (this is T decorator)
            {
                //MessageBox.Show(decorator.Name + " уже " + decorator.GetType().ToString());
                return decorator;
            }

            if (WrappedFish is BaseFishDecorator innerDecorator)
            {
                //MessageBox.Show(innerDecorator.Name+" " +innerDecorator.GetType().ToString());
                return innerDecorator.GetDecorator<T>(); 
            }

            return null;
        }



    }
}
