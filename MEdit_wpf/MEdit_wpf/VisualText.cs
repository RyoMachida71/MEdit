using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class VisualText {
        private const int FontSize = 12;

        private List<TextLine> _visualLines = new List<TextLine>();

        private double _lineYPos;

        private TextRunProperties _runProp;

        private TextParagraphProperties _paragrapghProp;

        public void DrawVisualLines(DrawingContext dc, IImmutableList<string> textLines) {
            _visualLines.Clear();
            _lineYPos = 0;
            foreach (var line in textLines) {
                var formatter = TextFormatter.Create();
                var textRunProperty = new PlainTextRunProperty(new Typeface("Consolas"), FontSize, FontSize, Brushes.Black, Brushes.White, CultureInfo.InvariantCulture);
                var textRun = new PlainTextSource(line, textRunProperty);
                var visualLine = formatter.FormatLine(textRun
                                                , 0
                                                , GetStringWidth(line, new Typeface("Consolas"))
                                                , new GeneralTextParagraphProperties(false, textRunProperty, textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                                , null);
                visualLine.Draw(dc, new Point(0, _lineYPos), InvertAxes.None);
                _lineYPos += visualLine.Height;

                _visualLines.Add(visualLine);
            }
        }
        // todo: キャレットの描画位置に必要な情報を返す
        public TextLine GetLocationByRow(int row) {
            var count = _visualLines.Count;
            if (count == 0 || count - 1 < row) return null;
            return _visualLines[row];
        }
        private double GetStringWidth(string text, Typeface typeface) {
            // todo: measure string width
            //var target = new FormattedText();
            return 400;
        }
    }
}
