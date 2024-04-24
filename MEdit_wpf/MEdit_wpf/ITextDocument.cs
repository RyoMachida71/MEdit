using System.Collections.Immutable;

namespace MEdit_wpf {
    public interface ITextDocument {
        string Text { get; set; }
        ImmutableList<DocumentLine> Lines { get; }

        void Insert(int insertPosRow, int insertPosCol, string text);

        int GetOffset(int row, int col);

        string GetText(int start, int end);

    }
}
