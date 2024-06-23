using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf.CaretNavigators {
    internal class DocumentEndNavigator : ICaretNavigator {
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            var lastLine = document.Lines[document.Lines.Count - 1];
            return new TextPosition(lastLine.LineNumber, lastLine.Length);
        }
    }
}
