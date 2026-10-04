using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal static class UsefulExtensions
{
    extension<T>(IEnumerable<T> self)
    {
        public IEnumerable<T> Forever()
        {
            while(true)
            {
                foreach(var value in self)
                {
                    yield return value;
                }
            }
        }
    }

    extension(IReadOnlyWordStore self)
    {
        public LetterResults<T> Process<T>(Func<int, IReadOnlyList<Word>, T> function)
        {
            var results = new LetterResults<T>();

            for(int i = 0; i < 26; i++)
            {
                var index = self[i];
                results[i] = function(i, index);
            }

            return results;
        }

        public LetterResults<T> Process<T, S>(S state, Func<S, int, IReadOnlyList<Word>, T> function)
        {
            var results = new LetterResults<T>();

            for(int i = 0; i < 26; i++)
            {
                var index = self[i];
                results[i] = function(state, i, index);
            }

            return results;
        }
    }
}
