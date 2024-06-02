using System;

namespace MEdit_wpf {
    public class DocumentChangedEventArgs : EventArgs {

        public DocumentChangedEventArgs(TextPosition newPosition)
        {
            NewPosition = newPosition;
        }
        public TextPosition NewPosition { get; private set; }
    }
}
