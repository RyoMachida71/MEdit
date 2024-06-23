using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestLineUpNavigator {
        [Test]
        public void TestLineUp() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineUpNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 2)));
        }

        [Test]
        public void TestStaysCurrentPositionAtFirstLine() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new LineUpNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 2)));
        }

        [Test]
        public void TestLineUpAtLineEnd() {
            var doc = new TextDocument("test\r\ntesttest");
            var navigator = new LineUpNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 8), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 4)));
        }
    }
}
