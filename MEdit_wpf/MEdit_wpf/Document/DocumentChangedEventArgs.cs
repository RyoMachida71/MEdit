using System;

namespace MEdit_wpf.Document {
    public class DocumentChangedEventArgs : EventArgs {

        public DocumentChangedEventArgs(int oldOffset,
                                        int newOffset,
                                        string deletetedText,
                                        string insertedText,
                                        TextPosition newPosition)
        {
            this.OldOffset = oldOffset;
            this.NewOffset = newOffset;
            this.DeletedText = deletetedText;
            this.InsertedText = insertedText;
            this.NewPosition = newPosition;
        }

        public int OldOffset { get; private set; }

        public int NewOffset { get; private set; }

        public string DeletedText { get; private set; }

        public string InsertedText { get; private set; }

        public TextPosition NewPosition { get; private set; }
    }
}
