using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System.Linq;

namespace MEdit_wpf.CaretNavigators {
    internal class LineDownNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var lastLineNumber = document.Lines.Max(x => x.LineNumber);
            if (currentPosition.Row == lastLineNumber) return currentPosition;

            var nextLine = document.Lines[currentPosition.Row + 1];
            var nextLineColumn = currentPosition.Column > nextLine.Length ? nextLine.Length : currentPosition.Column;
            return new TextPosition(nextLine.LineNumber, nextLineColumn);
        }
    }
}
