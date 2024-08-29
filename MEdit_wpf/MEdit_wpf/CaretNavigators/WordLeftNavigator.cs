using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class WordLeftNavigator : ICaretNavigator {

        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition == TextPosition.Empty) return currentPosition;

            var isLineStart = currentPosition.Column == 0;
            var line = isLineStart ? document.Lines[currentPosition.Row - 1] : document.Lines[currentPosition.Row];
            var parsingPosition = isLineStart ? new TextPosition(line.LineNumber, line.Length) : currentPosition;

            if (parsingPosition.Column == 0) return new TextPosition(line.LineNumber, 0);

            for (var column = parsingPosition.Column - 1; column >= 0; --column) {
                if (column == 0) return new TextPosition(line.LineNumber, 0);

                if (char.IsWhiteSpace(line.Text[column])) continue;

                if (!char.IsLetterOrDigit(line.Text[column - 1])) {
                    return new TextPosition(line.LineNumber, column);
                }
            }

            return TextPosition.Empty;
        }
    }
}
