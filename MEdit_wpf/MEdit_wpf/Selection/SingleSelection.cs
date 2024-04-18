using MEdit_wpf;

namespace MEdit_wpf.Selection {
    public class SingleSelection : ISelection {
        private ITextDocument _document;
        public SingleSelection(int start, int end, TextDocument document)
        {
            StartPosition = start;
            EndPosition = end;
            _document = document;
        }

        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public string SelectedText => _document.GetText(StartPosition, EndPosition);
        public bool HasSelection => StartPosition != EndPosition;
    }
}
