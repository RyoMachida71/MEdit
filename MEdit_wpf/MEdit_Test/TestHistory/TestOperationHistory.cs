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

            history.Push(mock.Object);
            history.Undo();

            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation."));
        }

        [Test]
        public void TestUndoMultipleTimes() {
            var sb = new StringBuilder();
            var counter = 0;
            var history = new OperationHistory();
            var mock = new Mock<IUndoable>();
            mock.Setup(x => x.Undo()).Callback(() => sb.Append($"Undo Operation{++counter}."));

            history.Push(mock.Object);
            history.Push(mock.Object);
            history.Push(mock.Object);
            history.Undo();
            history.Undo();

            Assert.That(sb.ToString(), Is.EqualTo("Undo Operation1.Undo Operation2."));
        }

        [Test]
        public void TestUndoWithoutAnyOperations() {
            // don't throw any exceptions
            new OperationHistory().Undo();
        }
    }
}
