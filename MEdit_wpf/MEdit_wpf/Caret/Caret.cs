using MEdit_wpf.Document;
using MEdit_wpf.Selection;
using System;
using System.Linq;

namespace MEdit_wpf {
    public class Caret {

        private ITextArea _textArea;

        private Action _showCaret;

        public Caret(ITextArea textArea, Action showCaret) {
            Position = new TextPosition(0, 0);
            _textArea = textArea;
            _showCaret = showCaret;
            Selection = new SingleSelection();

            _textArea.Document.DocumentChanged += DocumentChanged;
        }

        public TextPosition Position { get; set; }

        public ISelection Selection { get; private set; }

        private void DocumentChanged(object sender, DocumentChangedEventArgs e) {
            this.Position = e.NewPosition;
            this.Selection.Unselect(this.Position);
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
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.CharLeftExtendingSelection:
                    MoveCharLeftSelecting();
                    break;
                case CaretMovementType.CharRight:
                    MoveCharRight();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.CharRightExtendingSelection:
                    MoveCharRightSelecting();
                    break;
                case CaretMovementType.LineUp:
                    MoveLineUp();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.LineUpExtendingSelection:
                    MoveLineUpSelecting();
                    break;
                case CaretMovementType.LineDown:
                    MoveLineDown();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.LineDownExtendingSelection:
                    MoveLineDownSelecting();
                    break;
                case CaretMovementType.LineStart:
                    MoveToLineStart();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.LineStartExtendingSelection:
                    MoveToLineStartSelecting();
                    break;
                case CaretMovementType.LineEnd:
                    MoveToLineEnd();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.LineEndExtendingSelection:
                    MoveToLineEndSelecting();
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
                    MoveToDocumentStart();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.DocumentStartSelection:
                    MoveToDocumentStartSelecting();
                    break;
                case CaretMovementType.DocumentEnd:
                    MoveToDocumentEnd();
                    this.Selection.Unselect(this.Position);
                    break;
                case CaretMovementType.DocumentEndSelection:
                    MoveToDocumentEndSelecting();
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
                var newCol = _textArea.Document.Lines[rowAfterMove].Length;
                this.Position = new TextPosition(newRow, newCol);
            }
        }

        private void MoveCharLeftSelecting() {
            var start = this.Position;
            MoveCharLeft();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveCharRight() {
            var lines = _textArea.Document.Lines;
            var colAfterMove = this.Position.Column + 1;
            var currentLine = lines[this.Position.Row];
            if (colAfterMove <= currentLine.Text.Length) {
                this.Position = new TextPosition(this.Position.Row, colAfterMove);
                return;
            }
            var rowAfterMove = this.Position.Row + 1;
            if (rowAfterMove <= lines.Count - 1) {
                this.Position = new TextPosition(rowAfterMove, 0);
            }
        }

        private void MoveCharRightSelecting() {
            var start = this.Position;
            MoveCharRight();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveLineUp() {
            var rowAfterMove = this.Position.Row - 1;
            if (rowAfterMove < 0) return;

            var upLine = _textArea.Document.Lines[rowAfterMove];
            var col = this.Position.Column;
            if (this.Position.Column > upLine.Length) {
                col = upLine.Length;
            }
            this.Position = new TextPosition(rowAfterMove, col);
        }

        private void MoveLineUpSelecting() {
            var start = this.Position;
            MoveLineUp();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveLineDown() {
            var lines = _textArea.Document.Lines;
            var rowAfterMove = this.Position.Row + 1;
            if (rowAfterMove > lines.Count - 1) return;

            var downLine = lines[rowAfterMove];
            var col = this.Position.Column;
            if (this.Position.Column > downLine.Length) {
                col = downLine.Length;
            }
            this.Position = new TextPosition(rowAfterMove, col);
        }

        private void MoveLineDownSelecting() {
            var start = this.Position;
            MoveLineDown();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveToLineStart() {
            if (_textArea.Document.Lines.Count == 0) return;
            this.Position = new TextPosition(this.Position.Row, 0);
        }

        private void MoveToLineStartSelecting() {
            var start = this.Position;
            MoveToLineStart();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveToLineEnd() {
            var line = _textArea.Document.Lines[this.Position.Row];
            this.Position = new TextPosition(this.Position.Row, line.Length);
        }

        private void MoveToLineEndSelecting() {
            var start = this.Position;
            MoveToLineEnd();
            Selection.StartOrExtend(start, this.Position);
        }


        private void MoveToDocumentStart() => this.Position = TextPosition.Empty;

        private void MoveToDocumentStartSelecting() {
            var start = this.Position;
            MoveToDocumentStart();
            Selection.StartOrExtend(start, this.Position);
        }

        private void MoveToDocumentEnd() {
            var lines = _textArea.Document.Lines;
            var lastLine = lines.Find(x => x.LineNumber == lines.Max(y => y.LineNumber));
            this.Position = new TextPosition(lastLine.LineNumber, lastLine.Length);
        }

        private void MoveToDocumentEndSelecting() {
            var start = this.Position;
            MoveToDocumentEnd();
            Selection.StartOrExtend(start, this.Position);
        }
    }
}
