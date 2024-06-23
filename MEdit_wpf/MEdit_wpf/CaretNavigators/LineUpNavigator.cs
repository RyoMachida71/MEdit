
using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class LineUpNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition.Row == 0) return currentPosition;

            var previousLine = document.Lines[currentPosition.Row - 1];
            var previousLineColumn = currentPosition.Column > previousLine.Length ? previousLine.Length : currentPosition.Column;
            return new TextPosition(previousLine.LineNumber, previousLineColumn);
        }
    }
}
