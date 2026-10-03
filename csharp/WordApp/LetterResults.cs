using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordApp;

[InlineArray(26)]
internal struct LetterResults<T>
{
    private T element;
}
