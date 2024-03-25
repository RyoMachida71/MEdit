using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    /// <summary>
    /// TextArea.xaml の相互作用ロジック
    /// </summary>
    public partial class TextArea : Control {

        private TextDocument _document;

        public TextArea() {
            InitializeComponent();
            _document = new TextDocument();
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
                var textRunProperty = new PlainTextRunProperty(new Typeface("Consolas"), 12, 12, Brushes.Black, Brushes.White, CultureInfo.InvariantCulture);
                var textRun = new PlainTextSource(line, textRunProperty);
                var visualLine = formatter.FormatLine(textRun
                                                , 0
                                                , (double)this.ActualWidth
                                                , new GeneralTextParagraphProperties(false, textRunProperty, textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                                , null);
                visualLine.Draw(dc, new Point(0, lineYPos), InvertAxes.None);
                lineYPos += visualLine.Height;
            }
        }

        private void TextArea_KeyDown(object sender, KeyEventArgs e) {
            // todo: キー補足
        }

        private void TextArea_GotFocus(object sender, RoutedEventArgs args) {
            if (this.IsFocused) {
                Keyboard.Focus(this);
            }
        }
    }
}
