using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf.TextRendering {
    public class DrawingTextVisual : DrawingVisual {
        public DrawingTextVisual Create(VisualText visualText) {
            using (var dc = this.RenderOpen()) {
                double yPos = 0;
                foreach (var visualLine in visualText.VisualTextLines) {
                    visualLine.Draw(dc, new Point(0, yPos), InvertAxes.None);
                    yPos += visualLine.TextHeight;
                }
                return this;
            }
        }
    }
}
