using MEdit_wpf;

namespace MEdit_Test {
    public class Tests {

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
    }
}