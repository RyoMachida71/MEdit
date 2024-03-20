using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class TextEditArea : Control {

        private TextDocument _document;

        private Point _topLeft = new Point(0, 0);

        public TextEditArea() {
            _document = new TextDocument();
            this.GotFocus += TextEditArea_GotFocus;
        }

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            // todo: キャレットの位置にinsert
            _document.Insert(_document.Text.Length, e.Text);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            var rs = new StringReader(_document.Text);
            double lineYPos = 0;
            while (rs.Peek() > -1) {
                var line = rs.ReadLine();
                var formatter = TextFormatter.Create();
                var textRunProperty = new PlainTextRunProperty(new Typeface("ＭＳ ゴシック"), 12, 12, Brushes.Black, Brushes.White, CultureInfo.InvariantCulture);
                var textRun = new PlainTextSource(line, textRunProperty);
                var visualLine = formatter.FormatLine(textRun
                                                , 0
                                                , 400
                                                , new GeneralTextParagraphProperties(false, textRunProperty, textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                                , null);
                visualLine.Draw(dc, new Point(0, lineYPos), InvertAxes.None);
                lineYPos += visualLine.Height;
            }
        }

        private void TextEditArea_GotFocus(object sender, RoutedEventArgs args) {
            Keyboard.Focus(this);
        }
    }
}
