using MEdit_wpf;

namespace MEdit_Test {
    public class TestTextDocument {

        [Test]
        public void TestPrependText() {
            var document = new TextDocument();
            document.Insert(new TextPosition(0, 0), new TextInput("test"));
            Assert.That(document.Text, Is.EqualTo("test"));
        }

        [Test]
        public void TestAppendText() {
            var document = new TextDocument("test");
            document.Insert(new TextPosition(0, 4), new TextInput("test"));
            Assert.That(document.Text, Is.EqualTo("testtest"));
        }

        [Test]
        public void TestLines() {
            var text = "This is the first line.\r\nThis is the second line.\r\n";
            var document = new TextDocument(text);
            Assert.That(document.Lines, Has.Count.EqualTo(2));
            Assert.That(document.Lines[0].Text, Is.EqualTo("This is the first line.\r\n"));
            Assert.That(document.Lines[1].Text, Is.EqualTo("This is the second line.\r\n"));
        }

        [TestCase(0, 0, "", TestName = "Empty")]
        [TestCase(0, 3, "the", TestName = "RangeForward")]
        [TestCase(14, 10, "line", TestName = "RangeBackward")]
        public void TestGetText(int start, int end, string expected) {
            var text = "the first line";
            var document = new TextDocument(text);
            var segment = document.GetText(start, end);
            Assert.That(segment, Is.EqualTo(expected));
        }
    }
}