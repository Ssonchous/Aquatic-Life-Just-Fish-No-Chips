
using Aquatic_Life_Just_Fish__No_Chips.Classes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Fishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Fishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Aquatic_Life_Just_Fish__No_Chips
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml Aquatic Life-Just Fish, No Chips
    /// </summary>
    public partial class MainWindow : Window
    {
        private Aquarium aquarium;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.AllowsTransparency = true;
            this.Background = Brushes.Transparent;
            this.Topmost = true;

            // Создаем аквариум размером с экран
            aquarium = new Aquarium(new Point(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight));

            //var aquas = new List<IAquaSitting>();
            //aquas.Add(Barracuda.Create(new Point(500, 500))); // С декоратором
            //aquas.Add(Angelfish.Create(new Point(500, 500)));   // Без декоратора

            //foreach (var obj in aquas)
            //{
            //    // Приводим к BaseFish (все рыбы наследуются от него)
            //    if (obj is BaseFish baseFish)
            //    {
            //        // Пытаемся получить декораторы (если они есть)
            //        var hunter = baseFish.GetDecorator<HunterDecorator>();
            //        var schooling = baseFish.GetDecorator<SchoolingDecorator>();

            //        if (hunter != null)
            //            MessageBox.Show($"{baseFish.Name} — охотник (сила укуса: {hunter.BiteStrength})");

            //        if (schooling != null)
            //            MessageBox.Show($"{baseFish.Name} — стайная рыба");

            //        if (hunter == null && schooling == null)
            //            MessageBox.Show($"{baseFish.Name} — обычная рыба без декораторов");
            //    }
            //}

            aquarium.ProcessGenericFish(Barracuda.Create(new Point(700, 500)));
            aquarium.ProcessGenericFish(Goldfish.Create(new Point(300, 300)));
            aquarium.ProcessGenericFish(Piranha.Create(new Point(400, 400)));
            aquarium.ProcessGenericFish(Angelfish.Create(new Point(500, 500)));
            aquarium.ProcessGenericFish(Piranha.Create(new Point(100, 100)));


            
            //foreach (var obj in aquarium.Contents)
            //{
            //    // Приводим к BaseFish (все рыбы наследуются от него)
            //    if (obj is BaseFish baseFish)
            //    {
            //        // Пытаемся получить декораторы (если они есть)
            //        var hunter = baseFish.GetDecorator<HunterDecorator>();
            //        var schooling = baseFish.GetDecorator<SchoolingDecorator>();

            //        if (hunter != null)
            //            MessageBox.Show($"{baseFish.Name} — охотник (сила укуса: {hunter.BiteStrength})");

            //        if (schooling != null)
            //            MessageBox.Show($"{baseFish.Name} — стайная рыба");

            //        if (hunter == null && schooling == null)
            //            MessageBox.Show($"{baseFish.Name} — обычная рыба без декораторов");
            //    }
            //}

            // Таймер для обновления
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Очищаем канвас
            MainCanvas.Children.Clear();

            // Обновляем аквариум
            aquarium.Update();

            // Отрисовываем всех обитателей
            foreach (var obj in aquarium.Contents)
            {
                if (obj is BaseFish fish)
                {
                    var fishImage = new Image
                    {
                        Source = fish.Image,
                        Width = fish.Size.Width,
                        Height = fish.Size.Height,
                        RenderTransform = new RotateTransform(fish.CurrentAngle * 180 / Math.PI, fish.Size.Width / 2, fish.Size.Height / 2)
                    };

                    Canvas.SetLeft(fishImage, fish.Position.X - fish.Size.Width / 2);
                    Canvas.SetTop(fishImage, fish.Position.Y - fish.Size.Height / 2);

                    MainCanvas.Children.Add(fishImage);
                }

            }
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Добавляем еду по клику мыши
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Закрываем окно по нажатию Escape
            if (e.Key == System.Windows.Input.Key.Escape)
                this.Close();
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            BaseFish piranha = Piranha.Create(new Point(100, 100));


            MessageBox.Show("Ошибка: Изображение не загружено");

            var fishAsDecorator = piranha as BaseFishDecorator;
            var hunter = fishAsDecorator?.GetDecorator<HunterDecorator>();
            var schooling = fishAsDecorator?.GetDecorator<SchoolingDecorator>();



            var testWindow = new Window
            {
                Title = "Тест изображения пираньи",
                Content = new Image { Source = piranha.Image, Stretch = Stretch.Uniform },
                Width = 300,
                Height = 300
            };
            testWindow.ShowDialog();

        }
    }
}
