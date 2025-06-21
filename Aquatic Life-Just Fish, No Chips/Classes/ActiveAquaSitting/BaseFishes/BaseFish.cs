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
        private double health { get; set; } 
        private double hunger { get; set; }
        public virtual float VisionRange { get; set; }
        public virtual double CurrentAngle { get; set; }
        public virtual float Speed { get; set; } 
        public virtual IBehavior СurrentBehavior { get; set; }

        public virtual double Hunger
        {
            get => hunger;
            set
            {
                hunger = Bounds(value, 0, 100);
                if (hunger == 0) Health -= 0.1 ;
                else if (hunger > 50) Health++;
            }
        }

        public virtual double Health
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
            //if (Name == "barracuda")
            //    MessageBox.Show($"{Name} —  {СurrentBehavior}");
            СurrentBehavior.UpdateAngle(this);
        }

        public void IsHeadingToWall(Point maxPosition)
        {


        }

        public virtual T GetDecorator<T>() where T : BaseFishDecorator
        {
            return null; // Для обычных рыб возвращаем null
        }

        public virtual void UpdatePosition()
        {
            Hunger-=0.01;
            if (!IsAlive || СurrentBehavior == null) return;

            // Сохраняем старую позицию
            Point oldPos = Position;

            // Двигаем рыбу
            СurrentBehavior.Move(this);

            // Применяем упрощённую обработку столкновений
            HandleWallCollision(СurrentBehavior.MaxPosition);

            // Если рыба не сдвинулась - экстренный разворот
            if (Position == oldPos)
            {
                CurrentAngle += Math.PI / 2 + (random.NextDouble() - 0.5);
                CurrentAngle = NormalizeAngle(CurrentAngle);
            }
        }

        public void HandleWallCollision(Point aquariumSize)
        {
            // Простые границы с отступом в половину размера рыбы
            double leftBound = Size.Width / 2;
            double rightBound = aquariumSize.X - Size.Width / 2;
            double topBound = Size.Height / 2;
            double bottomBound = aquariumSize.Y - Size.Height / 2;

            // Принудительное ограничение позиции
            Position = new Point(
                Math.Max(leftBound, Math.Min(rightBound, Position.X)),
                Math.Max(topBound, Math.Min(bottomBound, Position.Y))
            );

            // Проверяем столкновения и отражаем угол
            bool needBounce = false;

            if (Position.X <= leftBound || Position.X >= rightBound)
            {
                CurrentAngle = Math.PI - CurrentAngle; // Горизонтальное отражение
                needBounce = true;
            }

            if (Position.Y <= topBound || Position.Y >= bottomBound)
            {
                CurrentAngle = -CurrentAngle; // Вертикальное отражение
                needBounce = true;
            }

            if (needBounce)
            {
                // Добавляем небольшую случайность (5-10 градусов)
                double angleVariation = (random.NextDouble() - 0.5) * Math.PI / 9;
                CurrentAngle = NormalizeAngle(CurrentAngle + angleVariation);

                // Слегка отталкиваем от стены
                double pushDistance = Speed * 0.5;
                Position = new Point(
                    Position.X + Math.Cos(CurrentAngle) * pushDistance,
                    Position.Y + Math.Sin(CurrentAngle) * pushDistance
                );
            }
        }

        private double NormalizeAngle(double angle)
        {
            angle %= (2 * Math.PI);
            return angle < 0 ? angle + 2 * Math.PI : angle;
        }



        public static double Bounds(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

    }
}
