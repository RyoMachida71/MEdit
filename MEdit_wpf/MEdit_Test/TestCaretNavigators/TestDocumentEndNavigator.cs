using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestDocumentEndNavigator {
        [Test]
        public void TestDocumentEnd() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new DocumentEndNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }

        [Test]
        public void TestStaysCurrentPositionAtDocumentEnd() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new DocumentEndNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 4), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(1, 4)));
        }
    }
}
