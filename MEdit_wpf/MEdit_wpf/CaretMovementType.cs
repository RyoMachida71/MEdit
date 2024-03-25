using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEdit_wpf {
    public enum CaretMovementType {
        None,
        CharLeft,
        CharRight,
        Backspace,
        WordLeft,
        WordRight,
        LineUp,
        LineDown,
        PageUp,
        PageDown,
        LineStart,
        LineEnd,
        DocumentStart,
        DocumentEnd
    }
}
