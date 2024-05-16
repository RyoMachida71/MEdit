﻿using MEdit_wpf.Layer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MEdit_wpf {
    /// <summary>
    /// TextArea.xaml の相互作用ロジック
    /// </summary>
    public partial class TextArea : Control, ITextArea {

        private TextDocument _document;
        private Caret _caret;
        private CaretLayer _caretLayer;
        private VisualText _visualText;

        public TextArea() {
            InitializeComponent();
            _document = new TextDocument();
            _visualText = new VisualText();
            _caret = new Caret(this, RenderCaret);
            _caretLayer = new CaretLayer();

            AddVisualChild(_caretLayer);
            CaretCommandBinder.SetBinding(_caret, this.CommandBindings, this.InputBindings);
        }

        public ITextDocument Document => _document; 

        protected override int VisualChildrenCount => 1;

        protected override void OnTextInput(TextCompositionEventArgs e) {
            base.OnTextInput(e);
            var input = new TextInput(e.Text);
            _document.Insert(_caret.Position, input);
            _caret.UpdatePos(input);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            _visualText.DrawVisualLines(dc, _document.Lines);
            RenderCaret();
        }

        protected override Visual GetVisualChild(int index) {
            switch (index) {
                case (int)LayerKind.Caret:
                    return _caretLayer;
                default:
                    return base.GetVisualChild(index);
            }
        }

        private void TextArea_GotFocus(object sender, RoutedEventArgs args) {
            if (this.IsFocused) {
                Keyboard.Focus(this);
            }
        }

        private void RenderCaret() {
            _caretLayer.Render(_visualText.GetCaretScreenPosition(_caret.Position), _visualText.GetSelectionScreenRects(_caret.Selection, _document));
        }
    }
}
