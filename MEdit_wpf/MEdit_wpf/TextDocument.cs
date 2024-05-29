using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace MEdit_wpf {
    public class TextDocument : ITextDocument {

        public static readonly string EndOfLine = "\r\n";

        private readonly string[] _splitDelimiter = new[] { EndOfLine };

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
                var lines = new List<DocumentLine>();
                var splited = this.Text.Split(_splitDelimiter, StringSplitOptions.None);
                int offset = 0;
                for (int lineNumber = 0; lineNumber < splited.Length; ++lineNumber)
                {
                    lines.Add(new DocumentLine(lineNumber, offset, splited[lineNumber]));
                    offset += splited[lineNumber].Length + EndOfLine.Length;
                }
                return lines.ToImmutableList();
            }
        }


        public void Insert(TextPosition position, TextInput input) {
            var offset = this.GetOffset(position);
            if (offset > _buffer.Length) offset = _buffer.Length;
            if (offset < 0) offset = 0;

            _buffer.Insert(offset, input.Value);
        }

        public void Replace(TextPosition start, TextPosition end, TextInput input) {
            (int startOffset, int endOffset) = GetOffsetRange(start, end);

            if (startOffset == endOffset) {
                _buffer.Insert(startOffset, input.Value);
            } else {
                _buffer.Remove(startOffset, Math.Abs(startOffset - endOffset));
                _buffer.Insert(startOffset, input.Value);
            }
        }

        public void Delete(TextPosition start, TextPosition end) {
            (int startOffset, int endOffset) = GetOffsetRange(start, end);

            var deleteLength = startOffset == endOffset ? IsEndOfLine(startOffset) ? 2 : 1 : Math.Abs(startOffset - endOffset);
            _buffer.Remove(startOffset, deleteLength);
        }

        private bool IsEndOfLine(int offset) {
            if (_buffer[offset] != '\r') return false;
            if (_buffer[offset + 1] != '\n') return false;
            return true;
        }

        private Tuple<int, int> GetOffsetRange(TextPosition start, TextPosition end) {
            int startOffset;
            int endOffset;

            if (start < end) {
                startOffset = this.GetOffset(start);
                endOffset = this.GetOffset(end);
            } else {
                startOffset = this.GetOffset(end);
                endOffset = this.GetOffset(start);
            }
            return new Tuple<int, int>(startOffset, endOffset);
        }

        private int GetOffset(TextPosition position) {
            var line = Lines.Find(x => x.LineNumber == position.Row);
            if (line == null) throw new ArgumentException($"Couldn't find line by TextPosition({ position })");

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
