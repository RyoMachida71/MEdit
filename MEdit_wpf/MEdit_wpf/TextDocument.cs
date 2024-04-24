using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text;

namespace MEdit_wpf {
    public class TextDocument : ITextDocument {

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


        public void Insert(int insertPosRow, int insertPosCol, string text) {
            var offset = this.GetOffset(insertPosRow, insertPosCol);
            if (offset > _buffer.Length) offset = _buffer.Length;
            if (offset < 0) offset = 0;
            _buffer.Insert(offset, text);
        }

        public int GetOffset(int row, int col) {
            var line = Lines.Find(x => x.LineNumber == row);
            if (line == null) return _buffer.Length;

            return line.Offset + col;
        }

        public string GetText(int startPos, int endPos) {
            if (startPos == endPos) return "";
            var length = Math.Abs(startPos - endPos);
            if (startPos < endPos) return this.Text.Substring(startPos, length);
            else return this.Text.Substring(endPos, length);
        }
    }
}
