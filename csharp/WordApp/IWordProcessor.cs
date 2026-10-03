using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal interface IWordProcessor
{
    ValueTask Process();

    public static IEnumerable<(int Index, char Letter, Color Color)> ColorsByLetter()
    {
        Color[] colors = [Color.Green, Color.Blue, Color.Yellow, Color.Orange1, Color.Aquamarine1, Color.DeepPink1];

        foreach(var (i, color) in colors.Forever().Take(26).Index())
        {
            char c = (char)('A' + i);
            yield return (i, c, color);
        }
    }
}
