using MEdit_wpf.Layer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MEdit_wpf {
    /// <summary>
    /// TextArea.xaml の相互作用ロジック
    /// </summary>
    public partial class TextArea : Control {

        private TextDocument _document;
        private Caret _caret;
        private CaretLayer _caretLayer;
        private VisualText _visualText;

        public event RoutedEventHandler TextAreaRendered;

        public TextArea() {
            InitializeComponent();
            _document = new TextDocument();
            _visualText = new VisualText();
            _caret = new Caret(_document);
            _caretLayer = new CaretLayer();

            AddVisualChild(_caretLayer);

            _caret.PositionChanged += Caret_PositionChanged;
            CaretCommandBinder.SetBinding(_caret, this.CommandBindings, this.InputBindings);
        }

        protected override int VisualChildrenCount => 1;

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            string input = (e.Text == "\r" || e.Text == "\n") ? "\r\n" : e.Text;
            _document.Insert(_caret.Offset, input);
            _caret.UpdatePos(input);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            _visualText.DrawVisualLines(dc, _document.Lines);
            _caretLayer.Render(_visualText.GetPhisicalPositionByLogicalOne(_caret.Row, _caret.Column));
        }

        protected override Visual GetVisualChild(int index) {
            switch (index) {
                case (int)LayerKind.Caret:
                    return _caretLayer;
                default:
                    return base.GetVisualChild(index);
            }
        }

        private void TextArea_GotFocus(object sender, RoutedEventArgs args) {
            if (this.IsFocused) {
                Keyboard.Focus(this);
            }
        }

        private void Caret_PositionChanged(object sender, RoutedEventArgs args) {
            _caretLayer.Render(_visualText.GetPhisicalPositionByLogicalOne(_caret.Row, _caret.Column));
        }
    }
}
