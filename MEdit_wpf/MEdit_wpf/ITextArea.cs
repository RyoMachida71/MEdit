using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System;

namespace MEdit_wpf
{
    public interface ITextArea
    {
        ITextDocument Document { get; }
        ICaret Caret { get; }
        VisualText VisualText { get; }
        IVisualTextInfo VisualTextInfo { get; }
        double ActualHeight { get; }
        event EventHandler ScrollOffsetChanged;
    }
}
