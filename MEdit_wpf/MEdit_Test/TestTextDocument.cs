using MEdit_wpf;
using MEdit_wpf.Document;

namespace MEdit_Test {
    public class TestTextDocument {

        [Test]
        public void TestAddText() {
            var document = new TextDocument();
            var changed = new DocumentChangedEventArgs(0, 0, string.Empty, string.Empty, TextPosition.Empty);
            document.DocumentChanged += (s, e) => changed = e;

            document.Replace(new TextPosition(0, 0), new TextPosition(0, 0), new TextInput("test"));

            Assert.That(document.Text, Is.EqualTo("test"));
            Assert.That(changed.TextBeforeChange, Is.EqualTo(string.Empty));
            Assert.That(changed.TextAfterChange, Is.EqualTo("test"));
            Assert.That(changed.OldOffset, Is.EqualTo(0));
            Assert.That(changed.NewOffset, Is.EqualTo(4));
            Assert.That(changed.NewPosition, Is.EqualTo(new TextPosition(0, 4)));

            document = new TextDocument("test");
            document.Replace(new TextPosition(0, 2), new TextPosition(0, 2), new TextInput("\r\n"));

            Assert.That(document.Text, Is.EqualTo("te\r\nst"));
        }

        [Test]
        public void TestAddRangeText() {
            var document = new TextDocument("test");
            var changed = new DocumentChangedEventArgs(0, 0, string.Empty, string.Empty, TextPosition.Empty);
            document.DocumentChanged += (s, e) => changed = e;

            document.Replace(new TextPosition(0, 2), new TextPosition(0, 4), new TextInput("test"));

            Assert.That(document.Text, Is.EqualTo("tetest"));
            Assert.That(changed.TextBeforeChange, Is.EqualTo("st"));
            Assert.That(changed.TextAfterChange, Is.EqualTo("test"));
            Assert.That(changed.OldOffset, Is.EqualTo(2));
            Assert.That(changed.NewOffset, Is.EqualTo(6));
            Assert.That(changed.NewPosition, Is.EqualTo(new TextPosition(0, 6)));
        }

        [Test]
        public void TestReplace() {
            var document = new TextDocument("test\r\ntest");
            var changed = new DocumentChangedEventArgs(0, 0, string.Empty, string.Empty, TextPosition.Empty);
            document.DocumentChanged += (s, e) => changed = e;

            document.Replace(2, 6, "test");

            Assert.That(document.Text, Is.EqualTo("tetestst"));
            Assert.That(changed.TextBeforeChange, Is.EqualTo(string.Empty));
            Assert.That(changed.TextAfterChange, Is.EqualTo(string.Empty));
            Assert.That(changed.OldOffset, Is.EqualTo(2));
            Assert.That(changed.NewOffset, Is.EqualTo(6));
            Assert.That(changed.NewPosition, Is.EqualTo(new TextPosition(0, 6)));
        }

        [Test]
        public void TestDeleteBlankDocument() {
            var blankDocument = new TextDocument();
            var position = new TextPosition(0, 0);
            blankDocument.Delete(position, position, EditingDirection.Forward);
            Assert.That(blankDocument.Text, Is.EqualTo(""));
        }

        [Test]
        public void TestDeleteCharForward() {
            var text = "test\r\ntest";
            var document = new TextDocument(text);
            var position = new TextPosition(1, 4);
            document.Delete(position, position, EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo(text));

            document = new TextDocument(text);
            position = new TextPosition(0, 1);
            document.Delete(position, position, EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo("tst\r\ntest"));

            document = new TextDocument(text);
            position = new TextPosition(0, 4);
            document.Delete(position, position, EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo("testtest"));
        }

        [Test]
        public void TestDeleteCharBackward() {
            var text = "test\r\ntest";
            var document = new TextDocument(text);
            var position = new TextPosition(0, 0);
            document.Delete(position, position, EditingDirection.Backward);
            Assert.That(document.Text, Is.EqualTo(text));

            document = new TextDocument(text);
            position = new TextPosition(0, 1);
            document.Delete(position, position, EditingDirection.Backward);
            Assert.That(document.Text, Is.EqualTo("est\r\ntest"));

            document = new TextDocument(text);
            var changed = new DocumentChangedEventArgs(0, 0, string.Empty, string.Empty, TextPosition.Empty);
            document.DocumentChanged += (s, e) => changed = e;
            position = new TextPosition(1, 0);
            document.Delete(position, position, EditingDirection.Backward);
            Assert.That(document.Text, Is.EqualTo("testtest"));
            Assert.That(changed.TextBeforeChange, Is.EqualTo("\r\n"));
            Assert.That(changed.TextAfterChange, Is.EqualTo(string.Empty));
            Assert.That(changed.OldOffset, Is.EqualTo(6));
            Assert.That(changed.NewOffset, Is.EqualTo(4));
            Assert.That(changed.NewPosition, Is.EqualTo(new TextPosition(0, 4)));
        }

