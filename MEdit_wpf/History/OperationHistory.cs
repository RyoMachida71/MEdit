namespace History {
    public class OperationHistory {
        Stack<IUndoable> _undos;

        public OperationHistory()
        {
            _undos = new Stack<IUndoable>();
        }

        public void Push(IUndoable operation) {
            _undos.Push(operation);
        }

        public void Undo() {
            if (_undos.Count == 0) return;
            var lastOperation = _undos.Pop();
            lastOperation.Undo();
        }
    }
}
