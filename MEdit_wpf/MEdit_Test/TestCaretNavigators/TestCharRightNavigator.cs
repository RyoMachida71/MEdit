using MEdit_wpf;
using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;

namespace MEdit_Test.TestCaretNavigators {
    public class TestCharRightNavigator {
        [Test]
        public void TestCharRight() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new CharRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 3)));
        }

        [Test]
        public void TestCharRightAtLineEnd() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new CharRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 0)));
        }

        [Test]
        public void TestCharRightAtDocumentEnd() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new CharRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }
    }
}
