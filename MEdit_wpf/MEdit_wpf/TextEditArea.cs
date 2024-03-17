using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

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
            if (text.Length <= 0) return;
            var formatter = TextFormatter.Create();
            var textRunProperty = new PlainTextRunProperty(new Typeface("Verdana"), 10, 10, Brushes.Black, Brushes.White, CultureInfo.InvariantCulture);
            var textRun = new PlainTextSource(text, textRunProperty);
            // todo: 各種引数の設定
            var line = formatter.FormatLine(textRun
                                            , 0
                                            , 400
                                            , new GeneralTextParagraphProperties(false, textRunProperty, 10, new GeneralTextMarkerProperties(0, textRun))
                                            , null);
            line.Draw(dc, _topLeft, InvertAxes.None);
        }

        private void TextEditArea_GotFocus(object sender, RoutedEventArgs args) {
            Keyboard.Focus(this);
        }
    }
}
