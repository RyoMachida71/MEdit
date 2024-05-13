
namespace MEdit_wpf.Selection {
    public class SingleSelection : ISelection {

        private ITextArea _textArea;

        public SingleSelection(ITextArea textArea)
        {
            StartPosition = new TextPosition(0, 0);
            EndPosition = new TextPosition(0, 0);
            _textArea = textArea;
        }

        public TextPosition StartPosition { get; private set; }
        public TextPosition EndPosition { get; private set; }
        public string SelectedText {
            get {
                var startOffset = _textArea.Document.GetOffset(StartPosition.Row, StartPosition.Column);
                var endOffset = _textArea.Document.GetOffset(EndPosition.Row, EndPosition.Column);
                return _textArea.Document.GetText(startOffset, endOffset);
            }
        }
        private bool HasSelection() => this.StartPosition != this.EndPosition;

        public void StartOrExtend(TextPosition start, TextPosition end)
        {
            if (!HasSelection())
            {
                this.StartPosition = start;
            }
            this.EndPosition = end;
        }

        public void Unselect() => this.StartPosition = this.EndPosition;
    }
}
