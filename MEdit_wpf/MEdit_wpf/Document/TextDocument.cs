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

            var removedText = this.Text.Substring(startOffset, Math.Abs(startOffset - endOffset));
            _buffer.Remove(startOffset, Math.Abs(startOffset - endOffset));
            _buffer.Insert(startOffset, input.Value);

            _shouldConstructLines = true;
            var newOffset = startOffset + input.Length;
            OnDocumentChanged(new DocumentChangedEventArgs(startOffset,
                                                           newOffset,
                                                           removedText,
                                                           input.Value,
                                                           GetNewTextPosition(newOffset)));
        }

        internal void Replace(int start, int length, string text) {
            if (start < 0 || length < 0) return;

            _buffer.Remove(start, length);
            _buffer.Insert(start, text);

            var newOffset = start + text.Length;
            OnDocumentChanged(new DocumentChangedEventArgs(start,
                                                           newOffset,
                                                           string.Empty,
                                                           string.Empty,
                                                           GetNewTextPosition(newOffset)));
        }

        public void Delete(TextPosition start, TextPosition end, EditingDirection direction = EditingDirection.Forward) {
            if (_buffer.Length == 0) return;

            (int startOffset, int endOffset) = GetOffsetRange(start, end);

            (int deleteionStart, int length) = CalcDeletionRange(startOffset, endOffset, direction);
            var deletedText = this.Text.Substring(deleteionStart, length);
            _buffer.Remove(deleteionStart, length);

            _shouldConstructLines = true;
            OnDocumentChanged(new DocumentChangedEventArgs(startOffset,
                                                           deleteionStart,
                                                           deletedText,
                                                           string.Empty,
                                                           GetNewTextPosition(deleteionStart)));
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

        private (int, int) CalcDeletionRange(int startOffset, int endOffset, EditingDirection direction) {
            if (startOffset != endOffset) return (startOffset, Math.Abs(startOffset - endOffset));

            var deletionStart = startOffset;
            var length = 0;
            if (direction == EditingDirection.Forward && deletionStart != _buffer.Length) {
                length = IsAtEndofLine(deletionStart) ? EndOfLine.Length : 1;
            }
            if (direction == EditingDirection.Backward && deletionStart != 0) {
                var isAtStartOfLine = this.Lines.Any(line => line.Offset == deletionStart);
                deletionStart = isAtStartOfLine ? deletionStart - EndOfLine.Length : deletionStart - 1;
                length = isAtStartOfLine ? EndOfLine.Length : 1;
            }
            return (deletionStart, length);
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
            var line = this.Lines.LastOrDefault(x => x.Offset <= newOffset);
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
