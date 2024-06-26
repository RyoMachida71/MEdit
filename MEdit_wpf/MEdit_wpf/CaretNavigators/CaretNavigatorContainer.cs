using MEdit_wpf.Caret;
using System.Collections.Generic;

namespace MEdit_wpf.CaretNavigators {
    public static class CaretNavigatorContainer {
        private static Dictionary<CaretMovementType, ICaretNavigator> _cache = new Dictionary<CaretMovementType, ICaretNavigator> ();

        public static ICaretNavigator GetNavigator(CaretMovementType type) {
            if (_cache.TryGetValue(type, out var cachedNavigator)) return cachedNavigator;

            ICaretNavigator navigator = null;
            switch (type) {
                case CaretMovementType.CharLeft:
                    navigator = new CharLeftNavigator();
                    break;
                case CaretMovementType.CharRight:
                    navigator = new CharRightNavigator();
                    break;
                case CaretMovementType.WordLeft:
                    navigator = new WordLeftNavigator();
                    break;
                case CaretMovementType.WordRight:
                    
                    break;
                case CaretMovementType.LineUp:
                    navigator = new LineUpNavigator();
                    break;
                case CaretMovementType.LineDown:
                    navigator = new LineDownNavigator();
                    break;
                case CaretMovementType.PageUp:
                    break;
                case CaretMovementType.PageDown:
                    break;
                case CaretMovementType.LineStart:
                    navigator = new LineStartNavigator();
                    break;
                case CaretMovementType.LineEnd:
                    navigator = new LineEndNavigator();
                    break;
                case CaretMovementType.DocumentStart:
                    navigator = new DocumentStartNavigator();
                    break;
                case CaretMovementType.DocumentEnd:
                    navigator = new DocumentEndNavigator();
                    break;
                default:
                    navigator = new NullNavigator();
                    break;
            }
            _cache.Add(type, navigator);
            return navigator;
        }
    }
}
