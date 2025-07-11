﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting
{
    public class Bubble : IAquaSitting
    {
        private static Random random = new Random();
        public bool IsAlive { get; set; } = true;
        public Point Position { get; set; }
        public Size Size { get; set; }
        public ImageSource Image { get; set; }
        private float riseSpeed;

        public Bubble(Point MaxPosition)
        {
            Position = new Point(
               random.Next((int)(MaxPosition.X * 0.1), (int)(MaxPosition.X * 0.9)),
               random.Next((int)(MaxPosition.Y * 0.5), (int)(MaxPosition.Y))
           );
            int rand = random.Next(30,90);
            Size = new Size(rand, rand);
            riseSpeed = (float)Size.Width * 0.03f;
            Image = LoadImage("bubble.png");
        }

        public void UpdatePosition()
        {
            Position = new Point(Position.X, Position.Y - riseSpeed);
            if (Position.Y < 0) IsAlive = false;
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
