using System.Text;
using History;
using Moq;

namespace MEdit_Test.TestHistory {
    public class TestOperationHistory {
        [Test]
        public void TestUndo() {
            var sb = new StringBuilder();
            var history = new OperationHistory();
            var mock = new Mock<IUndoable>();
            mock.Setup(x => x.Undo()).Callback(() => sb.Append("Undo Operation."));
            mock.Setup(x => x.Redo()).Callback(() => sb.Append("Redo Operation."));

            history.Push(mock.Object);
            history.Undo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation."));

            history.Redo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation.Redo Operation."));
        }

        [Test]
        public void TestUndoMultipleTimes() {
            var sb = new StringBuilder();
            var undoCounter = 0;
            var redoCounter = 0;
            var history = new OperationHistory();
            var mock = new Mock<IUndoable>();
            mock.Setup(x => x.Undo()).Callback(() => sb.Append($"Undo Operation{++undoCounter}."));
            mock.Setup(x => x.Redo()).Callback(() => sb.Append($"Redo Operation{++redoCounter}."));

            history.Push(mock.Object);
            history.Push(mock.Object);
            history.Push(mock.Object);
            history.Undo();
            history.Undo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation1.Undo Operation2."));

            history.Redo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation1.Undo Operation2.Redo Operation1."));

            history.Undo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation1.Undo Operation2.Redo Operation1.Undo Operation3."));

            history.Redo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation1.Undo Operation2.Redo Operation1.Undo Operation3.Redo Operation2."));
        }

        [Test]
        public void TestRedosClearAfterSomeOperation() {
            var sb = new StringBuilder();
            var history = new OperationHistory();
            var mock = new Mock<IUndoable>();
            mock.Setup(x => x.Undo()).Callback(() => sb.Append("Undo Operation."));
            mock.Setup(x => x.Redo()).Callback(() => sb.Append("Redo Operation."));

            history.Push(mock.Object);
            history.Undo();
            history.Push(mock.Object);
            history.Redo();
            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation."));
        }

        [Test]
        public void TestHistoryWithoutAnyOperations() {
            var history = new OperationHistory();
            // don't throw any exceptions
            history.Undo();
            history.Redo();
        }

        [Test]
        public void TestClear() {
            var sb = new StringBuilder();
            var history = new OperationHistory();
            var mock = new Mock<IUndoable>();
            mock.Setup(x => x.Undo()).Callback(() => sb.Append("Undo Operation."));
            mock.Setup(x => x.Redo()).Callback(() => sb.Append("Redo Operation."));

            history.Push(mock.Object);
            history.Clear();

            history.Undo();
            history.Redo();
            Assert.That(sb.ToString(), Is.EqualTo(string.Empty));
        }
    }
}
