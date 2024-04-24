using MEdit_wpf;
using MEdit_wpf.Selection;

namespace MEdit_Test {
    public class TestSelection {
        [Test]
        public void TestSingleSelection() {
            var document = new TextDocument("selection test");
            var startPos = new TextPosition(0, 5);
            var endPos = new TextPosition(0, 10);
            var sel = new SingleSelection(startPos, endPos, document);
            Assert.That(sel.SelectedText, Is.EqualTo("tion "));
            Assert.That(sel.HasSelection, Is.True);
        }

        [Test]
        public void TestEmptySelection() {
            var document = new TextDocument("selection test");
            var pos = new TextPosition(5, 5);
            var sel = new SingleSelection(pos, pos, document);
            Assert.That(sel.SelectedText, Is.EqualTo(""));
            Assert.That(sel.HasSelection, Is.False);
        }
    }
}
