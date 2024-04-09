using System.Windows;
using System.Windows.Media;

namespace MEdit_wpf.Layer {
    public class CaretLayer : FrameworkElement, ICaretLayer {
        private Point _renderPos;

        private const double Width = 2;
        private double Height = 12;
        private Brush Color = Brushes.Black;

        public void Render(Point pos) {
            _renderPos = pos;
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            dc.DrawRectangle(Color, null, new Rect(_renderPos.X, _renderPos.Y, Width, Height));
        }
    }
}
