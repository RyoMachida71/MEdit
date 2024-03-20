using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEdit_wpf {
    public class TextDocument {

        private StringBuilder _buffer;

        public string Text {
            get { return _buffer.ToString(); }
            set { _buffer = new StringBuilder(value); }
        }

        public TextDocument(string text = "") {
            _buffer = new StringBuilder(text);
        }

        public void Insert(int insertPos, string text) {
            if (insertPos > _buffer.Length) insertPos = _buffer.Length;
            if (insertPos < 0) insertPos = 0;
            _buffer.Insert(insertPos, text);
        }
    }
}
