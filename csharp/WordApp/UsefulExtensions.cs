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
}
