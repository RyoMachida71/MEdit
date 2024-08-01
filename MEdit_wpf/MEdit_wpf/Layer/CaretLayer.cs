using System.Windows;
using System.Windows.Media;

namespace MEdit_wpf.Layer {
    public class CaretLayer : FrameworkElement, IVisualLayer {
        private const double CaretWidth = 2;

        private ITextArea _textArea;
        private Brush ForeGround = Brushes.Black;
        private Brush SelectionBrush = new SolidColorBrush(Color.FromArgb(64, 0, 0, 255));


        public CaretLayer(ITextArea textArea)
        {
            _textArea = textArea;
        }

        public void Render() => InvalidateVisual();

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);

            var caretScreenPosition = _textArea.VisualText.GetCaretScreenPosition(_textArea.Caret.Position);
            dc.DrawRectangle(ForeGround, null, new Rect(caretScreenPosition.X, caretScreenPosition.Y, CaretWidth, _textArea.VisualText.LineHeight));

            foreach (var rect in _textArea.VisualText.GetSelectionScreenRects(_textArea.Caret.Selection, _textArea.Document))
            {
                dc.DrawRectangle(SelectionBrush, null, rect);
            }
        }
    }
}
