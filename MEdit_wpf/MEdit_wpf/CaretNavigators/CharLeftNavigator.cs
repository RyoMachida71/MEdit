using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class CharLeftNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition == TextPosition.Empty) return currentPosition;
            if (currentPosition.Column == 0) {
                var lineAbove = document.Lines[currentPosition.Row - 1];
                return new TextPosition(lineAbove.LineNumber, lineAbove.Length);
            }
            return new TextPosition(currentPosition.Row, currentPosition.Column - 1);
        }
    }
}
