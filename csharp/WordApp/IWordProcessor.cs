using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal interface IWordProcessor
{
    private readonly static Color[] s_ColorCycle = [Color.Green, Color.Blue, Color.Yellow, Color.Orange1, Color.Aquamarine1, Color.DeepPink1];

    ValueTask Process();

    public static IEnumerable<(int Index, char Letter, Color Color)> ColorsByLetter()
    {
        foreach(var (i, color) in s_ColorCycle.Forever().Take(26).Index())
        {
            char c = (char)('A' + i);
            yield return (i, c, color);
        }
    }

    public static IEnumerable<Color> ColorCyle()
    {
        foreach(var color in s_ColorCycle.Forever())
        {
            yield return color;
        }
    }
}
