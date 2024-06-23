using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class LineStartNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            return new TextPosition(currentPosition.Row, 0);
        }
    }
}
