
using System;
using System.Windows;

namespace MEdit_wpf {
    public class Caret {

        private const string Eol = "\r\n";

        private TextDocument _document;

        private Action _invalidateVisual;

        public Caret(TextDocument document, Action invalidateVisual) {
            Row = Column = 0;
            _document = document;
            _invalidateVisual = invalidateVisual;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public int Offset =>_document.GetOffsetByLine(Row, Column);

        public event RoutedEventHandler CaretPositionChanged;

        public void UpdatePos(string input) {
            if (input == Eol) {
                ++this.Row;
                this.Column = 0;
            } else {
                this.Column += input.Length;
            }
        }

        public void OnMove(CaretMovementType movement) {
            if (_document.Lines.Count == 0) return;

            switch (movement) {
                case CaretMovementType.None:
                    break;
                case CaretMovementType.Backspace:
                    break;
                case CaretMovementType.CharLeft:
                    MoveCharLeft();
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
            if (CaretPositionChanged != null) {
                CaretPositionChanged(this, new RoutedEventArgs());
            }
            _invalidateVisual();
        }

        private void MoveCharLeft() {
            if (this.Column - 1 >= 0) {
                --this.Column;
                return; 
            }
            var rowAfterMove = this.Row - 1;
            if (rowAfterMove >= 0) {
                this.Row = rowAfterMove;
                this.Column = _document.Lines[rowAfterMove].Text.Length - Eol.Length;
            }
        }

        private void MoveCharRight() {
            var colAfterMove = this.Column + 1;
            var currentLine = _document.Lines[this.Row];
            if (colAfterMove < currentLine.Text.Length - 1) {
                this.Column = colAfterMove;
                return;
            }
            var rowAfterMove = this.Row + 1;
            if (rowAfterMove <= _document.Lines.Count - 1) {
                this.Row = rowAfterMove;
                this.Column = 0;
            }
        }

        private void MoveLineUp() {
            var rowAfterMove = this.Row - 1;
            if (rowAfterMove < 0) return;

            this.Row = rowAfterMove;
            var upLine = _document.Lines[rowAfterMove];
            if (this.Column > upLine.Text.Length - Eol.Length) {
                this.Column = upLine.Text.Length - Eol.Length;
            }
        }

        private void MoveLineDown() {
            var rowAfterMove = this.Row + 1;
            if (rowAfterMove > _document.Lines.Count - 1) return;

            this.Row = rowAfterMove;
            var downLine = _document.Lines[rowAfterMove];
            if (this.Column > downLine.Text.Length - Eol.Length) {
                this.Column = downLine.Text.Length - Eol.Length;
            }
        }
    }
}
