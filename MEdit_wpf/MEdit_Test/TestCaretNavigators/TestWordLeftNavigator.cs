using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestWordLeftNavigator {
        [Test]
        public void TestReturnsPositionAfterNoLetterOrDigitAndBeforeLetterOrDigit() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 9), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 5)));

            doc = new TextDocument("  test    ");
            position = navigator.GetNextPosition(new TextPosition(0, 10), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 2)));
        }

        [Test]
        public void TestReturnsPositionAfterNoLetterOrDigitAndBeforeLetterOrDigitAtLineStart() {
            var doc = new TextDocument("  test    \r\ntest.test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 2)));
        }

        [Test]
        public void TestWordLeftSkipsConsecutiveNoDigitsOrLettersUntilLineStart() {
            var doc = new TextDocument("test   \r\n     test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 5), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 0)));
        }

        [Test]
        public void TestWordLeftStopsAtControlCharacter() {
            var doc = new TextDocument("test\r\nte\tst");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 5), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 3)));

            doc = new TextDocument("test \t  \r\n     test");
            position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 0)));
        }

        [Test]
        public void TestReturnsOneLeftPositionIfCurrentPositionBetweenPunctuation() {
            var doc = new TextDocument("    \r\n  ,.");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 3), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 2)));
        }

        [Test]
        public void TestReturnsCurrentPositionAtDocumentStart() {
            var doc = new TextDocument("test\r\ntest.test");
            var navigator = new WordLeftNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 0)));
        }
    }
}
