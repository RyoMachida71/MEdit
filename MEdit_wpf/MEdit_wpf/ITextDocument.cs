using System;
using System.Collections.Immutable;

namespace MEdit_wpf {
    public interface ITextDocument {
        string Text { get; set; }
        ImmutableList<DocumentLine> Lines { get; }
        event EventHandler<DocumentChangedEventArgs> DocumentChanged;
        void Replace(TextPosition start, TextPosition end, TextInput input);
        void Delete(TextPosition start, TextPosition end, EditingDirection direction);
        string GetText(TextPosition startPosition, TextPosition endPosition);
    }
}
