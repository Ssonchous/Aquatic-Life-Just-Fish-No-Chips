using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators
{
    public class BaseFishDecorator : BaseFish
    {
        public BaseFish wrappedFish;

        public BaseFishDecorator(BaseFish fish) : base(fish.Position)
        {
            wrappedFish = fish;
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
            Position = source.Position;
            target.СurrentBehavior = source.СurrentBehavior;
        }

        public override bool IsAlive
        {
            get => base.IsAlive;
            set { base.IsAlive = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.IsAlive = value;
            }
        }
        public override Point Position
        {
            get => base.Position;
            set { base.Position = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.Position = value;
            }
        }

        public override float Health
        {
            get => base.Health;
            set { base.Health = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.Health = value;
            }
        }

        public override float Hunger
        {
            get => base.Hunger;
            set { base.Hunger = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.Hunger = value;
            }
        }
        public override float VisionRange
        {
            get => base.VisionRange;
            set { base.VisionRange = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.VisionRange = value;
            }
        }

        public override double CurrentAngle
        {
            get => base.CurrentAngle;
            set { base.CurrentAngle = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.CurrentAngle = value;
            }
        }
        public override float Speed
        {
            get => base.Speed;
            set { base.Speed = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.Speed = value;
            }
        }

        public override IBehavior СurrentBehavior
        {
            get => base.СurrentBehavior;
            set { base.СurrentBehavior = value; if (wrappedFish != null) // ← Вот это!
                    wrappedFish.СurrentBehavior = value;
            }
        }


        public override T GetDecorator<T>() 
        {
            if (this is T decorator)
            {
                //MessageBox.Show(decorator.Name + " уже " + decorator.GetType().ToString());
                return decorator;
            }

            if (wrappedFish is BaseFishDecorator innerDecorator)
            {
                //MessageBox.Show(innerDecorator.Name+" " +innerDecorator.GetType().ToString());
                return innerDecorator.GetDecorator<T>(); 
            }

            return null;
        }



    }
}
