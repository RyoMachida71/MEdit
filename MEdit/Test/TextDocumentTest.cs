using MEdit;

namespace Test {
    public class TextDocumentTest {

        [Test]
        public void TestInsertText() {
            var doc = new TextDocument();
            doc.Insert(0, "test");
            Assert.That(doc.Text, Is.EqualTo("test"));
        }

        [Test]
        public void TestReplaceText() {
            var doc = new TextDocument();
            doc.Insert(0, "testtest");
            doc.Replace(0, 4, "test", "buffer");
            Assert.That(doc.Text, Is.EqualTo("buffertest"));
        }

        [Test]
        public void TestDeleteText() {
            var doc = new TextDocument();
            doc.Insert(0, "test");
            doc.Delete(0, 2);
            Assert.That(doc.Text, Is.EqualTo("st"));
        }

        // todo: implement function to get offset of specified string
    }
}
