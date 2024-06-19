
namespace MEdit_wpf.Document {
    public class DocumentLine {
        public DocumentLine(int lineNumber, int offset, string text)
        {
            LineNumber = lineNumber;
            Offset = offset;
            Text = text;
        }

        public int LineNumber { get; private set; }

        public int Offset { get; private set; }

        public string Text { get; private set; }

        public int Length => Text.Length;
    }
}
