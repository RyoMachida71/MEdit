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

            AddBinding(EditingCommands.MoveLeftByCharacter, none, Key.Left, OnMoveCaret(CaretMovementType.CharLeft));
            AddBinding(EditingCommands.SelectLeftByCharacter, shift, Key.Left, OnMoveCaret(CaretMovementType.CharLeftExtendingSelection));
            AddBinding(EditingCommands.MoveRightByCharacter, none, Key.Right, OnMoveCaret(CaretMovementType.CharRight));
            AddBinding(EditingCommands.SelectRightByCharacter, shift, Key.Right, OnMoveCaret(CaretMovementType.CharRightExtendingSelection));
            AddBinding(EditingCommands.MoveUpByLine, none, Key.Up, OnMoveCaret(CaretMovementType.LineUp));
            AddBinding(EditingCommands.SelectUpByLine, shift, Key.Up, OnMoveCaret(CaretMovementType.LineUpExtendingSelection));
            AddBinding(EditingCommands.MoveDownByLine, none, Key.Down, OnMoveCaret(CaretMovementType.LineDown));
            AddBinding(EditingCommands.SelectDownByLine, shift, Key.Down, OnMoveCaret(CaretMovementType.LineDownExtendingSelection));
            AddBinding(EditingCommands.MoveLeftByWord, ctrl, Key.Left, OnMoveCaret(CaretMovementType.WordLeft));
            AddBinding(EditingCommands.SelectLeftByWord, ctrl | shift, Key.Left, OnMoveCaret(CaretMovementType.WordLeft));
            AddBinding(EditingCommands.MoveRightByWord, ctrl, Key.Right, OnMoveCaret(CaretMovementType.WordRight));
            AddBinding(EditingCommands.SelectRightByWord, ctrl | shift, Key.Right, OnMoveCaret(CaretMovementType.WordRight));
            AddBinding(EditingCommands.MoveToLineStart, none, Key.Home, OnMoveCaret(CaretMovementType.LineStart));
            AddBinding(EditingCommands.SelectToLineStart, shift, Key.Home, OnMoveCaret(CaretMovementType.LineStartExtendingSelection));
            AddBinding(EditingCommands.MoveToLineEnd, none, Key.End, OnMoveCaret(CaretMovementType.LineEnd));
            AddBinding(EditingCommands.SelectToLineEnd, shift, Key.End, OnMoveCaret(CaretMovementType.LineEndExtendingSelection));
            AddBinding(EditingCommands.MoveToDocumentStart, ctrl, Key.Home, OnMoveCaret(CaretMovementType.DocumentStart));
            AddBinding(EditingCommands.SelectToDocumentStart, ctrl | shift, Key.Home, OnMoveCaret(CaretMovementType.DocumentStartSelection));
            AddBinding(EditingCommands.MoveToDocumentEnd, ctrl, Key.End, OnMoveCaret(CaretMovementType.DocumentEnd));
            AddBinding(EditingCommands.SelectToDocumentEnd, ctrl | shift, Key.End, OnMoveCaret(CaretMovementType.DocumentEndSelection));

            AddBinding(EditingCommands.Delete, none, Key.Delete, OnDeleteText(EditingDirection.Forward));
            AddBinding(EditingCommands.Backspace, none, Key.Back, OnDeleteText(EditingDirection.Backward));
        }

        private static ExecutedRoutedEventHandler OnMoveCaret(CaretMovementType type) {
            return (s, e) => {
                _textArea.Caret.Move(type);
            };
        }

        private static ExecutedRoutedEventHandler OnDeleteText(EditingDirection direction) {
            return (s, e) => {
                _textArea.DeleteText(direction);
            };
        }
    }
}
