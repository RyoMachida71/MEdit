using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf.Layer {
    public class TextLayer : FrameworkElement, IVisualLayer {
        private ITextArea _textArea;
        private IScrollInfo _scrollInfo;
        private DrawingTextVisual _drawingTextVisual;

        public TextLayer(ITextArea textArea, IScrollInfo scrollInfo)
        {
            _textArea = textArea;
            _scrollInfo = scrollInfo;
        }

        public void Render() => InvalidateVisual();

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);

            this.RemoveVisualChild(_drawingTextVisual);
            _drawingTextVisual = new DrawingTextVisual().Create(_textArea);
            this.AddVisualChild(_drawingTextVisual);
            _drawingTextVisual.Transform = new TranslateTransform(-_scrollInfo.HorizontalOffset, -_scrollInfo.VerticalOffset);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index) {
            return _drawingTextVisual;
        }

        private class DrawingTextVisual : DrawingVisual {
            internal DrawingTextVisual Create(ITextArea textArea) {
                using (var dc = this.RenderOpen()) {
                    double yPos = 0;
                    foreach (var visualLine in textArea.VisualText.VisualTextLines) {
                        visualLine.Draw(dc, new Point(0, yPos), InvertAxes.None);
                        yPos += visualLine.TextHeight;
                    }
                    return this;
                }
            }
        }
    }
}
