using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEdit_wpf
{
    public class TextInput
    {
        public TextInput(string input)
        {
            if (string.IsNullOrEmpty(input)) Value = "";
            else if(input == "\r" || input == "\n") Value = TextDocument.EndOfLine;
            else Value = input;
        }

        public string Value { get; private set; }

        public int Length => Value.Length;
    }
}
