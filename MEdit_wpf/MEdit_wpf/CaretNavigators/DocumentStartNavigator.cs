using MEdit_wpf.Caret;
using MEdit_wpf.Document;


namespace MEdit_wpf.CaretNavigators {
    internal class DocumentStartNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            return new TextPosition(0, 0);
        }
    }
}
