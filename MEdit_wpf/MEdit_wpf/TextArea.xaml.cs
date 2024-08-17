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
            _textLayer = new TextLayer(this, this);
            _visualText = new VisualText();
            _caretLayer = new CaretLayer(this, this);
            _caret = new SingleCaret(this, _caretLayer.BringCaretToView);
            AddVisualChild(_textLayer);
            AddVisualChild(_caretLayer);
            CommandBinder.SetBinding(this);
        }

        public ITextDocument Document => _document;

        public ICaret Caret => _caret;

        public VisualText VisualText => _visualText;

        public event EventHandler ScrollOffsetChanged;

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            var input = new TextInput(e.Text);
            _document.Replace(_caret.Selection.StartPosition, _caret.Selection.EndPosition, input);
            _visualText.BuildVisualLines(_document.Lines);
            _caretLayer.BringCaretToView();
        }
        public void OnTextDelete(EditingDirection direction) {
            _document.Delete(Caret.Selection.StartPosition, Caret.Selection.EndPosition, direction);
            _visualText.BuildVisualLines(_document.Lines);
            _caretLayer.BringCaretToView();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);

            this.OnScrollChange();
        }

        protected override Size ArrangeOverride(Size parentSize) {
            _textLayer.Arrange(new Rect(0, 0, parentSize.Width, parentSize.Height));
            _caretLayer.Arrange(new Rect(0, 0, parentSize.Width, parentSize.Height));
            return parentSize;
        }

        protected override int VisualChildrenCount => Enum.GetValues(typeof(LayerKind)).Length;

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
            if (offset == _horizontalOffset) return;

            _horizontalOffset = offset;
            OnScrollChange();
        }

        public void SetVerticalOffset(double offset) {
            offset = Math.Max(0, Math.Min(offset, ExtentHeight - ViewportHeight));
            if (offset == _horizontalOffset) return;

            _verticalOffset = offset;
            OnScrollChange();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) {
            if (rectangle.IsEmpty || visual == null || visual == this || !this.IsAncestorOf(visual)) {
                return Rect.Empty;
            }

            var visibleRect = new Rect(_horizontalOffset, _verticalOffset, ViewportWidth, ViewportHeight);
            if (rectangle.Left < visibleRect.Left) {
                _horizontalOffset = rectangle.Left;
            }
            else if (rectangle.Right > visibleRect.Right) {
                _horizontalOffset = rectangle.Right - this.ViewportWidth;
            }
            if (rectangle.Top < visibleRect.Top) {
                _verticalOffset = rectangle.Top;
            }
            else if (rectangle.Bottom > visibleRect.Bottom) {
                _verticalOffset = rectangle.Bottom - this.ViewportHeight;
            }

            OnScrollChange();
            return rectangle;
        }

        private void OnScrollChange() {
            this.ScrollOwner?.InvalidateScrollInfo();
            this.ScrollOffsetChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
