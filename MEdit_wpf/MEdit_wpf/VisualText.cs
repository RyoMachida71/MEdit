using MEdit_wpf.Selection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class VisualText {
        private const int FontSize = 12;

        private const double MaxParagraphWidth = 32000;

        private List<TextLine> _visualLines = new List<TextLine>();

        private double _lineYPos = 0;

        private double _lineHeight = 0;

        public VisualText()
        {
            _lineHeight = CalcLineHeight();
        }

        private double CalcLineHeight()
        {
            var formatter = TextFormatter.Create();
            var textRunProperty = new PlainTextRunProperty(new Typeface("Consolas"), FontSize, FontSize, Brushes.Black, Brushes.White, CultureInfo.InvariantCulture);
            var textRun = new PlainTextSource("aaa", textRunProperty);
            var visualLine = formatter.FormatLine(textRun
                                            , 0
                                            , MaxParagraphWidth
                                            , new GeneralTextParagraphProperties(false, textRunProperty, textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                            , null);
            return visualLine.TextHeight;
        }

        public void DrawVisualLines(DrawingContext dc, IImmutableList<DocumentLine> textLines) {
            _visualLines.Clear();
            _lineYPos = 0;
            foreach (var line in textLines) {
                var formatter = TextFormatter.Create();
                var textRunProperty = new PlainTextRunProperty(new Typeface("Consolas"), FontSize, FontSize, Brushes.Black, Brushes.White, CultureInfo.InvariantCulture);
                var textRun = new PlainTextSource(line.Text, textRunProperty);
                var visualLine = formatter.FormatLine(textRun
                                                , 0
                                                , MaxParagraphWidth
                                                , new GeneralTextParagraphProperties(false, textRunProperty, textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                                , null);
                visualLine.Draw(dc, new Point(0, _lineYPos), InvertAxes.None);
                _lineYPos += visualLine.TextHeight;

                _visualLines.Add(visualLine);
            }
        }

        public Point GetCaretScreenPosition(TextPosition position) {
            if (_visualLines.Count == 0) return new Point(0, 0);
            if (_visualLines.Count - 1 < position.Row) return new Point(0, _lineYPos);

            var textLine = _visualLines[position.Row];
            var xPos = textLine.GetDistanceFromCharacterHit(new CharacterHit(position.Column, 0));
            var yPos = position.Row * textLine.TextHeight;
            return new Point(xPos, yPos);
        }

        public IImmutableList<Rect> GetSelectionScreenRects(ISelection selection, ITextDocument document)
        {
            var rects = new List<Rect>();
            if (selection.StartPosition == selection.EndPosition) return rects.ToImmutableList();

            TextPosition start = selection.StartPosition;
            TextPosition end = selection.EndPosition;
            if (selection.StartPosition > selection.EndPosition)
            {
                start = selection.EndPosition;
                end = selection.StartPosition;
            }

            var position = GetCaretScreenPosition(selection.StartPosition);
            for ( var row = start.Row; row <= end.Row; ++row)
            {
                var line = document.Lines[row];
                var endCol = row == end.Row ? end.Column : line.Text.Length;
                for (int col = start.Column; col <= endCol; ++col)
                {
                    var nextPosition = GetCaretScreenPosition(new TextPosition(row, col));
                    var xPos = Math.Min(position.X, nextPosition.X);
                    var width = Math.Abs(position.X - nextPosition.X);
                    rects.Add(new Rect(xPos, position.Y, width, _lineHeight));
                    position = nextPosition;
                }
            }
            return rects.ToImmutableList();
        }
    }
}
