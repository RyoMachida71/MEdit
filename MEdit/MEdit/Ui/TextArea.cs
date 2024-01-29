using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEdit.Ui {
    public partial class TextArea : UserControl {
        private const int Drawing_Offset = 2;

        private TextView _textView;
        private int _prevLineCount;
        private readonly Point _bottomLeft;
        private readonly Brush _fontBrush;
        private StringFormat _lineFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        public TextArea() {
            InitializeComponent();
            this.splitContainer.Panel1.Paint += TextArea_Paint;
            _textView = new TextView();
            _textView.TextChanged += (s, e) => UpdateLineNumber();
            _textView.VScroll += (s, e) => UpdateLineNumber();
            _textView.FontChanged += (s, e) => UpdateLineNumber();
            this.splitContainer.Panel2.Controls.Add(_textView);

            _bottomLeft = new Point(0, this.ClientRectangle.Height);
            _fontBrush = new SolidBrush(_textView.ForeColor);
        }

        private void UpdateLineNumber() {
            // todo: get lines count from TextDocument
            if (_textView.Lines.Length != _prevLineCount) {
                _prevLineCount = _textView.Lines.Length;
                this.splitContainer.Panel1.Invalidate();
            }
        }

        private void TextArea_Paint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(_textView.BackColor);

            var visibleFirstline = GetVisibleLine(Point.Empty);
            var visibleLastLine = GetVisibleLine(_bottomLeft);
            for (int i = visibleFirstline; i <= visibleLastLine + 1; ++i) {
                var yPos = GetYPositionOfLine(i);
                if (yPos == -1) continue;
                e.Graphics.DrawString((i + 1).ToString(), this.Font, _fontBrush, new RectangleF(0, yPos + Drawing_Offset, splitContainer.Panel1.Width, this.Font.Height), _lineFormat);
            }
        }

        private int GetVisibleLine(Point pos) {
            var index = _textView.GetCharIndexFromPosition(pos);
            return _textView.GetLineFromCharIndex(index);
        }

        private int GetYPositionOfLine(int line) {
            var index = _textView.GetFirstCharIndexFromLine(line);
            if (index == -1) return index;
            return _textView.GetPositionFromCharIndex(index).Y;
        }
    }
}
