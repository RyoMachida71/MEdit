using MEdit_wpf;
using MEdit_wpf.Selection;
using Moq;

namespace MEdit_Test {
    public class TestSelection {
        [Test]
        public void TestSingleSelection() {
            var mock = new Mock<ITextArea>();
            var doc = new TextDocument("selection test");
            mock.SetupGet(x => x.Document).Returns(doc);
            var sel = new SingleSelection(mock.Object);
            sel.StartOrExtend(new TextPosition(0, 5), new TextPosition(0, 10));
            Assert.That(sel.StartPosition, Is.EqualTo(new TextPosition(0, 5)));
            Assert.That(sel.EndPosition, Is.EqualTo(new TextPosition(0, 10)));
            Assert.That(sel.SelectedText, Is.EqualTo("tion "));

            sel.StartOrExtend(TextPosition.Empty, new TextPosition(0, 11));
            Assert.That(sel.StartPosition, Is.EqualTo(new TextPosition(0, 5)));
            Assert.That(sel.EndPosition, Is.EqualTo(new TextPosition(0, 11)));
            Assert.That(sel.SelectedText, Is.EqualTo("tion t"));

            sel.Unselect();
            Assert.That(sel.StartPosition, Is.EqualTo(new TextPosition(0, 11)));
            Assert.That(sel.EndPosition, Is.EqualTo(new TextPosition(0, 11)));
            Assert.That(sel.SelectedText, Is.EqualTo(""));
        }
    }
}