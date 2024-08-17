using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MEdit_wpf.Layer {
    public class CaretLayer : FrameworkElement {

        private ITextArea _textArea;
        private IScrollInfo _scrollInfo;
        private Brush ForeGround = Brushes.Black;
        private Brush SelectionBrush = new SolidColorBrush(Color.FromArgb(64, 0, 0, 255));

        public CaretLayer(ITextArea textArea, IScrollInfo scrollInfo)
        {
            _textArea = textArea;
            _textArea.ScrollOffsetChanged += (s, e) => this.InvalidateVisual();
            _scrollInfo = scrollInfo;
        }

        public void BringCaretToView(){
            var caretScreenPosition = _textArea.VisualText.GetCaretScreenPosition(_textArea.Caret.Position);
            _scrollInfo.MakeVisible(this, new Rect(caretScreenPosition.X, caretScreenPosition.Y, SystemParameters.CaretWidth, _textArea.VisualText.LineHeight));
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);

            var caretScreenPosition = _textArea.VisualText.GetCaretScreenPosition(_textArea.Caret.Position);
            dc.DrawRectangle(ForeGround, null, new Rect(caretScreenPosition.X - _scrollInfo.HorizontalOffset,
                                                        caretScreenPosition.Y - _scrollInfo.VerticalOffset,
                                                        SystemParameters.CaretWidth,
                                                        _textArea.VisualText.LineHeight));

            foreach (var rect in _textArea.VisualText.GetSelectionScreenRects(_textArea.Caret.Selection, _textArea.Document))
            {
                rect.Offset(new Vector(-_scrollInfo.HorizontalOffset, -_scrollInfo.VerticalOffset));
                dc.DrawRectangle(SelectionBrush, null, rect);
            }
        }
    }
}
