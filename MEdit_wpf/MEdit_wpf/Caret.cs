using MEdit_wpf.Selection;
using System;

namespace MEdit_wpf {
    public class Caret {

        private const string Eol = "\r\n";

        private TextDocument _document;

        private Action _showCaret;

        public Caret(TextDocument document, Action showCaret) {
            Position = new TextPosition(0, 0);
            _document = document;
            _showCaret = showCaret;
            Selection = new SingleSelection(document);
        }

        public TextPosition Position { get; set; }

        public ISelection Selection { get; private set; }

        public void UpdatePos(string input) {
            var row = this.Position.Row;
            var col = this.Position.Column;
            if (input == Eol) {
                ++row;
                col = 0;
            } else {
                col += input.Length;
            }
            this.Position = new TextPosition(row, col);
        }

        public void Move(CaretMovementType movement) {
            if (_document.Lines.Count == 0) return;

            switch (movement) {
                case CaretMovementType.None:
                    break;
                case CaretMovementType.Backspace:
                    break;
                case CaretMovementType.CharLeft:
                    MoveCharLeft();
                    break;
                case CaretMovementType.CharLeftExtendingSelection:
                    MoveCharLeftSeleccting();
                    break;
                case CaretMovementType.CharRight:
                    MoveCharRight();
                    break;
                case CaretMovementType.LineUp:
                    MoveLineUp();
                    break;
                case CaretMovementType.LineDown:
                    MoveLineDown();
                    break;
                case CaretMovementType.LineStart:
                    break;
                case CaretMovementType.LineEnd:
                    break;
                case CaretMovementType.WordLeft:
                    break;
                case CaretMovementType.WordRight:
                    break;
                case CaretMovementType.PageUp:
                    break;
                case CaretMovementType.PageDown:
                    break;
                case CaretMovementType.DocumentStart:
                    break;
                case CaretMovementType.DocumentEnd:
                    break;
                default:
                    break;
            }
            if (_showCaret != null) {
                _showCaret();
            }
        }

        private void MoveCharLeft() {
            if (this.Position.Column - 1 >= 0) {
                this.Position = new TextPosition(this.Position.Row, this.Position.Column - 1);
                return; 
            }
            var rowAfterMove = this.Position.Row - 1;
            if (rowAfterMove >= 0) {
                var newRow = rowAfterMove;
                var newCol = _document.Lines[rowAfterMove].Text.Length - Eol.Length;
                this.Position = new TextPosition(newRow, newCol);
            }
        }

        private void MoveCharLeftSeleccting() {
            var start = this.Position;
            MoveCharLeft();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveCharRight() {
            var colAfterMove = this.Position.Column + 1;
            var currentLine = _document.Lines[this.Position.Row];
            if (colAfterMove < currentLine.Text.Length - 1) {
                this.Position = new TextPosition(this.Position.Row, colAfterMove);
                return;
            }
            var rowAfterMove = this.Position.Row + 1;
            if (rowAfterMove <= _document.Lines.Count - 1) {
                this.Position = new TextPosition(rowAfterMove, 0);
            }
        }

        private void MoveLineUp() {
            var rowAfterMove = this.Position.Row - 1;
            if (rowAfterMove < 0) return;

            var upLine = _document.Lines[rowAfterMove];
            var col = this.Position.Column;
            if (this.Position.Column > upLine.Text.Length - Eol.Length) {
                col = upLine.Text.Length - Eol.Length;
            }
            this.Position = new TextPosition(rowAfterMove, col);
        }

        private void MoveLineDown() {
            var rowAfterMove = this.Position.Row + 1;
            if (rowAfterMove > _document.Lines.Count - 1) return;

            var downLine = _document.Lines[rowAfterMove];
            var col = this.Position.Column;
            if (this.Position.Column > downLine.Text.Length - Eol.Length) {
                col = downLine.Text.Length - Eol.Length;
            }
            this.Position = new TextPosition(rowAfterMove, col);
        }
    }
}
