﻿
namespace MEdit_wpf.Selection {
    public interface ISelection {
        TextPosition StartPosition { get; }
        TextPosition EndPosition { get; }
        void StartOrExtend(TextPosition start, TextPosition end);
        void Unselect();
    }
}
