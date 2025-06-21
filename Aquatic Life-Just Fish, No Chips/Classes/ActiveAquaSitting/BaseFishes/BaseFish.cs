using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes
{
    public class BaseFish : IActiveAquaSitting
    {
        private Random random;
        public string Name {get; set;}
        public virtual bool IsAlive { get; set; } 
        public virtual Point Position { get; set; }
        public Size Size { get; set; }
        public ImageSource Image { get; set; }
        private int health { get; set; } 
        private int hunger { get; set; }
        public virtual float VisionRange { get; set; }
        public virtual double CurrentAngle { get; set; }
        public virtual float Speed { get; set; } 
        public virtual IBehavior СurrentBehavior { get; set; }

        public virtual int Hunger
        {
            get => hunger;
            set
            {
                hunger = Bounds(value, 0, 100);
                if (hunger == 0) Health--;
                else if (hunger > 50) Health++;
            }
        }

        public virtual int Health
        {
            get { return health; }
            set
            {
                health = Bounds(value, 0, 100);
                if (health <= 0) IsAlive = false;
            }
        }


        public BaseFish(Point position)
        {
            Position = position;
            Size = new Size(100,50);

            IsAlive = true;
            Health = 100;
            Hunger = 50;
            VisionRange = 200;
            Speed = 2;

            СurrentBehavior = null;
            random = new Random();
        }

        public BaseFish(Point position, Size size, int visionRange, float speed)
        {
            Position = position;
            Size = size;

            IsAlive = true;
            Health = 100;
            Hunger = 50;

            VisionRange = visionRange;
            Speed = speed;

            СurrentBehavior = null;
            random = new Random();
        }

        protected ImageSource LoadImage(string imageFileName)
        {
            // Путь к папке с изображениями
            string imagesFolder = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Img", "Fish");

            // Полный путь к файлу
            string imagePath = System.IO.Path.Combine(imagesFolder, imageFileName);


            try
            {
                if (!System.IO.File.Exists(imagePath))
                {
                    MessageBox.Show($"Файл изображения не найден: {imagePath}");
                    return null;
                }

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                return bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}\nПуть: {imagePath}");
                return null;
            }
        }

        public virtual void UpdateBehavior(AquariumContent content)
        {
            СurrentBehavior = BehaviorSelector.Choose(this, content);
            СurrentBehavior.UpdateAngle(this);
        }

        public void IsHeadingToWall(Point maxPosition)
        {
            double margin = 30; // Расстояние до стены
            double nextX = Position.X + Math.Cos(CurrentAngle) * Speed;
            double nextY = Position.Y + Math.Sin(CurrentAngle) * Speed;

            // Проверяем каждую стенку
            bool isNearLeft = nextX - Size.Width / 2 < margin;
            bool isNearRight = nextX + Size.Width / 2 > maxPosition.X - margin;
            bool isNearTop = nextY - Size.Height / 2 < margin;
            bool isNearBottom = nextY + Size.Height / 2 > maxPosition.Y - margin;

            if (isNearLeft || isNearRight || isNearTop || isNearBottom)
            {
                MessageBox.Show("Ну типа блять угол");
                CurrentAngle += Math.PI + (random.NextDouble() - 0.5) * 0.5; // 25% случайности
            }

        }

        public virtual T GetDecorator<T>() where T : BaseFishDecorator
        {
            return null; // Для обычных рыб возвращаем null
        }

        public virtual void UpdatePosition()
        {
            if (IsAlive)
                СurrentBehavior.Move(this);
        }

        public static int Bounds(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

    }
}
