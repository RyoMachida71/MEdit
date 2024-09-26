using History;
using System;

namespace MEdit_wpf.Document {
    internal class DocumentOperation : IUndoable {
        private readonly Action<int, int, string> _replaceText;
        private readonly DocumentChangedEventArgs _changed;

        public DocumentOperation(Action<int, int, string> replaceText, DocumentChangedEventArgs e)
        {
            _replaceText = replaceText;
            _changed = e;
        }

        public void Undo() {
            _replaceText(_changed.OldOffset, _changed.TextAfterChange.Length, _changed.TextBeforeChange);
        }

        public void Redo() {
            _replaceText(_changed.OldOffset, _changed.TextBeforeChange.Length, _changed.TextAfterChange);
        }
    }
}
