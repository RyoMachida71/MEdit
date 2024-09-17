using MEdit_wpf.Document;

namespace MEdit_Test {
    public class TestDocumentOperation {
        [Test]
        public void TestDocumentUndo() {
            var operation = new DocumentOperation();
            operation.Undo();
        }
    }
}
