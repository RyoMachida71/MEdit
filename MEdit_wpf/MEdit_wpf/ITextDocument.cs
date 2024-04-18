using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEdit_wpf {
    interface ITextDocument {
        string Text { get; set; }
        ImmutableList<DocumentLine> Lines { get; }

        void Insert(int insertPos, string text);

        int GetOffset(int row, int col);

        string GetText(int start, int end);

    }
}
