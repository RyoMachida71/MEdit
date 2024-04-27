using System.Collections.Immutable;
using System.Windows;

namespace MEdit_wpf.Layer {
    public interface ICaretLayer {
        void Render(Point pos, IImmutableList<Rect> selectionRects);
    }
}
