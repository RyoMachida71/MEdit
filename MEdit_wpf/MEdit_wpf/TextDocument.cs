using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
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
                if (string.IsNullOrEmpty(this.Text)) return lines.ToImmutableList();

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

        public void Delete(TextPosition start, TextPosition end) {
            int startOffset;
            int endOffset;

            if (start < end) {
                startOffset = this.GetOffset(start);
                endOffset = this.GetOffset(end);
            } else {
                startOffset = this.GetOffset(end);
                endOffset = this.GetOffset(start);
            }
            int deleteLength;
            if (startOffset == endOffset) {
                deleteLength = IsEndOfLine(startOffset) ? 2 : 1;
            } else {
                deleteLength = Math.Abs(startOffset - endOffset);
            }
            
            _buffer.Remove(startOffset, deleteLength);
        }

        private bool IsEndOfLine(int offset) {
            if (_buffer[offset] != '\r') return false;
            if (_buffer[offset + 1] != '\n') return false;
            return true;
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
