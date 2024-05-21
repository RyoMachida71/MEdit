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
            var lines = document.Lines;
            Assert.That(lines, Has.Count.EqualTo(3));

            Assert.That(lines[0].Text, Is.EqualTo("This is the first line."));
            Assert.That(lines[0].LineNumber, Is.EqualTo(0));
            Assert.That(lines[0].Length, Is.EqualTo(23));
            Assert.That(lines[0].Offset, Is.EqualTo(0));

            Assert.That(lines[1].Text, Is.EqualTo("This is the second line."));
            Assert.That(lines[1].LineNumber, Is.EqualTo(1));
            Assert.That(lines[1].Length, Is.EqualTo(24));
            Assert.That(lines[1].Offset, Is.EqualTo(25));

            Assert.That(lines[2].Text, Is.EqualTo(""));
            Assert.That(lines[2].LineNumber, Is.EqualTo(2));
            Assert.That(lines[2].Length, Is.EqualTo(0));
            Assert.That(lines[2].Offset, Is.EqualTo(51));
        }

        [TestCase(0, 0, 0, 0, "", TestName = "Empty")]
        [TestCase(0, 0, 0, 3, "the", TestName = "RangeForward")]
        [TestCase(0, 14, 0, 10, "line", TestName = "RangeBackward")]
        public void TestGetText(int startRow, int startCol, int endRow, int endCol, string expected) {
            var text = "the first line";
            var document = new TextDocument(text);
            var segment = document.GetText(new TextPosition(startRow, startCol), new TextPosition(endRow, endCol));
            Assert.That(segment, Is.EqualTo(expected));
        }
    }
}