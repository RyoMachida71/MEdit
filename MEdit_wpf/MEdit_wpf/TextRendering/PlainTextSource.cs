using System;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf.TextRendering {
    public class PlainTextSource :TextSource {

        private readonly string _text;
        private readonly TextRunProperties _properties;

        public PlainTextSource(string text, TextRunProperties properties) {
            _text = text;
            _properties = properties;
        }

        public override TextRun GetTextRun(int textSourceCharacterIndex) {
            if (textSourceCharacterIndex < _text.Length)
                return new TextCharacters(_text, textSourceCharacterIndex, _text.Length - textSourceCharacterIndex, _properties);
            else
                return new TextEndOfParagraph(1);
        }

        public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit) {
            throw new NotImplementedException();
        }

        public override int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex) {
            throw new NotImplementedException();
        }
    }
}
