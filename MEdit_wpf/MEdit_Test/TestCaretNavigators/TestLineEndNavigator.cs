using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestLineEndNavigator {
        [Test]
        public void TestLineEnd() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineEndNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }

        [Test]
        public void TestStaysCurrentPositionAtLineEnd() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineEndNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }
    }
}
