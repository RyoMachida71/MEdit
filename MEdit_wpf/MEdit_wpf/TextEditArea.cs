using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MEdit_wpf {
    public class TextEditArea : Control {

        private StringBuilder _buffer = new StringBuilder();

        private Point _topLeft = new Point(0, 0);

        public TextEditArea() {
            this.GotFocus += this.TextEditArea_GotFocus;
        }

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            _buffer.Append(e.Text);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            var text = _buffer.ToString();
            var format = new FormattedText(text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), (double)10, Brushes.Black, (double)1);
            dc.DrawText(format, _topLeft);
        }

        private void TextEditArea_GotFocus(object sender, RoutedEventArgs args) {
            Keyboard.Focus(this);
        }
    }
}
