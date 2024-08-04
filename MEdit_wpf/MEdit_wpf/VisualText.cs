using MEdit_wpf.Document;
using MEdit_wpf.Selection;
using MEdit_wpf.TextRendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class VisualText {
        private const int FontSize = 12;

        private const double MaxParagraphWidth = 32000;

        private List<TextLine> _visualLines = new List<TextLine>();

        private double _lineYPos = 0;

        private TextRunProperties _textRunProperty = new PlainTextRunProperty(new Typeface("Consolas"), FontSize, FontSize, Brushes.Black, Brushes.Transparent, CultureInfo.InvariantCulture);

        public VisualText()
        {
            this.LineHeight = CalcLineHeight();
            this.CharWidth = _textRunProperty.FontHintingEmSize;
        }

        public IImmutableList<TextLine> VisualTextLines => _visualLines.ToImmutableList();

        public double LineHeight { get; private set; }

        public double CharWidth { get; private set; }

        public double TotalLineHeight => _visualLines.Sum(x => x.TextHeight);

        public double MaxLineWidth => _visualLines.Max(x => x.WidthIncludingTrailingWhitespace);

        private double CalcLineHeight()
        {
            var formatter = TextFormatter.Create();
            var textRun = new PlainTextSource("", _textRunProperty);
            var visualLine = formatter.FormatLine(textRun
                                            , 0
                                            , MaxParagraphWidth
                                            , new GeneralTextParagraphProperties(false, _textRunProperty, _textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                            , null);
            _visualLines.Add(visualLine);
            return visualLine.TextHeight;
        }

        public void BuildVisualLines(IImmutableList<DocumentLine> documentLines) {
            _visualLines.Clear();
            _lineYPos = 0;
            foreach (var line in documentLines) {
                var formatter = TextFormatter.Create();
                var textRun = new PlainTextSource(line.Text, _textRunProperty);
                var visualLine = formatter.FormatLine(textRun
                                                , 0
                                                , MaxParagraphWidth
                                                , new GeneralTextParagraphProperties(false, _textRunProperty, _textRunProperty.FontHintingEmSize, new GeneralTextMarkerProperties(0, textRun))
                                                , null);
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
            if (selection.StartPosition == selection.EndPosition) return new List<Rect>().ToImmutableList();

            if (selection.StartPosition < selection.EndPosition)
            {
                return MakeSelectionRectsForward(selection, document);
            }
            else
            {
                return MakeSelectionRectsBackward(selection, document);
            }
        }

        private IImmutableList<Rect> MakeSelectionRectsForward(ISelection selection, ITextDocument document)
        {
            var start = selection.StartPosition;
            var end = selection.EndPosition;
            var rects = new List<Rect>();

            for (var row = start.Row; row <= end.Row; ++row)
            {
                var line = document.Lines[row];
                var startCol = row == start.Row ? start.Column : 0;
                var endCol = row == end.Row ? end.Column : line.Length;
                var position = GetCaretScreenPosition(row == start.Row ? selection.StartPosition : new TextPosition(row, 0));
                for (int col = startCol; col <= endCol; ++col)
                {
                    var nextPosition = GetCaretScreenPosition(new TextPosition(row, col));
                    var xPos = Math.Min(position.X, nextPosition.X);
                    var width = Math.Abs(position.X - nextPosition.X);
                    rects.Add(new Rect(xPos, position.Y, width, this.LineHeight));
                    position = nextPosition;
                }
            }
            return rects.ToImmutableList();
        }

        private IImmutableList<Rect> MakeSelectionRectsBackward(ISelection selection, ITextDocument document)
        {
            var start = selection.StartPosition;
            var end = selection.EndPosition;
            var rects = new List<Rect>();

            for (var row = start.Row; row >= end.Row; --row)
            {
                var line = document.Lines[row];
                var startCol = row == start.Row ? start.Column : line.Length;
                var endCol = row == end.Row ? end.Column : 0;
                var position = GetCaretScreenPosition(row == start.Row ? selection.StartPosition : new TextPosition(row, line.Length));
                for (int col = startCol; col >= endCol; --col)
                {
                    var nextPosition = GetCaretScreenPosition(new TextPosition(row, col));
                    var xPos = Math.Min(position.X, nextPosition.X);
                    var width = Math.Abs(position.X - nextPosition.X);
                    rects.Add(new Rect(xPos, position.Y, width, this.LineHeight));
                    position = nextPosition;
                }
            }
            return rects.ToImmutableList();
        }
    }
}
