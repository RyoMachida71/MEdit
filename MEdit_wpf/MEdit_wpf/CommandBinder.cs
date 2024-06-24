using MEdit_wpf.CaretNavigators;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace MEdit_wpf {
    public static class CommandBinder {

        private static TextArea _textArea;

        private static List<CommandBinding> _commands = new List<CommandBinding>();

        private static List<InputBinding> _inputs = new List<InputBinding>();

        public static void SetBinding(TextArea textArea, CommandBindingCollection commands, InputBindingCollection inputs) {
            _textArea = textArea;
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

        static CommandBinder() {
            var none = ModifierKeys.None;
            var alt = ModifierKeys.Alt;
            var ctrl = ModifierKeys.Control;
            var shift = ModifierKeys.Shift;

            AddBinding(EditingCommands.MoveLeftByCharacter, none, Key.Left, OnMoveCaret(CaretMovementType.CharLeft, false));
            AddBinding(EditingCommands.SelectLeftByCharacter, shift, Key.Left, OnMoveCaret(CaretMovementType.CharLeft, true));
            AddBinding(EditingCommands.MoveRightByCharacter, none, Key.Right, OnMoveCaret(CaretMovementType.CharRight, false));
            AddBinding(EditingCommands.SelectRightByCharacter, shift, Key.Right, OnMoveCaret(CaretMovementType.CharRight, true));
            AddBinding(EditingCommands.MoveUpByLine, none, Key.Up, OnMoveCaret(CaretMovementType.LineUp, false));
            AddBinding(EditingCommands.SelectUpByLine, shift, Key.Up, OnMoveCaret(CaretMovementType.LineUp, true));
            AddBinding(EditingCommands.MoveDownByLine, none, Key.Down, OnMoveCaret(CaretMovementType.LineDown, false));
            AddBinding(EditingCommands.SelectDownByLine, shift, Key.Down, OnMoveCaret(CaretMovementType.LineDown, true));
            AddBinding(EditingCommands.MoveLeftByWord, ctrl, Key.Left, OnMoveCaret(CaretMovementType.WordLeft, false));
            AddBinding(EditingCommands.SelectLeftByWord, ctrl | shift, Key.Left, OnMoveCaret(CaretMovementType.WordLeft, true));
            AddBinding(EditingCommands.MoveRightByWord, ctrl, Key.Right, OnMoveCaret(CaretMovementType.WordRight, false));
            AddBinding(EditingCommands.SelectRightByWord, ctrl | shift, Key.Right, OnMoveCaret(CaretMovementType.WordRight, true));
            AddBinding(EditingCommands.MoveToLineStart, none, Key.Home, OnMoveCaret(CaretMovementType.LineStart, false));
            AddBinding(EditingCommands.SelectToLineStart, shift, Key.Home, OnMoveCaret(CaretMovementType.LineStart, true));
            AddBinding(EditingCommands.MoveToLineEnd, none, Key.End, OnMoveCaret(CaretMovementType.LineEnd, false));
            AddBinding(EditingCommands.SelectToLineEnd, shift, Key.End, OnMoveCaret(CaretMovementType.LineEnd, true));
            AddBinding(EditingCommands.MoveToDocumentStart, ctrl, Key.Home, OnMoveCaret(CaretMovementType.DocumentStart, false));
            AddBinding(EditingCommands.SelectToDocumentStart, ctrl | shift, Key.Home, OnMoveCaret(CaretMovementType.DocumentStart, true));
            AddBinding(EditingCommands.MoveToDocumentEnd, ctrl, Key.End, OnMoveCaret(CaretMovementType.DocumentEnd, false));
            AddBinding(EditingCommands.SelectToDocumentEnd, ctrl | shift, Key.End, OnMoveCaret(CaretMovementType.DocumentEnd, true));

            AddBinding(EditingCommands.Delete, none, Key.Delete, OnDeleteText(EditingDirection.Forward));
            AddBinding(EditingCommands.Backspace, none, Key.Back, OnDeleteText(EditingDirection.Backward));
        }

        private static ExecutedRoutedEventHandler OnMoveCaret(CaretMovementType type, bool isSelectionMode) {
            return (s, e) => {
                _textArea.Caret.Move(CaretNavigatorContainer.GetNavigator(type), isSelectionMode);
            };
        }

        private static ExecutedRoutedEventHandler OnDeleteText(EditingDirection direction) {
            return (s, e) => {
                _textArea.DeleteText(direction);
            };
        }
    }
}
