namespace History {
    public class OperationHistory : IUndoable {
        Stack<IUndoable> _undos;
        Stack<IUndoable> _redos;

        public OperationHistory()
        {
            _undos = new Stack<IUndoable>();
            _redos = new Stack<IUndoable>();
        }

        public void Push(IUndoable operation) {
            _undos.Push(operation);
            _redos.Clear();
        }

        public void Undo() {
            if (_undos.Count == 0) return;
            var lastOperation = _undos.Pop();
            lastOperation.Undo();
            _redos.Push(lastOperation);
        }

        public void Redo() {
            if (_redos.Count == 0) return;
            var lastOperation =_redos.Pop();
            lastOperation.Redo();
            _undos.Push(lastOperation);
        }

        public void Clear() {
            _undos.Clear();
            _redos.Clear();
        }
    }
}
