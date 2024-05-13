﻿using MEdit_wpf.Selection;
using System;

namespace MEdit_wpf {
    public class Caret {

        private ITextArea _textArea;

        private Action _showCaret;

        public Caret(ITextArea textArea, Action showCaret) {
            Position = new TextPosition(0, 0);
            _textArea = textArea;
            _showCaret = showCaret;
            Selection = new SingleSelection(textArea);
        }

        public TextPosition Position { get; set; }

        public ISelection Selection { get; private set; }

        public void UpdatePos(string input) {
            var row = this.Position.Row;
            var col = this.Position.Column;
            if (input == TextDocument.EndOfLine) {
                ++row;
                col = 0;
            } else {
                col += input.Length;
            }
            this.Position = new TextPosition(row, col);
        }

        public void Move(CaretMovementType movement) {
            if (_textArea.Document.Lines.Count == 0) return;

            switch (movement) {
                case CaretMovementType.None:
                    break;
                case CaretMovementType.Backspace:
                    break;
                case CaretMovementType.CharLeft:
                    MoveCharLeft();
                    break;
                case CaretMovementType.CharLeftExtendingSelection:
                    MoveCharLeftSelecting();
                    break;
                case CaretMovementType.CharRight:
                    MoveCharRight();
                    break;
                case CaretMovementType.CharRightExtendingSelection:
                    MoveCharRightSelecting();
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
                var newCol = _textArea.Document.Lines[rowAfterMove].Text.Length - TextDocument.EndOfLine.Length;
                this.Position = new TextPosition(newRow, newCol);
            }
        }

        private void MoveCharLeftSelecting() {
            var start = this.Position;
            MoveCharLeft();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveCharRight() {
            var colAfterMove = this.Position.Column + 1;
            var currentLine = _textArea.Document.Lines[this.Position.Row];
            if (colAfterMove < currentLine.Text.Length - 1) {
                this.Position = new TextPosition(this.Position.Row, colAfterMove);
                return;
            }
            var rowAfterMove = this.Position.Row + 1;
            if (rowAfterMove <= _textArea.Document.Lines.Count - 1) {
                this.Position = new TextPosition(rowAfterMove, 0);
            }
        }

        private void MoveCharRightSelecting()
        {
            var start = this.Position;
            MoveCharRight();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveLineUp() {
            var rowAfterMove = this.Position.Row - 1;
            if (rowAfterMove < 0) return;

            var upLine = _textArea.Document.Lines[rowAfterMove];
            var col = this.Position.Column;
            if (this.Position.Column > upLine.Text.Length - TextDocument.EndOfLine.Length) {
                col = upLine.Text.Length - TextDocument.EndOfLine.Length;
            }
            this.Position = new TextPosition(rowAfterMove, col);
        }

        private void MoveLineDown() {
            var rowAfterMove = this.Position.Row + 1;
            if (rowAfterMove > _textArea.Document.Lines.Count - 1) return;

            var downLine = _textArea.Document.Lines[rowAfterMove];
            var col = this.Position.Column;
            if (this.Position.Column > downLine.Text.Length - TextDocument.EndOfLine.Length) {
                col = downLine.Text.Length - TextDocument.EndOfLine.Length;
            }
            this.Position = new TextPosition(rowAfterMove, col);
        }
    }
}
