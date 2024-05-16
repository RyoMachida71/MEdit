﻿using System.Collections.Immutable;

namespace MEdit_wpf {
    public interface ITextDocument {
        string Text { get; set; }
        ImmutableList<DocumentLine> Lines { get; }
        void Insert(TextPosition position, TextInput input);
        string GetText(TextPosition startPosition, TextPosition endPosition);
    }
}
