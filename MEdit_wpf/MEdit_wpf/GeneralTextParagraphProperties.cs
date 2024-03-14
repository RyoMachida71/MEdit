using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class GeneralTextParagraphProperties : TextParagraphProperties {

        public override double DefaultIncrementalTab => 4;
        public override bool FirstLineInParagraph => throw new NotImplementedException();
        public override TextRunProperties DefaultTextRunProperties => throw new NotImplementedException();
        public override FlowDirection FlowDirection => throw new NotImplementedException();
        public override double Indent => 0;
        public override double LineHeight => throw new NotImplementedException();
        public override TextAlignment TextAlignment => TextAlignment.Left;
        public override TextMarkerProperties TextMarkerProperties => throw new NotImplementedException();
        public override TextWrapping TextWrapping => throw new NotImplementedException();
    }
}
