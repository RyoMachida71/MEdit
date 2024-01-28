using System;
using System.Windows.Forms;

namespace MEdit {
    public class TextView : RichTextBox, ITextView {

        public TextDocument Document { get; private set; }

        public TextView() {
            Document = new TextDocument();
            this.InitializeView();
        }

        private void InitializeView() {
            this.BorderStyle = BorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.LanguageOption = RichTextBoxLanguageOptions.DualFont;
            this.Multiline = true;
        }
    }
}
