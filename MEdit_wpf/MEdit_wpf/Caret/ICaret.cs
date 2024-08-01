
using MEdit_wpf.Selection;

namespace MEdit_wpf.Caret {
    public interface ICaret {
        TextPosition Position { get; set; }
        ISelection Selection { get; }
        void Move(ICaretNavigator navigator, bool isSelectionMode);
    }
}
