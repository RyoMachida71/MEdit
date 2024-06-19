using MEdit_wpf.Document;

namespace MEdit_wpf
{
    public interface ITextArea
    {
        ITextDocument Document { get; }
        Caret Caret { get; }
    }
}
