using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text;

namespace MEdit_wpf {
    public class TextDocument {

        private List<string> _lines = new List<string>();

        private StringBuilder _buffer;
        public TextDocument(string text = "") {
            _buffer = new StringBuilder(text);
        }

        public string Text {
            get { return _buffer.ToString(); }
            set { _buffer = new StringBuilder(value); }
        }
        public ImmutableList<string> Lines {
            get {
                _lines.Clear();
                if (string.IsNullOrEmpty(this.Text)) return _lines.ToImmutableList();
                var reader = new StringReader(this.Text);
                while (reader.Peek() > -1) {
                    _lines.Add(reader.ReadLine() + "\r\n");
                }
                return _lines.ToImmutableList();
            }
        }

        public void Insert(int insertPos, string text) {
            if (insertPos > _buffer.Length) insertPos = _buffer.Length;
            if (insertPos < 0) insertPos = 0;
            _buffer.Insert(insertPos, text);
        }

    }
}
