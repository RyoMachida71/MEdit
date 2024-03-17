using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class GeneralTextMarkerProperties : TextMarkerProperties {
        private double _offset;
        private TextSource _textSource;

        public GeneralTextMarkerProperties(double offset, TextSource textSource) {
            _offset = offset;
            _textSource = textSource;
        }

        public override double Offset => _offset;
        public override TextSource TextSource => _textSource;
    }
}
