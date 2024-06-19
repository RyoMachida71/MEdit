using MEdit_wpf.Caret;
using MEdit_wpf.Document;

namespace MEdit_wpf
{
    public interface ITextArea
    {
        ITextDocument Document { get; }
        SingleCaret Caret { get; }
    }
}
