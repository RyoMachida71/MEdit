
using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestLineStartNavigator {
        [Test]
        public void TestLineStart() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineStartNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 0)));
        }

        [Test]
        public void TestStaysCurrentPositionAtLineStart() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineStartNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 0)));
        }
    }
}
