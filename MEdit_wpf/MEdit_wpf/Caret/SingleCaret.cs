using MEdit_wpf.Document;
using MEdit_wpf.Selection;
using System;

namespace MEdit_wpf.Caret {
    public class SingleCaret : ICaret {

        private ITextArea _textArea;

        private Action _showCaret;

        public SingleCaret(ITextArea textArea, Action showCaret) {
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

        public void Move(ICaretNavigator navigator, bool isSelectionMode) {
            if (string.IsNullOrEmpty(_textArea.Document.Text)) return;

            var nextPosition = navigator.GetNextPosition(this.Position, _textArea.Document);
            if (isSelectionMode) {
                this.Selection.StartOrExtend(this.Position, nextPosition);
                this.Position = nextPosition;
            } else {
                this.Position = nextPosition;
                this.Selection.Unselect(this.Position);
            }

            if (_showCaret != null) {
                _showCaret();
            }
        }
    }
}
