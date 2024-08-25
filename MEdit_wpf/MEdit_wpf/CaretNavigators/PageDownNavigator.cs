using MEdit_wpf.Caret;
using MEdit_wpf.Document;
using System;

namespace MEdit_wpf.CaretNavigators {
    internal class PageDownNavigator : ICaretNavigator {
        private ITextArea _textArea;
        public PageDownNavigator(ITextArea textArea) {
            _textArea = textArea;
        }
        public TextPosition GetNextPosition(TextPosition currentPosition, ITextDocument document) {
            if (currentPosition.Row == document.Lines.Count - 1) return currentPosition;

            var lineCountOnScreen = (int)(_textArea.ActualHeight / _textArea.VisualTextInfo.LineHeight);
            var desiredPosX = Math.Min(document.Lines.Count - 1, currentPosition.Row + lineCountOnScreen);
            var desiredPosY = Math.Min(currentPosition.Column, document.Lines[desiredPosX].Length);
            return new TextPosition(desiredPosX, desiredPosY);
        }
    }
}
