using MEdit_wpf;

namespace MEdit_Test {
    public class TestCaret {
        [Test]
        public void TestInitCaret() {
            var caret = new Caret(new TextDocument());
            Assert.That(caret.Row, Is.EqualTo(0));
            Assert.That(caret.Column, Is.EqualTo(0));
        }

        [TestCase(CaretMovementType.CharLeft, 0, 0, 0, 0, TestName = "TopLeft")]
        [TestCase(CaretMovementType.CharLeft, 0, 2, 0, 1, TestName = "MoveLeft")]
        [TestCase(CaretMovementType.CharRight, 2, 3, 2, 3, TestName = "BottomRight")]
        [TestCase(CaretMovementType.CharRight, 0, 1, 0, 2, TestName = "MoveRight")]
        [TestCase(CaretMovementType.LineUp, 0, 1, 0, 1, TestName = "TopLineUp")]
        [TestCase(CaretMovementType.LineUp, 2, 3, 1, 3, TestName = "MoveLineUp")]
        [TestCase(CaretMovementType.LineDown, 2, 1, 2, 1, TestName = "BottomLineDown")]
        [TestCase(CaretMovementType.LineDown, 1, 1, 2, 1, TestName = "MoveLineDown")]
        public void TestMoveCaretCharLeft(CaretMovementType type, int row, int column, int expectedRow, int expectedColumn) {
            var caret = new Caret(new TextDocument("test\r\ntest\r\ntest"));
            caret.Row = row;
            caret.Column = column;
            caret.Move(type);
            Assert.That(caret.Row, Is.EqualTo(expectedRow));
            Assert.That(caret.Column, Is.EqualTo(expectedColumn));
        }
    }
}
