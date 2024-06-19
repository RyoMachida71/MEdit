using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf.TextRendering {
    public class GeneralTextParagraphProperties : TextParagraphProperties {

        private bool _firstLineInParagraph;
        private TextRunProperties _textRunProperties;
        private double _lineHeight;
        private TextMarkerProperties _textMarMarkerProperties;


        public GeneralTextParagraphProperties(bool firstLineInParagraph, TextRunProperties textRunProperties, double lineHeight, TextMarkerProperties textMarMarkerProperties) {
            _firstLineInParagraph = firstLineInParagraph;
            _textRunProperties = textRunProperties;
            _lineHeight = lineHeight;
            _textMarMarkerProperties = textMarMarkerProperties;
        }

        public override double DefaultIncrementalTab => 4;
        public override bool FirstLineInParagraph => _firstLineInParagraph;
        public override TextRunProperties DefaultTextRunProperties => _textRunProperties;
        public override FlowDirection FlowDirection => FlowDirection.LeftToRight;
        public override double Indent => 0;
        public override double LineHeight => _lineHeight;
        public override TextAlignment TextAlignment => TextAlignment.Left;
        public override TextMarkerProperties TextMarkerProperties => _textMarMarkerProperties;
        public override TextWrapping TextWrapping => TextWrapping.NoWrap;
    }
}
