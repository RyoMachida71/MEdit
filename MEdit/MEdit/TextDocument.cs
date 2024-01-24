namespace MEdit {
    public class TextDocument {
        private TextBuffer _buffer;

        public string Text => _buffer.Text;

        public TextDocument() {
            _buffer = new TextBuffer();
        }

        public void Insert(int offset, string value) {
            _buffer.Insert(offset, value);
        }

        public void Replace(int start, int end, string oldValue, string newValue) {
            _buffer.Replace(start, end, oldValue, newValue);
        }

        public void Delete(int start, int end) {
            _buffer.DeleteRange(start, end);
        }
    }
}
