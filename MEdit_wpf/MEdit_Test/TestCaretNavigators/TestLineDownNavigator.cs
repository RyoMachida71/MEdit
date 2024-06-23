using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestLineDownNavigator {
        [Test]
        public void TestLineDown() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineDownNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 2)));
        }

        [Test]
        public void TestStaysCurrentPositionAtLastLine() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineDownNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 2)));
        }

        [Test]
        public void TestLineDownAtLineEnd() {
            var doc = new TextDocument("testtest\r\ntest");
            var navigator = new LineDownNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 8), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }
    }
}
