using MEdit_wpf;
using MEdit_wpf.Caret;
using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using Moq;
using System.Xml.Serialization;

namespace MEdit_Test {
    public class TestSingleCaret {
        private static Action EmptyAction = () => { };

        [Test]
        public void TestInitSingleCaret() {
            var mock = new Mock<ITextArea>();
            mock.SetupGet(x => x.Document).Returns(new TextDocument());
            var caret = new SingleCaret(mock.Object, EmptyAction);
            Assert.That(caret.Position, Is.EqualTo(new TextPosition(0, 0)));
        }

        [Test]
        public void TestCaretStaysCurrentPositionWhenDocumentIsEmpty() {
            var textAreaMock = new Mock<ITextArea>();
            textAreaMock.SetupGet(x => x.Document).Returns(new TextDocument());
            var naviMock = new Mock<ICaretNavigator>();
            naviMock.Setup(x => x.GetNextPosition(new TextPosition(0, 0), textAreaMock.Object.Document)).Returns(new TextPosition(0, 1));

            var caret = new SingleCaret(textAreaMock.Object, EmptyAction);
            caret.Move(naviMock.Object, false);
            Assert.That(caret.Position, Is.EqualTo(new TextPosition(0, 0)));
            Assert.That(caret.Selection.StartPosition, Is.EqualTo(new TextPosition(0, 0)));
            Assert.That(caret.Selection.EndPosition, Is.EqualTo(new TextPosition(0, 0)));
        }

        [Test]
        public void TestCaretMoves() {
            var textAreaMock = new Mock<ITextArea>();
            textAreaMock.SetupGet(x => x.Document).Returns(new TextDocument("test"));
            var naviMock = new Mock<ICaretNavigator>();
            naviMock.Setup(x => x.GetNextPosition(new TextPosition(0, 0), textAreaMock.Object.Document)).Returns(new TextPosition(0, 1));

            var caret = new SingleCaret(textAreaMock.Object, EmptyAction);
            caret.Move(naviMock.Object, false);
            Assert.That(caret.Position, Is.EqualTo(new TextPosition(0, 1)));
            Assert.That(caret.Selection.StartPosition, Is.EqualTo(new TextPosition(0, 1)));
            Assert.That(caret.Selection.EndPosition, Is.EqualTo(new TextPosition(0, 1)));
        }

        [Test]
        public void TestCaretMovesWithSelection() {
            var textAreaMock = new Mock<ITextArea>();
            textAreaMock.SetupGet(x => x.Document).Returns(new TextDocument("test"));
            var naviMock = new Mock<ICaretNavigator>();
            naviMock.Setup(x => x.GetNextPosition(new TextPosition(0, 0), textAreaMock.Object.Document)).Returns(new TextPosition(0, 1));

            var caret = new SingleCaret(textAreaMock.Object, EmptyAction);
            caret.Move(naviMock.Object, true);
            Assert.That(caret.Position, Is.EqualTo(new TextPosition(0, 1)));
            Assert.That(caret.Selection.StartPosition, Is.EqualTo(new TextPosition(0, 0)));
            Assert.That(caret.Selection.EndPosition, Is.EqualTo(new TextPosition(0, 1)));
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
