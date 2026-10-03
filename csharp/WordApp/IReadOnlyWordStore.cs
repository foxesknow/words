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

    public LetterResults<T> Process<T>(Func<int, IReadOnlyList<Word>, T> function);
    public LetterResults<T> Process<T, S>(S state, Func<S, int, IReadOnlyList<Word>, T> function);
}
