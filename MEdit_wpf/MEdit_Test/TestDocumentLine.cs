using MEdit_wpf;

namespace MEdit_Test {
    public class TestDocumentLine {

        [Test]
        public void TestInitDocumentLine() {
            var text = "testtesttest";
            var line = new DocumentLine(2, 15, text);
            Assert.That(line.LineNumber, Is.EqualTo(2));
            Assert.That(line.Offset, Is.EqualTo(15));
            Assert.That(line.Text, Is.EqualTo("testtesttest"));
        }
    }
}