        [Test]
        public void TestDeleteRange()
        {
            var text = "test\r\ntest";
            var document = new TextDocument(text);
            var changed = new DocumentChangedEventArgs(0, 0, string.Empty, string.Empty, TextPosition.Empty);
            document.DocumentChanged += (s, e) => changed = e;
            document.Delete(new TextPosition(0, 1), new TextPosition(0, 3), EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo("tt\r\ntest"));
            Assert.That(changed.TextBeforeChange, Is.EqualTo("es"));
            Assert.That(changed.TextAfterChange, Is.EqualTo(string.Empty));
            Assert.That(changed.OldOffset, Is.EqualTo(1));
            Assert.That(changed.NewOffset, Is.EqualTo(1));
            Assert.That(changed.NewPosition, Is.EqualTo(new TextPosition(0, 1)));

            document = new TextDocument(text);
            document.Delete(new TextPosition(0, 1), new TextPosition(0, 3), EditingDirection.Backward);
            Assert.That(document.Text, Is.EqualTo("tt\r\ntest"));

            document = new TextDocument(text);
            document.Delete(new TextPosition(0, 4), new TextPosition(1, 0), EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo("testtest"));

            document = new TextDocument(text);
            document.Delete(new TextPosition(0, 4), new TextPosition(1, 2), EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo("testst"));

            document = new TextDocument(text);
            document.Delete(new TextPosition(1, 2), new TextPosition(1, 4), EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo("test\r\nte"));

            document = new TextDocument(text);
            document.Delete(new TextPosition(0, 0), new TextPosition(1, 4), EditingDirection.Forward);
            Assert.That(document.Text, Is.EqualTo(""));
        }

        [Test]
        public void TestLines() {
            var text = "This is the first line.\r\nThis is the second line.\r\n";
            var document = new TextDocument(text);

            Assert.That(document.Lines, Has.Count.EqualTo(3));

            Assert.That(document.Lines[0].Text, Is.EqualTo("This is the first line."));
            Assert.That(document.Lines[0].LineNumber, Is.EqualTo(0));
            Assert.That(document.Lines[0].Length, Is.EqualTo(23));
            Assert.That(document.Lines[0].Offset, Is.EqualTo(0));

            Assert.That(document.Lines[1].Text, Is.EqualTo("This is the second line."));
            Assert.That(document.Lines[1].LineNumber, Is.EqualTo(1));
            Assert.That(document.Lines[1].Length, Is.EqualTo(24));
            Assert.That(document.Lines[1].Offset, Is.EqualTo(25));

            Assert.That(document.Lines[2].Text, Is.EqualTo(""));
            Assert.That(document.Lines[2].LineNumber, Is.EqualTo(2));
            Assert.That(document.Lines[2].Length, Is.EqualTo(0));
            Assert.That(document.Lines[2].Offset, Is.EqualTo(51));

            document.Replace(new TextPosition(2, 0), new TextPosition(2, 0), new TextInput("test"));
            Assert.That(document.Lines[2].Text, Is.EqualTo("test"));
            Assert.That(document.Lines[2].LineNumber, Is.EqualTo(2));
            Assert.That(document.Lines[2].Length, Is.EqualTo(4));
            Assert.That(document.Lines[2].Offset, Is.EqualTo(51));

            document.Delete(new TextPosition(2, 0), new TextPosition(2, 4), EditingDirection.Forward);
            Assert.That(document.Lines[2].Text, Is.EqualTo(""));
            Assert.That(document.Lines[2].LineNumber, Is.EqualTo(2));
            Assert.That(document.Lines[2].Length, Is.EqualTo(0));
            Assert.That(document.Lines[2].Offset, Is.EqualTo(51));
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