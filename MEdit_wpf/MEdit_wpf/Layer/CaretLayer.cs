using System.Windows;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Windows.Media;


namespace MEdit_wpf.Layer {
    public class CaretLayer : FrameworkElement, ICaretLayer {
        private Point _renderPos;
        private IImmutableList<Rect> _selectionRects = new List<Rect>().ToImmutableList();

        private const double CaretWidth = 2;
        private double CaretHeight = 12;
        private Brush ForeGround = Brushes.Black;
        private Brush SelectionBrush = new SolidColorBrush(Color.FromArgb(64, 0, 0, 255));

        public CaretLayer()
        {
        }

        public void Render(Point pos, IImmutableList<Rect> selectionRects) {
            _renderPos = pos;
            _selectionRects = selectionRects;
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            dc.DrawRectangle(ForeGround, null, new Rect(_renderPos.X, _renderPos.Y, CaretWidth, CaretHeight));

            foreach (var rect in _selectionRects)
            {
                dc.DrawRectangle(SelectionBrush, null, rect);
            }
        }
    }
}
