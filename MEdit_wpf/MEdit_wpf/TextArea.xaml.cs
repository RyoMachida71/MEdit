using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using MEdit_wpf.Layer;
using System;
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
        private TextLayer _textLayer;
        private VisualText _visualText;

        public TextArea() {
            InitializeComponent();
            _document = new TextDocument();
            _visualText = new VisualText();
            _caretLayer = new CaretLayer(this, this);
            _caret = new SingleCaret(this, _caretLayer.Render);

            _textLayer = new TextLayer(this, this);
            AddVisualChild(_textLayer);
            AddVisualChild(_caretLayer);
            CommandBinder.SetBinding(this);
        }

        public ITextDocument Document => _document;

        public ICaret Caret => _caret;

        public VisualText VisualText => _visualText;

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            var input = new TextInput(e.Text);
            _document.Replace(_caret.Selection.StartPosition, _caret.Selection.EndPosition, input);
            _visualText.BuildVisualLines(_document.Lines);
            this.InvalidateLayers();
            this.OnScrollChange();
        }
        public void OnTextDelete(EditingDirection direction) {
            _document.Delete(Caret.Selection.StartPosition, Caret.Selection.EndPosition, direction);
            _visualText.BuildVisualLines(_document.Lines);
            this.InvalidateLayers();
            this.OnScrollChange();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);

            _caretLayer.Render();
            this.InvalidateLayers();
            this.OnScrollChange();
        }

        protected override Size ArrangeOverride(Size arrangeBounds) {
            _textLayer.Arrange(new Rect(0, 0, arrangeBounds.Width, arrangeBounds.Height));
            _caretLayer.Arrange(new Rect(0, 0, arrangeBounds.Width, arrangeBounds.Height));
            return arrangeBounds;
        }

        protected override int VisualChildrenCount => 2;

        protected override Visual GetVisualChild(int index) {
            switch (index) {
                case (int)LayerKind.Text:
                    return _textLayer;
                case (int)LayerKind.Caret:
                    return _caretLayer;
                default:
                    return base.GetVisualChild(index);
            }
        }

        private void InvalidateLayers() {
            _textLayer.Render();
            _caretLayer.Render();
        }

        private void TextArea_GotFocus(object sender, RoutedEventArgs args) {
            if (this.IsFocused) {
                Keyboard.Focus(this);
            }
        }

        public bool CanVerticallyScroll { get; set; }

        public bool CanHorizontallyScroll { get; set; }

        public double ExtentWidth => _visualText.MaxLineWidth;

        public double ExtentHeight => _visualText.TotalLineHeight;

        public double ViewportWidth => this.ActualWidth;

        public double ViewportHeight => this.ActualHeight;

        private double _horizontalOffset;
        public double HorizontalOffset => _horizontalOffset;

        private double _verticalOffset;
        public double VerticalOffset => _verticalOffset;

        public ScrollViewer ScrollOwner { get; set; }

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
            this.SetHorizontalOffset(_horizontalOffset - this.ViewportWidth);
        }

        public void PageRight() {
            this.SetHorizontalOffset(_horizontalOffset + this.ViewportWidth);
        }

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

        public void SetHorizontalOffset(double offset) {
            offset = Math.Max(0, Math.Min(offset, ExtentWidth - ViewportWidth));
            if (offset != _horizontalOffset) {
                _horizontalOffset = offset;
                OnScrollChange();
            }
        }

        public void SetVerticalOffset(double offset) {
            offset = Math.Max(0, Math.Min(offset, ExtentHeight - ViewportHeight));
            if (offset != _verticalOffset) {
                _verticalOffset = offset;
                OnScrollChange();
            }
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) {
            if (rectangle.IsEmpty || visual == null || visual == this || !this.IsAncestorOf(visual)) {
                return Rect.Empty;
            }

            rectangle = visual.TransformToAncestor(this).TransformBounds(rectangle);

            double offsetX = CalculateOffset(HorizontalOffset, rectangle.Left, rectangle.Right, ViewportWidth);
            double offsetY = CalculateOffset(VerticalOffset, rectangle.Top, rectangle.Bottom, ViewportHeight);

            SetHorizontalOffset(offsetX);
            SetVerticalOffset(offsetY);

            return rectangle;
        }

        private double CalculateOffset(double currentOffset, double start, double end, double viewport) {
            double newOffset = currentOffset;

            if (end > currentOffset + viewport) {
                newOffset = end - viewport;
            } else if (start < currentOffset) {
                newOffset = start;
            }

            return newOffset;
        }

        private void OnScrollChange() {
            this.ScrollOwner?.InvalidateScrollInfo();
            this.InvalidateLayers();
        }
    }
}
