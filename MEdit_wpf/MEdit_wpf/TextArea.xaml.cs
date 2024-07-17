using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using MEdit_wpf.Layer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MEdit_wpf {
    /// <summary>
    /// TextArea.xaml の相互作用ロジック
    /// </summary>
    public partial class TextArea : Control, ITextArea, IScrollInfo {

        private TextDocument _document;
        private SingleCaret _caret;
        private CaretLayer _caretLayer;
        private VisualText _visualText;

        public TextArea() {
            InitializeComponent();
            _document = new TextDocument();
            _visualText = new VisualText();
            _caret = new SingleCaret(this, RenderCaret);
            _caretLayer = new CaretLayer();

            AddVisualChild(_caretLayer);
            CommandBinder.SetBinding(this, this.CommandBindings, this.InputBindings);
        }

        public ITextDocument Document => _document;

        public SingleCaret Caret => _caret;

        public void DeleteText(EditingDirection direction) {
            _document.Delete(Caret.Selection.StartPosition, Caret.Selection.EndPosition, direction);
            this.InvalidateVisual();
        }

        protected override int VisualChildrenCount => 1;

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            var input = new TextInput(e.Text);
            _document.Replace(_caret.Selection.StartPosition, _caret.Selection.EndPosition, input);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            _visualText.DrawVisualLines(dc, _document.Lines);
            RenderCaret();
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

        private void RenderCaret() {
            _caretLayer.Render(_visualText.GetCaretScreenPosition(_caret.Position), _visualText.GetSelectionScreenRects(_caret.Selection, _document));
        }

        private bool _canVerticallyScroll;
        public bool CanVerticallyScroll { get => _canVerticallyScroll; set => _canVerticallyScroll = value; }

        private bool _canHorizontallyScroll;
        public bool CanHorizontallyScroll { get => _canHorizontallyScroll; set => _canHorizontallyScroll = value; }

        public double ExtentWidth => throw new System.NotImplementedException();

        public double ExtentHeight => throw new System.NotImplementedException();

        public double ViewportWidth => throw new System.NotImplementedException();

        public double ViewportHeight => throw new System.NotImplementedException();

        public double HorizontalOffset => throw new System.NotImplementedException();

        public double VerticalOffset => throw new System.NotImplementedException();

        public ScrollViewer ScrollOwner { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void LineUp() {
            throw new System.NotImplementedException();
        }

        public void LineDown() {
            throw new System.NotImplementedException();
        }

        public void LineLeft() {
            throw new System.NotImplementedException();
        }

        public void LineRight() {
            throw new System.NotImplementedException();
        }

        public void PageUp() {
            throw new System.NotImplementedException();
        }

        public void PageDown() {
            throw new System.NotImplementedException();
        }

        public void PageLeft() {
            throw new System.NotImplementedException();
        }

        public void PageRight() {
            throw new System.NotImplementedException();
        }

        public void MouseWheelUp() {
            throw new System.NotImplementedException();
        }

        public void MouseWheelDown() {
            throw new System.NotImplementedException();
        }

        public void MouseWheelLeft() {
            throw new System.NotImplementedException();
        }

        public void MouseWheelRight() {
            throw new System.NotImplementedException();
        }

        public void SetHorizontalOffset(double offset) {
            throw new System.NotImplementedException();
        }

        public void SetVerticalOffset(double offset) {
            throw new System.NotImplementedException();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) {
            throw new System.NotImplementedException();
        }
    }
}
