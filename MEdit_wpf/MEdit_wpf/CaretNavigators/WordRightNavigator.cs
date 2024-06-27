using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System;
using System.Linq;

namespace MEdit_wpf.CaretNavigators {
    internal class WordRightNavigator : ICaretNavigator {
        private TextPosition _currentPosition;
        private ITextDocument _document;
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var lastLine = document.Lines.Last();
            if (currentPosition == new TextPosition(lastLine.LineNumber, lastLine.Length)) return currentPosition;

            var line = document.Lines[currentPosition.Row];
            if (currentPosition.Column == line.Length) return new TextPosition(currentPosition.Row + 1, 0);

            _currentPosition = currentPosition;
            _document = document;
            if (char.IsWhiteSpace(line.Text[currentPosition.Column])) {
                return GetNextPositionInternal((char target) => !char.IsWhiteSpace(target) || char.IsControl(target));
            } else {
                return GetNextPositionInternal((char target) => !char.IsLetterOrDigit(target) || char.IsControl(target));
            }
        }

        private TextPosition GetNextPositionInternal(Func<char, bool> shoulStop) {
            for (var row = _currentPosition.Row; row < _document.Lines.Count; ++row) {
                var line = _document.Lines[row];
                var startColumn = row == _currentPosition.Row ? _currentPosition.Column : 0;
                for (int column = startColumn; column < line.Length; ++column) {
                    if (shoulStop(line.Text[column])) {
                        return new TextPosition(row, column);
                    }
                }
            }

            var lastLine = _document.Lines.Last();
            return new TextPosition(lastLine.LineNumber, lastLine.Length);
        }
    }
}
