using MEdit;
using NUnit.Framework;

namespace Test {
    public class Tests {

        [Test]
        public void TestInsertAtBeginning() {
            var buffer = new TextBuffer();
            buffer.Insert(0, "test");
            Assert.That(buffer.Text, Is.EqualTo("test"));
            Assert.That(buffer.Text.Length, Is.EqualTo(4));
        }

        [Test]
        public void TestInsertInMiddleOfText() {
            var buffer = new TextBuffer();
            buffer.Insert(0, "test");
            buffer.Insert(2, "te");
            Assert.That(buffer.Text, Is.EqualTo("tetest"));
            Assert.That(buffer.Text.Length, Is.EqualTo(6));
        }

        [Test]
        public void TestInsertSpecialChar() {
            var buffer = new TextBuffer();
            buffer.Insert(0, "test\n");
            buffer.Insert(5, "te\tst");
            Assert.That(buffer.Text, Is.EqualTo("test\nte\tst"));
            Assert.That(buffer.Text.Length, Is.EqualTo(10));
        }

        [Test]
        public void TestDeleteRange() {
            var buffer = new TextBuffer();
            buffer.Insert(0, "testtest");
            buffer.DeleteRange(0, 4);
            Assert.That(buffer.Text, Is.EqualTo("test"));
            Assert.That(buffer.Text.Length, Is.EqualTo(4));
        }

        [Test]
        public void TestDeleteBackward() {
            var buffer = new TextBuffer();
            buffer.Insert(0, "testtest");
            buffer.DeleteBackward(4);
            Assert.That(buffer.Text, Is.EqualTo("testest"));
            Assert.That(buffer.Text.Length, Is.EqualTo(7));
        }

        [Test]
        public void TestReplace() {
            var buffer = new TextBuffer();
            buffer.Insert(0, "testtesttest");
            buffer.Replace(4, 8, "test", "a");
            Assert.That(buffer.Text, Is.EqualTo("testatest"));
            Assert.That(buffer.Text.Length, Is.EqualTo(9));
        }
    }
}