
namespace MEdit_wpf.Selection {
    public class SingleSelection : ISelection {
        private ITextDocument _document;
        public SingleSelection(ITextDocument document)
        {
            StartPosition = new TextPosition(0, 0);
            EndPosition = new TextPosition(0, 0);
            _document = document;
        }

        public TextPosition StartPosition { get; private set; }
        public TextPosition EndPosition { get; private set; }
        public string SelectedText {
            get {
                var startOffset = _document.GetOffset(StartPosition.Row, StartPosition.Column);
                var endOffset = _document.GetOffset(EndPosition.Row, EndPosition.Column);
                return _document.GetText(startOffset, endOffset);
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
