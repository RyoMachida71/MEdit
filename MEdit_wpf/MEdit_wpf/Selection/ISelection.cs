
namespace MEdit_wpf.Selection {
    interface ISelection {
        int StartPosition { get; set; }
        int EndPosition { get; set; }
        string SelectedText { get; }
        bool HasSelection { get; }
    }
}
