using MEdit_wpf;

namespace MEdit_Test
{
    public class TestTextInput
    {
        [Test]
        public void TestOrdinalTextInput()
        {
            var input = new TextInput("t");
            Assert.That(input.Value, Is.EqualTo("t"));
            Assert.That(input.Length, Is.EqualTo(1));

            input = new TextInput("テスト");
            Assert.That(input.Value, Is.EqualTo("テスト"));
            Assert.That(input.Length, Is.EqualTo(3));
        }

        [Test]
        public void TestNullInput()
        {
            var input = new TextInput(null);
            Assert.That(input.Value, Is.EqualTo(""));
            Assert.That(input.Length, Is.EqualTo(0));
        }

        [Test]
        public void TestEolInput()
        {
            var input = new TextInput("\r");
            Assert.That(input.Value, Is.EqualTo("\r\n"));
            Assert.That(input.Length, Is.EqualTo(2));

            input = new TextInput("\n");
            Assert.That(input.Value, Is.EqualTo("\r\n"));
            Assert.That(input.Length, Is.EqualTo(2));
        }
    }
}
