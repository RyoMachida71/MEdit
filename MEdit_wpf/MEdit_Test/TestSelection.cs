using MEdit_wpf;
using MEdit_wpf.Selection;

namespace MEdit_Test {
    public class TestSelection {
        [Test]
        public void TestSingleSelection() {
            var document = new TextDocument("selection test");
            var sel = new SingleSelection(5, 10, document);
            Assert.That(sel.StartPosition, Is.EqualTo(5));
            Assert.That(sel.EndPosition, Is.EqualTo(10));
            Assert.That(sel.SelectedText, Is.EqualTo("tion "));
            Assert.That(sel.HasSelection, Is.True);
        }
    }
}
