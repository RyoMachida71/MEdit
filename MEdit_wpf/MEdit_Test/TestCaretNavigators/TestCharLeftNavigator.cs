using MEdit_wpf;
using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using Moq;

namespace MEdit_Test.TestCaretNavigators {
    public class TestCharLeftNavigator {
        [Test]
        public void TestCharLeft() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new CharLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 1)));
        }

        [Test]
        public void TestCharLeftAtLineStart() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new CharLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 4)));
        }

        [Test]
        public void TestCharLeftAtDocumentStart() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new CharLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 0)));
        }
    }
}
