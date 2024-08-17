using MEdit_wpf.Document;

namespace MEdit_wpf
{
    public class TextInput
    {
        public TextInput(string input)
        {
            Value = string.IsNullOrEmpty(input) ? string.Empty : ConvertEol(input);
        }

        public string Value { get; private set; }

        public int Length => Value.Length;

        private string ConvertEol(string input)
            => (input == "\r" || input == "\n") ? TextDocument.EndOfLine : input.Replace("\n", TextDocument.EndOfLine).Replace("\r\r", "\r");
    }
}
