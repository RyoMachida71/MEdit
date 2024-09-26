using MEdit_wpf;
using MEdit_wpf.Document;

namespace MEdit_Test {
    public class TestDocumentOperation {
        [Test]
        public void TestDocumentUndo() {
            int actual1 = 0;
            int actual2 = 0;
            string actual3 = string.Empty;
            var callback = (int a, int b, string c) => { actual1 = a; actual2 = b; actual3 = c; };
            var e = new DocumentChangedEventArgs(0, 4, "abc", "defg", TextPosition.Empty);
            var operation = new DocumentOperation(callback, e);

            operation.Undo();

            Assert.That(actual1, Is.EqualTo(0));
            Assert.That(actual2, Is.EqualTo(4));
            Assert.That(actual3, Is.EqualTo("abc"));
        }

        [Test]
        public void TestDocumentRedo() {
            int actual1 = 0;
            int actual2 = 0;
            string actual3 = string.Empty;
            var callback = (int a, int b, string c) => { actual1 = a; actual2 = b; actual3 = c; };
            var e = new DocumentChangedEventArgs(0, 4, "abc", "defg", TextPosition.Empty);
            var operation = new DocumentOperation(callback, e);

            operation.Redo();

            Assert.That(actual1, Is.EqualTo(0));
            Assert.That(actual2, Is.EqualTo(3));
            Assert.That(actual3, Is.EqualTo("defg"));
        }
    }
}
