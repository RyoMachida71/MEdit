using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System;

namespace MEdit_wpf.CaretNavigators {
    internal class WordLeftNavigator : ICaretNavigator {

        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition == TextPosition.Empty) return currentPosition;

            var parsingPosition = currentPosition;
            if (currentPosition.Column == 0) {
                var lineAbove = document.Lines[currentPosition.Row - 1];
                parsingPosition = new TextPosition(lineAbove.LineNumber, lineAbove.Length);
            }
            return GetNextPositionInternal(parsingPosition, document);
        }

        private TextPosition GetNextPositionInternal(TextPosition position, ITextDocument document) {
            for (var row = position.Row; row >= 0; --row) {
                var line = document.Lines[row];
                var startColumn = row != position.Row ? line.Length : position.Column;
                for (int column = startColumn - 1; column >= 0; --column) {
                    if (column == 0) return new TextPosition(row, 0);
                    if (char.IsWhiteSpace(line.Text[column])) continue;

                    if (!char.IsWhiteSpace(line.Text[column]) && !char.IsLetterOrDigit(line.Text[column - 1])) {
                        return new TextPosition(row, column);
                    }
                }
            }
            return TextPosition.Empty;
        }
    }
}
