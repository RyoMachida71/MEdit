using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace MEdit_wpf {
    public static class CaretCommandBinder {

        private static Caret _caret;

        private static List<CommandBinding> _commands = new List<CommandBinding>();

        private static List<InputBinding> _inputs = new List<InputBinding>();

        public static void SetBinding(Caret caret, CommandBindingCollection commands, InputBindingCollection inputs) {
            _caret = caret;
            commands.AddRange(_commands);
            inputs.AddRange(_inputs);
        }

        private static void AddBinding(ICommand command, ModifierKeys modifier, Key key, ExecutedRoutedEventHandler handler) {
            _commands.Add(new CommandBinding(command, handler));
            _inputs.Add(NewFrozenKeyBinding(command, modifier, key));
        }

        private static KeyBinding NewFrozenKeyBinding(ICommand command, ModifierKeys modifier, Key key) {
            var keyBinding = new KeyBinding(command, key, modifier);
            var freezable = keyBinding as Freezable;
            if (freezable != null) {
                freezable.Freeze();
            }
            return keyBinding;
        }

        static CaretCommandBinder() {
            var none = ModifierKeys.None;
            var alt = ModifierKeys.Alt;
            var ctrl = ModifierKeys.Control;
            var shift = ModifierKeys.Shift;

            AddBinding(EditingCommands.MoveLeftByCharacter, none, Key.Left, OnMoveCaret(CaretMovementType.CharLeft));
            AddBinding(EditingCommands.SelectLeftByCharacter, shift, Key.Left, OnMoveCaretExtendingSelection(CaretMovementType.CharLeftExtendingSelection));
            AddBinding(EditingCommands.MoveRightByCharacter, none, Key.Right, OnMoveCaret(CaretMovementType.CharRight));
            AddBinding(EditingCommands.MoveUpByLine, none, Key.Up, OnMoveCaret(CaretMovementType.LineUp));
            AddBinding(EditingCommands.MoveDownByLine, none, Key.Down, OnMoveCaret(CaretMovementType.LineDown));
        }

        private static ExecutedRoutedEventHandler OnMoveCaret(CaretMovementType type) {
            return (s, e) => {
                _caret.Move(type);
            };
        }

        private static ExecutedRoutedEventHandler OnMoveCaretExtendingSelection(CaretMovementType type) {
            return (s, e) => {
                _caret.Move(type);
                
            };
        }
    }
}
