using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TDD_Jumper
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer;
        int speed = 0;
        Rect stickR, boxR;

        public MainPage() {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Tick;
            timer.Start();

            DataContext = this;
            stickR = new Rect(20, 200, 100, 150);
            boxR = new Rect(600, 300, 50, 200);
            Window.Current.CoreWindow.KeyDown += CoreWindonw_KeyDown;
        }

        private void Tick(object sender, object e) {
            ++speed;
            stickR.Y = Math.Min(200, stickR.Y + speed);
            Stickman.SetValue(Canvas.TopProperty, stickR.Y);

            boxR.X = boxR.X > -100 ? boxR.X - 10 : 600;
            Box.SetValue(Canvas.LeftProperty, boxR.X);

            if (RectHelper.Intersect(boxR, stickR) != Rect.Empty) {
                GameOver.Visibility = Visibility.Visible;
                timer.Stop();
            }
        }

        private void CoreWindonw_KeyDown(CoreWindow sender, KeyEventArgs e) {
            if (e.VirtualKey == Windows.System.VirtualKey.Space && stickR.Y == 200) {
                speed = -17;
            }
        }
    }
}
