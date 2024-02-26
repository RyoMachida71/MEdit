using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace drill_uwp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random rnd = new Random();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private Vector2 RndPosition() {
            var x = rnd.NextDouble() * 500f;
            var y = rnd.NextDouble() * 500f;
            return new Vector2((float)x, (float)y);
        }

        private float RndRadius() => (float)rnd.NextDouble() * 150f;

        private byte RndByte() => (byte)rnd.Next(256);

        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args) {
            for (int i = 0; i < 100; ++i) {
                args.DrawingSession.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
                args.DrawingSession.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
                args.DrawingSession.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            }
        }

        private void Page_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }
    }
}
