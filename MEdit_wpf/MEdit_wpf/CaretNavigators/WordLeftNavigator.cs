using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class WordLeftNavigator : ICaretNavigator {

        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition == TextPosition.Empty) return currentPosition;

            var line = document.Lines[currentPosition.Row];
            var parsingPosition = currentPosition;
            if (currentPosition.Column == 0) {
                line = document.Lines[currentPosition.Row - 1];
                parsingPosition = new TextPosition(line.LineNumber, line.Length);
            }

            for (var column = parsingPosition.Column - 1; column >= 0; --column) {
                if (column == 0) return new TextPosition(line.LineNumber, 0);

                if (char.IsWhiteSpace(line.Text[column])) continue;

                if (!char.IsWhiteSpace(line.Text[column]) && !char.IsLetterOrDigit(line.Text[column - 1])) {
                    return new TextPosition(line.LineNumber, column);
                }
            }

            return TextPosition.Empty;
        }
    }
}
