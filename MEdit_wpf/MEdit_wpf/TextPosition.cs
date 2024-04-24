using System;
using System.Data.Common;
using System.Windows.Shapes;

namespace MEdit_wpf {
    public struct TextPosition : IComparable<TextPosition>, IEquatable<TextPosition> {
        public TextPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public override bool Equals(object obj) {
            if (!(obj is TextPosition)) return false;
            return (TextPosition)obj == this;
        }

        public bool Equals(TextPosition other) => this == other;

        public static bool operator ==(TextPosition left, TextPosition right) {
            return left.Row == right.Row && left.Column == right.Column;
        }

        public static bool operator !=(TextPosition left, TextPosition right) {
            return left.Row != right.Row || left.Column != right.Column;
        }

        public static bool operator <(TextPosition left, TextPosition right) {
            if (left.Row < right.Row)
                return true;
            else if (left.Row == right.Row)
                return left.Column < right.Column;
            else
                return false;
        }

        public static bool operator >(TextPosition left, TextPosition right) {
            if (left.Row > right.Row)
                return true;
            else if (left.Row == right.Row)
                return left.Column > right.Column;
            else
                return false;
        }

        public static bool operator <=(TextPosition left, TextPosition right) => !(left > right);

        public static bool operator >=(TextPosition left, TextPosition right) => !(left < right);

        public int CompareTo(TextPosition other) {
            if (this == other) return 0;
            if (this < other) return -1;
            else return 1;
        }

        public override int GetHashCode() => unchecked(191 * Row.GetHashCode() ^ Column.GetHashCode());
    }
}
