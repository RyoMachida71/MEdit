using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class LineEndNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var currentLine = document.Lines[currentPosition.Row];
            return new TextPosition(currentLine.LineNumber, currentLine.Length);
        }
    }
}
