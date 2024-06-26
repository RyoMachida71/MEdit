using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System;

namespace MEdit_wpf.CaretNavigators {
    internal class WordLeftNavigator : ICaretNavigator {
        private TextPosition _currentPosition;
        private ITextDocument _document;

        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition == TextPosition.Empty) return currentPosition;
            if (currentPosition.Column == 0) {
                var lineAbove = document.Lines[currentPosition.Row - 1];
                return new TextPosition(lineAbove.LineNumber, lineAbove.Length);
            }

            _currentPosition = currentPosition;
            _document = document;

            if (char.IsWhiteSpace(document.Lines[currentPosition.Row].Text, currentPosition.Column - 1)) {
                return GetNextPositionInternal((char target) => !char.IsWhiteSpace(target) || char.IsControl(target));
            } else {
                return GetNextPositionInternal((char target) => !char.IsLetterOrDigit(target) || char.IsControl(target));
            }
        }

        private TextPosition GetNextPositionInternal(Func<char, bool> shoulStop) {
            for (var row = _currentPosition.Row; row >= 0; --row) {
                var line = _document.Lines[row];
                var startColumn = row != _currentPosition.Row ? line.Length : _currentPosition.Column;
                for (int column = startColumn - 1; column >= 0; --column) {
                    if (shoulStop(line.Text[column])) {
                        return new TextPosition(row, column + 1);
                    }
                }
            }
            return TextPosition.Empty;
        }
    }
}
