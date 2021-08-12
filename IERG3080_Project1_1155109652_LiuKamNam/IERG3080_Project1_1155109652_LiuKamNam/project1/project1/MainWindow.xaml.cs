using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


namespace project1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        List<BitmapImage> bitmaps = new List<BitmapImage>();
        public void addBitmaps()
        {
            bitmaps.Add(BitmapToImageSource(Properties.Resources._1));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._2));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._3));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._4));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._5));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._6));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._7));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._8));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._9));
            bitmaps.Add(BitmapToImageSource(Properties.Resources._10));
        }

        List<Image> images = new List<Image>();
        public void addImages()
        {
            images.Add(a1);
            images.Add(a2);
            images.Add(a3);
            images.Add(a4);
            images.Add(a5);
            images.Add(a6);
            images.Add(a7);
            images.Add(a8);
            images.Add(a9);
            images.Add(a10);
            images.Add(a11);
            images.Add(a12);
            images.Add(a13);
            images.Add(a14);
            images.Add(a15);
            images.Add(a16);
            images.Add(a17);
            images.Add(a18);
            images.Add(a19);
            images.Add(a20);
        }


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int counter;

        private void StartTimer()
        {
            counter = 60;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            counter--;
            count.Text = counter + "s";
            if (counter == 0)
            {
                Init();
                MessageBox.Show("Time Over!" + "\n" + "You Lose!!!");
            }
        }

        private void StopTimer()
        {
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
        }

        public void hideImages()
        {
            foreach (var pic in images)
            {
                pic.Source = BitmapToImageSource(Properties.Resources._0);
                pic.Tag = null;
                pic.IsEnabled = true;
            }
        }

        Random rnd = new Random();
        int num;

        private Image getfreeslot()
        {
            int i = 1;
            while(i==1)
            {
                num = rnd.Next(0, images.Count);
                if (images[num].Tag == null)
                {
                    i = 0;
                }  
            }
            return images[num];
        }

        private void setrandome()
        {
            foreach(var pic in bitmaps)
            {
                var a = getfreeslot();
                a.Tag = pic;
            }
            foreach (var pic in bitmaps)
            {
                var a = getfreeslot();
                a.Tag = pic;
            }
        }

        private async void reset(Image guess1, Image firstGuess1)
        {
            await Task.Delay(500);
            guess1.Source = firstGuess1.Source = BitmapToImageSource(Properties.Resources._0);
            guess1.IsEnabled = firstGuess1.IsEnabled = true;
            allowClick = true;
        }

        bool allowClick = false;
        Image firstGuess;
        int chase=10;

        private void a_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (allowClick==false) return;
            ((Image)sender).IsEnabled = false;
            Image guess = ((Image)sender);
            if (firstGuess == null)
            {
                firstGuess = guess;
                foreach(var pic in bitmaps)
                {
                    if (((Image)sender).Tag == pic)
                    {
                        ((Image)sender).Source = pic;
                        return;
                    }
                }
            }
            foreach (var pic in bitmaps)
            {
                if (((Image)sender).Tag == pic)
                {
                        ((Image)sender).Source = pic;
                }
            }

            if(guess.Tag != firstGuess.Tag)
            {
                show.Foreground = Brushes.Red;
                show.Text = ("Incorrect");
                allowClick = false;
                reset(guess,firstGuess);
            }
            else
            {
                show.Foreground = Brushes.Green;
                show.Text = ("Correct");
                chase--;
            }
            firstGuess = null;
            if (chase == 0)
            {
                StopTimer();
                MessageBox.Show("You Win!");
            }
            return;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            addImages();
            addBitmaps();
            Init();
            StartTimer();
            allowClick = true;
            ((Button)sender).Visibility = Visibility.Hidden;
            restart.IsEnabled = true;
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            Init();
            StartTimer();
            allowClick = true;
        }

        private void Init()
        {
            count.Text = "60s";
            show.Text = ("");
            allowClick = false;
            chase = 10;
            hideImages();
            setrandome();
            StopTimer();
        }

    }
}
