using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System.Linq;

namespace MEdit_wpf.CaretNavigators {
    internal class CharRightNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var currentLine = document.Lines[currentPosition.Row];
            var lastLineNumber = document.Lines.Max(x => x.LineNumber);
            var isAtLineEnd = currentPosition.Column == currentLine.Length;
            if (currentLine.LineNumber == lastLineNumber && isAtLineEnd) {
                return currentPosition;
            }
            if (isAtLineEnd) {
                return new TextPosition(currentPosition.Row + 1, 0);
            }
            return new TextPosition(currentPosition.Row, currentPosition.Column + 1);
        }
    }
}
