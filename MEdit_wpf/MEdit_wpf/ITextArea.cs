using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf
{
    public interface ITextArea
    {
        ITextDocument Document { get; }
        ICaret Caret { get; }
        VisualText VisualText { get; }
    }
}
