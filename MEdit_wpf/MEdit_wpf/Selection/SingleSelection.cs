
namespace MEdit_wpf.Selection {
    public class SingleSelection : ISelection {
        private ITextDocument _document;
        public SingleSelection(TextPosition start, TextPosition end, ITextDocument document)
        {
            StartPosition = start;
            EndPosition = end;
            _document = document;
        }

        public TextPosition StartPosition { get; set; }
        public TextPosition EndPosition { get; set; }
        public string SelectedText {
            get {
                var startOffset = _document.GetOffset(StartPosition.Row, StartPosition.Column);
                var endOffset = _document.GetOffset(EndPosition.Row, EndPosition.Column);
                return _document.GetText(startOffset, endOffset);
            }
        }
        public bool HasSelection => StartPosition != EndPosition;
    }
}
