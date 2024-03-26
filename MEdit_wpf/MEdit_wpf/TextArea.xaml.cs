using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    /// <summary>
    /// TextArea.xaml の相互作用ロジック
    /// </summary>
    public partial class TextArea : Control {

        private TextDocument _document;

        private Caret _caret;

        public TextArea() {
            InitializeComponent();
            _document = new TextDocument();
            _caret = new Caret(_document);
        }

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            // todo: キャレットの位置にinsert
            string input = (e.Text == "\r" || e.Text == "\n") ? "\r\n" : e.Text;
            _document.Insert(_caret.Column, input);
            _caret.UpdatePos(input);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            var renderer = new VisualText();
            renderer.DrawVisualLines(dc, _document.Lines);

            // todo: キャレットの描画クラスを作成する
            var textLine = renderer.GetLocationByRow(_caret.Row);
            double xPos, yPos;
            if (textLine == null) {
                xPos = 0;
                yPos = 0;
            } else {
                xPos = textLine.GetDistanceFromCharacterHit(new CharacterHit(_caret.Column, 0));
                yPos = textLine.Height * _caret.Row - textLine.Height;
                if (yPos < 0) yPos = 0;
            }
            dc.DrawRectangle(Brushes.Black, null, new Rect(xPos, yPos, 2, 12));
        }

        private void TextArea_KeyDown(object sender, KeyEventArgs e) {
            // todo: コマンドバインディングを使用する
        }

        private void TextArea_GotFocus(object sender, RoutedEventArgs args) {
            if (this.IsFocused) {
                Keyboard.Focus(this);
            }
        }
    }
}
