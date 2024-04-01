using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace MEdit_wpf {
    public class TextDocument {

        private List<DocumentLine> _lines = new List<DocumentLine>();

        private StringBuilder _buffer;
        public TextDocument(string text = "") {
            _buffer = new StringBuilder(text);
        }

        public string Text {
            get { return _buffer.ToString(); }
            set { _buffer = new StringBuilder(value); }
        }

        public ImmutableList<DocumentLine> Lines {
            get {
                _lines.Clear();
                if (string.IsNullOrEmpty(this.Text)) return _lines.ToImmutableList();

                var reader = new StringReader(this.Text);
                int lineNumber = 0;
                int offset = 0;
                while (reader.Peek() > -1) {
                    var line = reader.ReadLine() + "\r\n";
                    _lines.Add(new DocumentLine(lineNumber, offset, line));
                    ++lineNumber;
                    offset += line.Length;
                }
                return _lines.ToImmutableList();
            }
        }

        public int GetOffsetByLine(int row, int col) {
            var line = Lines.Find(x => x.LineNumber == row);
            if (line == null) return _buffer.Length;

            return line.Offset + col;
        }

        public void Insert(int insertPos, string text) {
            if (insertPos > _buffer.Length) insertPos = _buffer.Length;
            if (insertPos < 0) insertPos = 0;
            _buffer.Insert(insertPos, text);
        }

    }
}
