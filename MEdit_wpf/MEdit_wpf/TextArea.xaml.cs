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

        private Caret _caret;

        private CaretLayer _caretLayer;

        public event RoutedEventHandler TextAreaRendered;

        public TextArea() {
            InitializeComponent();
            Document = new TextDocument();
            VisualText = new VisualText();
            _caret = new Caret(this);
            _caretLayer = new CaretLayer();

            AddVisualChild(_caretLayer);

            CaretCommandBinder.SetBinding(_caret, this.CommandBindings, this.InputBindings);
        }

        public ICaretLayer CaretLayer => _caretLayer;

        public VisualText VisualText { get; private set; }

        public TextDocument Document { get; private set; }

        protected override int VisualChildrenCount => 1;

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            string input = (e.Text == "\r" || e.Text == "\n") ? "\r\n" : e.Text;
            Document.Insert(_caret.Offset, input);
            _caret.UpdatePos(input);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            VisualText.DrawVisualLines(dc, Document.Lines);

            if (TextAreaRendered != null) {
                TextAreaRendered(this, new RoutedEventArgs());
            }
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
    }
}
