﻿using MEdit_wpf;
using MEdit_wpf.Caret;
using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using Moq;

namespace MEdit_Test {
    public class TestSingleCaret {
        private static Action EmptyAction = () => { };

        [Test]
        public void TestInitSingleCaret() {
            var mock = new Mock<ITextArea>();
            mock.SetupGet(x => x.Document).Returns(new TextDocument());
            var caret = new SingleCaret(mock.Object, EmptyAction);
            Assert.That(caret.Position.Row, Is.EqualTo(0));
            Assert.That(caret.Position.Column, Is.EqualTo(0));
        }

        [TestCase(CaretMovementType.CharLeft, 0, 0, 0, 0, TestName = "TopLeft")]
        [TestCase(CaretMovementType.CharLeft, 0, 2, 0, 1, TestName = "MoveLeft")]
        [TestCase(CaretMovementType.CharRight, 2, 4, 2, 4, TestName = "BottomRight")]
        [TestCase(CaretMovementType.CharRight, 0, 1, 0, 2, TestName = "MoveRight")]
        [TestCase(CaretMovementType.LineUp, 0, 1, 0, 1, TestName = "TopLineUp")]
        [TestCase(CaretMovementType.LineUp, 2, 3, 1, 3, TestName = "MoveLineUp")]
        [TestCase(CaretMovementType.LineDown, 2, 1, 2, 1, TestName = "BottomLineDown")]
        [TestCase(CaretMovementType.LineDown, 1, 1, 2, 1, TestName = "MoveLineDown")]
        [TestCase(CaretMovementType.LineStart, 0, 3, 0, 0, TestName = "LineStart")]
        [TestCase(CaretMovementType.LineStart, 0, 0, 0, 0, TestName = "LineStartAtLineStart")]
        [TestCase(CaretMovementType.LineEnd, 0, 3, 0, 4, TestName = "LineEnd")]
        [TestCase(CaretMovementType.LineEnd, 0, 4, 0, 4, TestName = "LineStartAtLineEnd")]
        [TestCase(CaretMovementType.DocumentStart, 1, 4, 0, 0, TestName = "DocumentStart")]
        [TestCase(CaretMovementType.DocumentStart, 0, 0, 0, 0, TestName = "DocumentStartAtDocumentStart")]
        [TestCase(CaretMovementType.DocumentEnd, 1, 4, 2, 4, TestName = "DocumentEnd")]
        [TestCase(CaretMovementType.DocumentEnd, 2, 4, 2, 4, TestName = "DocumentEndAtDocumentEnd")]
        public void TestMoveCaret(CaretMovementType type, int row, int column, int expectedRow, int expectedColumn) {
            var mock = new Mock<ITextArea>();
            var doc = new TextDocument("test\r\ntest\r\ntest");
            mock.SetupGet(x => x.Document).Returns(doc);
            var caret = new SingleCaret(mock.Object, EmptyAction);
            caret.Position = new TextPosition(row, column);
            caret.Move(type);
            Assert.That(caret.Position.Row, Is.EqualTo(expectedRow));
            Assert.That(caret.Position.Column, Is.EqualTo(expectedColumn));
            Assert.That(caret.Selection.StartPosition, Is.EqualTo(caret.Selection.EndPosition));
        }

        [TestCase("\r\n", 1, 0, TestName = "NewLineInput")]
        [TestCase("abc", 0, 5, TestName = "OrdinalInput")]
        [TestCase("a\r\nb\r\n", 2, 0, TestName = "NewLinesInput")]
        public void TestCaretPositionUpdateByInput(string input, int expectedRow, int expectedColumn) {
            var mock = new Mock<ITextArea>();
            var doc = new TextDocument("test");
            mock.SetupGet(x => x.Document).Returns(doc);

            var caret = new SingleCaret(mock.Object, EmptyAction);
            var position = new TextPosition(0, 2);
            caret.Position = position;
            caret.Selection.Unselect(caret.Position);
            mock.Object.Document.Replace(caret.Selection.StartPosition, caret.Selection.EndPosition, new TextInput(input));
            Assert.That(caret.Position.Row, Is.EqualTo(expectedRow));
            Assert.That(caret.Position.Column, Is.EqualTo(expectedColumn));
            Assert.That(caret.Selection.StartPosition, Is.EqualTo(caret.Selection.EndPosition));
        }

        [Test]
        public void TestCaretPositionWithSelectionUpdateByInput() {
            var mock = new Mock<ITextArea>();
            var doc = new TextDocument("test");
            mock.SetupGet(x => x.Document).Returns(doc);

            var caret = new SingleCaret(mock.Object, EmptyAction);
            var position = new TextPosition(0, 4);
            caret.Position = position;
            caret.Selection.StartOrExtend(new TextPosition(0, 2), caret.Position);
            mock.Object.Document.Replace(caret.Selection.StartPosition, caret.Selection.EndPosition, new TextInput("test\r\ntest"));
            Assert.That(caret.Position.Row, Is.EqualTo(1));
            Assert.That(caret.Position.Column, Is.EqualTo(4));
        }

        [Test]
        public void TestCaretPositionUpdateByDelete() {
            var mock = new Mock<ITextArea>();
            var doc = new TextDocument("test");
            mock.SetupGet(x => x.Document).Returns(doc);

            var caret = new SingleCaret(mock.Object, EmptyAction);
            var position = new TextPosition(0, 2);
            caret.Position = position;
            caret.Selection.Unselect(caret.Position);
            mock.Object.Document.Delete(caret.Selection.StartPosition, caret.Selection.EndPosition, EditingDirection.Forward);
            Assert.That(caret.Position.Row, Is.EqualTo(0));
            Assert.That(caret.Position.Column, Is.EqualTo(2));
            Assert.That(caret.Selection.StartPosition, Is.EqualTo(caret.Selection.EndPosition));
        }

        [Test]
        public void TestCaretPositionWithSelectionUpdateByDelete() {
            var mock = new Mock<ITextArea>();
            var doc = new TextDocument("test\r\ntest");
            mock.SetupGet(x => x.Document).Returns(doc);

            var caret = new SingleCaret(mock.Object, EmptyAction);
            var position = new TextPosition(1, 4);
            caret.Position = position;
            caret.Selection.StartOrExtend(new TextPosition(0, 2), caret.Position);
            mock.Object.Document.Delete(caret.Selection.StartPosition, caret.Selection.EndPosition, EditingDirection.Forward);
            Assert.That(caret.Position.Row, Is.EqualTo(0));
            Assert.That(caret.Position.Column, Is.EqualTo(2));
        }
    }
}
