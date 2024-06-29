using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestWorkRightNavigator {
        [Test]
        public void TestWordRight() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }

        [Test]
        public void TestReturnsNextLineStopPositionAtLineEnd() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }

        [Test]
        public void TestWordLeftStopsAtControlCharacter() {
            var doc = new TextDocument("test\r\nte\tst");
            var navigator = new WordRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 2)));
        }

        [Test]
        public void TestReturnsLinetEndIFStopPositionDoesNotExists() {
            var doc = new TextDocument("test   \r\n   ");
            var navigator = new WordRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 7)));
        }

        [Test]
        public void TestReturnsCurrentPositionAtDocumentEnd() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordRightNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 9), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 9)));
        }
    }
}
