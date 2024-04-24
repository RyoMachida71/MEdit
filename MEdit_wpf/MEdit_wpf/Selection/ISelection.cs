
namespace MEdit_wpf.Selection {
    public interface ISelection {
        TextPosition StartPosition { get; set; }
        TextPosition EndPosition { get; set; }
        string SelectedText { get; }
        bool HasSelection { get; }
    }
}
