using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System.Linq;

namespace MEdit_wpf.CaretNavigators {
    internal class WordRightNavigator : ICaretNavigator {

        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var lastLine = document.Lines.Last();
            if (currentPosition == new TextPosition(lastLine.LineNumber, lastLine.Length)) return currentPosition;

            var isLineEnd = currentPosition.Column == document.Lines[currentPosition.Row].Length;
            var line = isLineEnd ? document.Lines[currentPosition.Row + 1] : document.Lines[currentPosition.Row];
            var parsingPosition = isLineEnd ? new TextPosition(line.LineNumber, 0) : currentPosition;

            for ( var column = parsingPosition.Column; column <= line.Length; ++column) {
                if (column == line.Length) return new TextPosition(line.LineNumber, line.Length);

                if (char.IsWhiteSpace(line.Text[column])) continue;

                if (column + 1 == line.Length || !char.IsLetterOrDigit(line.Text[column + 1])) {
                    return new TextPosition(line.LineNumber, column + 1);
                }
            }

            return TextPosition.Empty;
        }
    }
}
