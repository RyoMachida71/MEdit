using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace MEdit_wpf.Document {
    public class TextDocument : ITextDocument {

        public static readonly string EndOfLine = "\r\n";

        private readonly string[] _splitDelimiter = new[] { EndOfLine };

        private bool _shouldConstructLines = true;

        private List<DocumentLine> _lines = new List<DocumentLine>();

        private StringBuilder _buffer;

        public TextDocument(string text = "") {
            _buffer = new StringBuilder(text);
        }

        public event EventHandler<DocumentChangedEventArgs> DocumentChanged;

        public string Text {
            get { return _buffer.ToString(); }
            set { _buffer = new StringBuilder(value); }
        }

        public ImmutableList<DocumentLine> Lines {
            get {
                if (!_shouldConstructLines) return _lines.ToImmutableList();

                _shouldConstructLines = false;

                _lines.Clear();
                var splited = this.Text.Split(_splitDelimiter, StringSplitOptions.None);
                int offset = 0;
                for (int lineNumber = 0; lineNumber < splited.Length; ++lineNumber)
                {
                    _lines.Add(new DocumentLine(lineNumber, offset, splited[lineNumber]));
                    offset += splited[lineNumber].Length + EndOfLine.Length;
                }
                return _lines.ToImmutableList();
            }
        }

        public void Replace(TextPosition start, TextPosition end, TextInput input) {
            (int startOffset, int endOffset) = GetOffsetRange(start, end);

            if (startOffset == endOffset) {
                _buffer.Insert(startOffset, input.Value);
            } else {
                _buffer.Remove(startOffset, Math.Abs(startOffset - endOffset));
                _buffer.Insert(startOffset, input.Value);
            }

            _shouldConstructLines = true;
            OnDocumentChanged(new DocumentChangedEventArgs(GetNewTextPosition(startOffset + input.Length)));
        }

        public void Delete(TextPosition start, TextPosition end, EditingDirection direction = EditingDirection.Forward) {
            if (_buffer.Length == 0) return;

            (int startOffset, int endOffset) = GetOffsetRange(start, end);

            var updatedOffset = startOffset;
            if (startOffset == endOffset) {
                updatedOffset = DeleteChar(startOffset, direction);
            } else {
                var deleteLength = Math.Abs(startOffset - endOffset);
                _buffer.Remove(startOffset, deleteLength);
            }
            _shouldConstructLines = true;
            OnDocumentChanged(new DocumentChangedEventArgs(GetNewTextPosition(updatedOffset)));
        }

        private int DeleteChar(int offset, EditingDirection direction) {
            int offsetAfterDeletion;
            if (direction == EditingDirection.Forward) {
                if (offset == _buffer.Length) return offset;

                var deleteLength = IsAtEndofLine(offset) ? EndOfLine.Length : 1;
                _buffer.Remove(offset, deleteLength);
                offsetAfterDeletion = offset;
            } else {
                if (offset == 0) return offset;

                var isAtStartOfLine = this.Lines.Any(line => line.Offset == offset);
                var adjustedOffset = isAtStartOfLine ? offset - EndOfLine.Length : offset - 1;
                var deleteLength = isAtStartOfLine ? EndOfLine.Length : 1;
                _buffer.Remove(adjustedOffset, deleteLength);
                offsetAfterDeletion = adjustedOffset;
            }
            return offsetAfterDeletion;
        }

        private (int, int) GetOffsetRange(TextPosition start, TextPosition end) {
            int startOffset;
            int endOffset;

            if (start < end) {
                startOffset = this.GetOffset(start);
                endOffset = this.GetOffset(end);
            } else {
                startOffset = this.GetOffset(end);
                endOffset = this.GetOffset(start);
            }
            return (startOffset, endOffset);
        }

        private int GetOffset(TextPosition position) {
            var line = Lines.Find(x => x.LineNumber == position.Row);
            if (line == null) throw new ArgumentException($"Couldn't find line by TextPosition({ position })");

            return line.Offset + position.Column;
        }

        private bool IsAtEndofLine(int offset) {
            if (_buffer[offset] != '\r') return false;
            if (_buffer[offset + 1] != '\n') return false;
            return true;
        }

        private void OnDocumentChanged(DocumentChangedEventArgs e) {
            if (DocumentChanged != null) {
                DocumentChanged(this, e);
            }
        }

        private TextPosition GetNewTextPosition(int newOffset) {
            var line = this.Lines.OrderByDescending(x => x.LineNumber).FirstOrDefault(x => x.Offset <= newOffset);
            return line == null ? TextPosition.Empty : new TextPosition(line.LineNumber, newOffset - line.Offset);
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
