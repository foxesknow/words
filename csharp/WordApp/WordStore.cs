using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal sealed class WordStore : IReadOnlyWordStore
{
    private const byte AsciiLowerA = (byte)'a';
    private const byte AsciiLowerZ = (byte)'z';

    private const byte AsciiUpperA = (byte)'A';
    private const byte AsciiUpperZ = (byte)'Z';

    private readonly List<Word>[] m_Indexes = new List<Word>[26];
    private int m_Count;

    public WordStore()
    {
        for(var i = 0; i < m_Indexes.Length; i++)
        {
            m_Indexes[i] = new List<Word>(500);
        }
    }

    public void Add(Word word)
    {
        var index = GetIndex(word.Span[0]);
        index.Add(word);
        m_Count++;
    }

    public int Count
    {
        get{return m_Count;}
    }

    public IReadOnlyList<Word> this[int index]
    {
        get
        {
            if((uint)index < (uint)m_Indexes.Length)
            {
                return m_Indexes[index];
            }

            throw new IndexOutOfRangeException();
        }
    }

    public IReadOnlyList<Word> this[char c]
    {
        get
        {
            if(char.IsAsciiLetter(c)) return GetIndex((byte)c);
            throw new InvalidOperationException($"not a valid character: {c}");
        }
    }

    public IEnumerable<IReadOnlyList<Word>> Indexes()
    {
        for(int i = 0, length = m_Indexes.Length; i < length; i++)
        {
            yield return m_Indexes[i];
        }
    }

    private List<Word> GetIndex(byte asciiValue)
    {
        if(asciiValue >= AsciiUpperA && asciiValue <= AsciiUpperZ)
        {
            asciiValue = (byte)(AsciiLowerA + (asciiValue - AsciiUpperA));
        }

        if(asciiValue >= AsciiLowerA && asciiValue <= AsciiLowerZ)
        {
            return m_Indexes[asciiValue - AsciiLowerA];
        }

        throw new IndexOutOfRangeException();
    }

    public IEnumerator<Word> GetEnumerator()
    {
        for(int i = 0, length = m_Indexes.Length; i < length; i++)
        {
            var index = m_Indexes[i];
            foreach(var word in index)
            {
                yield return word;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
