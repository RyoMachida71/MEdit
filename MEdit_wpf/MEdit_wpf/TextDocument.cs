using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text;

namespace MEdit_wpf {
    public class TextDocument : ITextDocument {

        public static readonly string EndOfLine = "\r\n";

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
                    var line = reader.ReadLine() + EndOfLine;
                    _lines.Add(new DocumentLine(lineNumber, offset, line));
                    ++lineNumber;
                    offset += line.Length;
                }
                return _lines.ToImmutableList();
            }
        }


        public void Insert(TextPosition position, TextInput input) {
            var offset = this.GetOffset(position);
            if (offset > _buffer.Length) offset = _buffer.Length;
            if (offset < 0) offset = 0;

            _buffer.Insert(offset, input.Value);
        }

        private int GetOffset(TextPosition position) {
            var line = Lines.Find(x => x.LineNumber == position.Row);
            if (line == null) return _buffer.Length;

            return line.Offset + position.Column;
        }

        public string GetText(TextPosition startPos, TextPosition endPos) {
            if (startPos == endPos) return "";
            var startOffset = GetOffset(startPos);
            var endOffset = GetOffset(endPos);
            var length = Math.Abs(startOffset - endOffset);
            return startPos < endPos ? this.Text.Substring(startOffset, length) : this.Text.Substring(endOffset, length);
        }
    }
}
