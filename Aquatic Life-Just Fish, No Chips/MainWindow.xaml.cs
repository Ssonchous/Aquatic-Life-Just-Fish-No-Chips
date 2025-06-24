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
using System.Windows.Media.Animation;
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

        public enum FishClasses
        {
            Angelfish = 1,
            Barracuda,
            Goldfish,
            Piranha
        }
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.Topmost = true;

            aquarium = new Aquarium(new Point(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight));

            FishSettingsMenu.Children.Clear();
            foreach (var fish in Enum.GetValues(typeof(FishClasses)))
            {
                var fishButton = new Button
                {
                    Content = (FishClasses)fish,
                    Style = (Style)FindResource("MenuItemButton"),
                    Tag = fish
                };
                fishButton.Click += FishButton_Click;
                FishSettingsMenu.Children.Add(fishButton);
            }


            // Таймер для обновления
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MainCanvas.Children.Clear();
            aquarium.Update();

            
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

                if (obj is Food food)
                {
                    var foodImage = new Image
                    {
                        Source = food.Image,
                        Width = food.Size.Width,
                        Height = food.Size.Height
                    };

                    Canvas.SetLeft(foodImage, food.Position.X - food.Size.Width / 2);
                    Canvas.SetTop(foodImage, food.Position.Y - food.Size.Height / 2);
                    MainCanvas.Children.Add(foodImage);
                }

                if (obj is Bubble bubble)
                {
                    var bubbleImage = new Image
                    {
                        Source = bubble.Image,
                        Width = bubble.Size.Width,
                        Height = bubble.Size.Height
                    };

                    Canvas.SetLeft(bubbleImage, bubble.Position.X - bubble.Size.Width / 2);
                    Canvas.SetTop(bubbleImage, bubble.Position.Y - bubble.Size.Height / 2);
                    MainCanvas.Children.Add(bubbleImage);
                }

            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private bool isMenuOpen = false;
        private bool isAddFishOpen = false;

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen)
            {
                CloseMenuAnimation(DropdownMenu);
                if (isAddFishOpen)
                {
                    CloseMenuAnimation(FishSettingsMenu);
                    isAddFishOpen = false;
                }
            }
            else
            {
                OpenMenuAnimation(DropdownMenu);
            }
            isMenuOpen = !isMenuOpen;
        }

        private void AddFishButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAddFishOpen)
            {
                CloseMenuAnimation(FishSettingsMenu);
            }
            else
            {
                OpenMenuAnimation(FishSettingsMenu);
            }
            isAddFishOpen = !isAddFishOpen;
        }

        private void FishButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var fishType = (FishClasses)(button.Tag);
            var random = new Random();
            var x = random.Next(100, (int)SystemParameters.PrimaryScreenWidth - 100);
            var y = random.Next(100, (int)SystemParameters.PrimaryScreenHeight - 100);

            BaseFish newFish = null;

            switch (fishType)
            {
                case FishClasses.Angelfish:
                    newFish = Angelfish.Create(new Point(x, y));
                    break;
                case FishClasses.Barracuda:
                    newFish = Barracuda.Create(new Point(x, y));
                    break;
                case FishClasses.Goldfish:
                    newFish = Goldfish.Create(new Point(x, y));
                    break;
                case FishClasses.Piranha:
                    newFish = Piranha.Create(new Point(x, y));
                    break;
            }
            aquarium.AddFish(newFish);
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            aquarium.AddFood();
        }

        private void AddBubbleButton_Click(object sender, RoutedEventArgs e)
        {

            aquarium.AddBubble();
        }


        // Общие методы для анимации
        private void OpenMenuAnimation(FrameworkElement element)
        {
            element.Visibility = Visibility.Visible;
            var openAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            element.BeginAnimation(OpacityProperty, openAnimation);
        }

        private void CloseMenuAnimation(FrameworkElement element)
        {
            var closeAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            closeAnimation.Completed += (s, _) => element.Visibility = Visibility.Collapsed;
            element.BeginAnimation(OpacityProperty, closeAnimation);
        }
    }
}

        //private bool isMenuOpen = false;

        //private void MenuButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (isMenuOpen)
        //    {
        //        // Анимация закрытия меню
        //        var closeAnimation = new DoubleAnimation
        //        {
        //            To = 0,
        //            Duration = TimeSpan.FromSeconds(0.2)
        //        };
        //        closeAnimation.Completed += (s, _) => DropdownMenu.Visibility = Visibility.Collapsed;
        //        DropdownMenu.BeginAnimation(OpacityProperty, closeAnimation);
        //    }
        //    else
        //    {
        //        // Анимация открытия меню
        //        DropdownMenu.Visibility = Visibility.Visible;
        //        var openAnimation = new DoubleAnimation
        //        {
        //            From = 0,
        //            To = 1,
        //            Duration = TimeSpan.FromSeconds(0.2)
        //        };
        //        DropdownMenu.BeginAnimation(OpacityProperty, openAnimation);
        //    }
        //    isMenuOpen = !isMenuOpen;

        //    // Обновляем текст кнопки в зависимости от состояния
        //    MenuButton.Content = isMenuOpen ? "-" : "≡";
        //}

        //private void ExitButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}
        //private void HideButton_Click(object sender, RoutedEventArgs e)
        //{
        //    //Application.Current.Shutdown();
        //}

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ChangedButton == MouseButton.Left)
        //        this.DragMove();
        //}

        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape)
        //        this.Close();
        //}
    

