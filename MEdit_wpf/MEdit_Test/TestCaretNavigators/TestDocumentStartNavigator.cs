using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestDocumentStartNavigator {
        [Test]
        public void TestDocumentStart() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new DocumentStartNavigator();
            var position = navigator.GetNextPosition(new TextPosition(1, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 0)));
        }

        [Test]
        public void TestStaysCurrentPositionAtDocumentStart() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new DocumentStartNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 0), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 0)));
        }
    }
}
