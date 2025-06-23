using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting
{
    public class Food : IAquaSitting
    {
        private static Random random = new Random();
        public bool IsAlive { get; set; } = true;
        public Point Position { get; set; }
        public Size Size { get; set; }
        private float fallSpeed;
        public ImageSource Image { get; set; }
        public int NutritionValue { get; }
        public Point MaxPosition { get; set; }

        public Food(Point maxPosition)
        {
            MaxPosition = maxPosition;
            Position = new Point(
               random.Next(0, (int)MaxPosition.X),
               random.Next(50, (int)(MaxPosition.Y * 0.2))
           );

            int rand = random.Next(20, 60);
            Size = new Size(rand, rand);
            fallSpeed = (float)Size.Width * 0.01f;
            Image = LoadImage("food.png");

            NutritionValue = random.Next(10, 30);
        }

        public void UpdatePosition()
        {
            Position = new Point(Position.X, Position.Y + fallSpeed);
            if (Position.Y > MaxPosition.Y) 
                IsAlive = false;
        }

        protected ImageSource LoadImage(string imageFileName)
        {
            // Путь к папке с изображениями
            string imagesFolder = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Img", "Other");

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
    }
}
