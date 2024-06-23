
namespace MEdit_wpf.CaretNavigators {
    public enum CaretMovementType {
        None,
        CharLeft,
        CharLeftExtendingSelection,
        CharRight,
        CharRightExtendingSelection,
        WordLeft,
        WordRight,
        LineUp,
        LineUpExtendingSelection,
        LineDown,
        LineDownExtendingSelection,
        PageUp,
        PageDown,
        LineStart,
        LineStartExtendingSelection,
        LineEnd,
        LineEndExtendingSelection,
        DocumentStart,
        DocumentStartSelection,
        DocumentEnd,
        DocumentEndSelection,
    }
}
