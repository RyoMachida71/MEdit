using MEdit_wpf.CaretNavigators;
using MEdit_wpf.Document;
using MEdit_wpf;

namespace MEdit_Test.TestCaretNavigators {
    public class TestNullNavigator {
        [Test]
        public void TestNullNavigatorReturnsCurrentPosition() {
            var doc = new TextDocument("test\r\ntest");
            var navigator = new NullNavigator();
            var position = navigator.GetNextPosition(new TextPosition(0, 2), doc);
            Assert.That(position, Is.EqualTo(new TextPosition(0, 2)));
        }
    }
}
