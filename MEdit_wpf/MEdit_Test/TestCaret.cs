using MEdit_wpf;

namespace MEdit_Test {
    public class TestCaret {
        private static Action EmptyAction = () => { };

        [Test]
        public void TestInitCaret() {
            var caret = new Caret(new TextDocument(), EmptyAction);
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
        public void TestMoveCaret(CaretMovementType type, int row, int column, int expectedRow, int expectedColumn) {
            var caret = new Caret(new TextDocument("test\r\ntest\r\ntest"), EmptyAction);
            caret.Position = new TextPosition(row, column);
            caret.Move(type);
            Assert.That(caret.Position.Row, Is.EqualTo(expectedRow));
            Assert.That(caret.Position.Column, Is.EqualTo(expectedColumn));
        }

        [TestCase("\r\n", 1, 0, TestName = "NewLineInput")]
        [TestCase("abc", 0, 5, TestName = "OrdinalInput")]
        public void TestCaretPositionUpdateByInput(string input, int expectedRow, int expectedColumn) {
            var caret = new Caret(new TextDocument("test"), EmptyAction);
            caret.Position = new TextPosition(0, 2);
            caret.UpdatePos(input);
            Assert.That(caret.Position.Row, Is.EqualTo(expectedRow));
            Assert.That(caret.Position.Column, Is.EqualTo(expectedColumn));
        }
    }
}
