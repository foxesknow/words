using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal interface IReadOnlyWordStore : IEnumerable<Word>
{
    public int Count{get;}
    public IReadOnlyList<Word> this[int index]{get;}
    public IReadOnlyList<Word> this[char c]{get;}

    public IEnumerable<IReadOnlyList<Word>> Indexes();
}
