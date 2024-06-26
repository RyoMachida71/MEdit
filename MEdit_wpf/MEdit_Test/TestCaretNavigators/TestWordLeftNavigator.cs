using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestWordLeftNavigator {
        [Test]
        public void TestWordLeft() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 9), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 5)));
        }

        [Test]
        public void TestWordLeftSkipsConsecutiveNoDigitLetters() {
            var doc = new TextDocument("test   \r\n     test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 5), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 4)));
        }

        [Test]
        public void TestWordLeftStopsAtControlCharacter() {
            var doc = new TextDocument("test\r\nte\tst");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 5), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 3)));

            doc = new TextDocument("test \t  \r\n     test");
            position = navigator.GetNextPosition(new TextPosition(1, 1), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 6)));
        }

        [Test]
        public void TestReturnsCurrentPositionAtDocumentStart() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 0)));
        }

        [Test]
        public void TestReturnsPrevioustLineEndAtLineStart() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 4)));
        }
    }
}
