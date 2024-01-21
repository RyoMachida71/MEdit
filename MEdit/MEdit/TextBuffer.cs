using System.Text;

namespace MEdit {
    public class TextBuffer {
        private StringBuilder _buffer = new StringBuilder();

        public string Text => _buffer.ToString();

        public void Insert(int insertionIndex, string value) {
            _buffer.Insert(insertionIndex, value);
        }

        public void Replace(int start, int end, string oldValue, string newValue) {
            _buffer.Replace(oldValue, newValue, start, end - start);
        }

        public void DeleteBackward(int start) {
            DeleteRange(start - 1, 1);
        }

        public void DeleteRange(int start, int length) {
            _buffer.Remove(start, length);
        }
    }
}
