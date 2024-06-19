using MEdit_wpf.Document;

namespace MEdit_wpf.Caret {
    public interface ICaretNavigator {
        TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document);
    }
}
