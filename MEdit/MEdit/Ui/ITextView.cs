using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEdit {
    public interface ITextView {

        TextDocument Document { get; }
    }
}
