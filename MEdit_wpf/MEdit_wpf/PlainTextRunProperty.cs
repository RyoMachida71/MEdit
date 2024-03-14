using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MEdit_wpf {
    public class PlainTextRunProperty : TextRunProperties {

        private Typeface _typeface;
        private double _fontRenderingEmSize;
        private double _fontHintingEmSize;
        private Brush _foregroundBrush;
        private Brush _backgroundBrush;
        private CultureInfo _cultureInfo;

        public PlainTextRunProperty(Typeface typeface
                                    ,double fontRenderingEmSize
                                    ,double fontHintingEmSize
                                    ,Brush foregroundBrush
                                    ,Brush backgroundBrush
                                    ,CultureInfo cultureInfo) {
            _typeface = typeface;
            _fontRenderingEmSize = fontRenderingEmSize;
            _fontHintingEmSize = fontHintingEmSize;
            _foregroundBrush = foregroundBrush;
            _backgroundBrush = backgroundBrush;
            _cultureInfo = cultureInfo;
        }

        public override Typeface Typeface => _typeface;
        public override double FontRenderingEmSize => _fontRenderingEmSize;
        public override double FontHintingEmSize => _fontHintingEmSize;
        public override TextDecorationCollection TextDecorations { get { return null; } }
        public override Brush ForegroundBrush => _foregroundBrush;
        public override Brush BackgroundBrush => _backgroundBrush;
        public override CultureInfo CultureInfo => _cultureInfo;
        public override TextEffectCollection TextEffects { get { return null; } }
    }
}
