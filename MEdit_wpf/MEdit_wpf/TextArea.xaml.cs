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

        public double ExtentWidth => this.ActualWidth;

        public double ExtentHeight => this.ActualWidth;

        public double ViewportWidth => this.ActualWidth;

        public double ViewportHeight => this.ActualWidth;

        private double _horizontalOffset;
        public double HorizontalOffset => _horizontalOffset;

        private double _verticalOffset;
        public double VerticalOffset => _verticalOffset;

        ScrollViewer _scrollViewer;
        public ScrollViewer ScrollOwner {
            get {
                var parent = VisualTreeHelper.GetParent(this);
                if (parent is ScrollViewer scrollViewer) {
                    _scrollViewer = scrollViewer;
                }
                return _scrollViewer;
            }
            set { _scrollViewer = value; }
        }

        public void LineUp() {
            this.SetVerticalOffset(_verticalOffset - _visualText.LineHeight);
        }

        public void LineDown() {
            this.SetVerticalOffset(_verticalOffset + _visualText.LineHeight);
        }

        public void LineLeft() {
            this.SetHorizontalOffset(_horizontalOffset - _visualText.CharWidth);
        }

        public void LineRight() {
            this.SetHorizontalOffset(_horizontalOffset + _visualText.CharWidth);
        }

        public void PageUp() {
            this.SetVerticalOffset(_verticalOffset - this.ViewportHeight);
        }

        public void PageDown() {
            this.SetVerticalOffset(_verticalOffset + this.ViewportHeight);
        }

        public void PageLeft() {
            this.SetVerticalOffset(_horizontalOffset - this.ViewportWidth);
        }

        public void PageRight() {
            this.SetVerticalOffset(_horizontalOffset + this.ViewportWidth);
        }

        // ----仮実装----
        public void MouseWheelUp() {
            this.LineUp();
        }

        public void MouseWheelDown() {
            this.LineDown();
        }

        public void MouseWheelLeft() {
            this.LineLeft();
        }

        public void MouseWheelRight() {
            this.LineRight();
        }
        // --------------
        public void SetHorizontalOffset(double offset) {
            _horizontalOffset = offset;
            OnScrollChange();
        }

        public void SetVerticalOffset(double offset) {
            _verticalOffset = offset;
            OnScrollChange();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) {
            // todo: 仮実装
            return rectangle;
            /*
            if (rectangle.IsEmpty || visual == null || visual == this || !this.IsAncestorOf(visual)) {
                return Rect.Empty;
            }
            GeneralTransform childTransform = visual.TransformToAncestor(this);
            rectangle = childTransform.TransformBounds(rectangle);

            MakeVisible(Rect.Offset(rectangle, scrollOffset));

            return rectangle;
            */
        }

        private void OnScrollChange() {
            this.ScrollOwner?.InvalidateScrollInfo();
        }
    }
}
