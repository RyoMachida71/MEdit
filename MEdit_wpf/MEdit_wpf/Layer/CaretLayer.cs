using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;


namespace MEdit_wpf.Layer {
    public class CaretLayer : FrameworkElement, ICaretLayer {
        private Point _renderPos;
        private List<Point> _renderPosList = new List<Point>();

        private const double CaretWidth = 2;
        private double CaretHeight = 12;
        private Brush ForeGround = Brushes.Black;

        public CaretLayer()
        {
        }

        public void Render(Point pos) {
            _renderPos = pos;
            _renderPosList.Add(pos);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            dc.DrawRectangle(ForeGround, null, new Rect(_renderPos.X, _renderPos.Y, CaretWidth, CaretHeight));
        }
    }
}
