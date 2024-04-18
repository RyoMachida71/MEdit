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
            var text = "This is the first line.\r\nThis is the second line.\r\n";
            var document = new TextDocument(text);
            Assert.That(document.Lines, Has.Count.EqualTo(2));

            var offset = document.GetOffset(0, 0);
            Assert.That(offset, Is.EqualTo(0));
            Assert.That(document.Text[offset], Is.EqualTo('T'));

            offset = document.GetOffset(1, 3);
            Assert.That(offset, Is.EqualTo(28));
            Assert.That(document.Text[offset], Is.EqualTo('s'));
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