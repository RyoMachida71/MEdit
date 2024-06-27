using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System.Linq;

namespace MEdit_wpf.CaretNavigators {
    internal class CharRightNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var lastLine = document.Lines.Last();
            if (currentPosition == new TextPosition(lastLine.LineNumber, lastLine.Length)) return currentPosition;

            var currentLine = document.Lines[currentPosition.Row];
            if (currentPosition.Column == currentLine.Length) return new TextPosition(currentPosition.Row + 1, 0);

            
            return new TextPosition(currentPosition.Row, currentPosition.Column + 1);
        }
    }
}
