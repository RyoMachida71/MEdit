using System;

namespace MEdit_wpf.Document {
    public class DocumentChangedEventArgs : EventArgs {

        public DocumentChangedEventArgs(int oldOffset,
                                        int newOffset,
                                        string textBeforeChange,
                                        string textAfterChange,
                                        TextPosition newPosition)
        {
            this.OldOffset = oldOffset;
            this.NewOffset = newOffset;
            this.TextBeforeChange = textBeforeChange;
            this.TextAfterChange = textAfterChange;
            this.NewPosition = newPosition;
        }

        public int OldOffset { get; private set; }

        public int NewOffset { get; private set; }

        public string TextBeforeChange { get; private set; }

        public string TextAfterChange { get; private set; }

        public TextPosition NewPosition { get; private set; }
    }
}
