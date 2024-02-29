using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace drill_uwp {
    public sealed partial class MyUserControl1 : UserControl {
        public MyUserControl1() {
            this.InitializeComponent();
        }

        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args) {
            args.DrawingSession.DrawText("test", 0, 0, Colors.Blue);
            args.DrawingSession.DrawRectangle(new Rect(0, 0, this.ActualWidth, this.ActualHeight), color: Colors.Blue);
        }
    }
}
