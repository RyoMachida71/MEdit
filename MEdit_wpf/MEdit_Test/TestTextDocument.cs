using MEdit_wpf;

namespace MEdit_Test {
    public class TestTextDocument {

        [Test]
        public void TestPrependText() {
            var document = new TextDocument();
            document.Insert(0, "test");
            Assert.That(document.Text, Is.EqualTo("test"));
        }

        [Test]
        public void TestAppendText() {
            var document = new TextDocument("test");
            document.Insert(4, "test");
            Assert.That(document.Text, Is.EqualTo("testtest"));
        }

        [Test]
        public void TestLines() {
            var text = "This is a test.\r\nThis is a test.\r\nThis is a test.";
            var document = new TextDocument(text);
            Assert.That(document.Lines, Has.Count.EqualTo(3));
            var firstLine = document.Lines[0];
            Assert.That(firstLine, Is.EqualTo("This is a test."));
        }
    }
}