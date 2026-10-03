using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WordApp;

internal readonly struct Word : IEquatable<Word>, IComparable<Word>
{
    private readonly ReadOnlyMemory<byte> m_Characters;

    public Word(ReadOnlyMemory<byte> characters)
    {
        m_Characters = characters;
    }

    public ReadOnlySpan<byte> Span
    {
        get{return m_Characters.Span;}
    }

    public int Length
    {
        get{return m_Characters.Length;}
    }

    public int CompareTo(Word other)
    {
        return m_Characters.Span.SequenceCompareTo(other.Span);
    }

    public bool Equals(Word other)
    {
        return m_Characters.Span.SequenceEqual(other.Span);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Word rhs && Equals(rhs);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            uint hash = 2166136261;

            var characters = m_Characters.Span;
            for(int i = 0, length = characters.Length; i < length; i++)
            {
                hash ^= characters[i];
                hash *= 16777619;
            }

            return (int)hash;
        }
    }

    public override string ToString()
    {
        return string.Create(m_Characters.Length, m_Characters, (buffer, state) =>
        {
            var characters = state.Span;
            for(int i = 0, length = buffer.Length; i < length; i++)
            {
                buffer[i] = (char)characters[i];
            }
        });
    }
}
