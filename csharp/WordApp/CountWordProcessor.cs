using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal class CountWordProcessor(IReadOnlyWordStore wordStore) : IWordProcessor
{
    public ValueTask Process()
    {
        var chart = new BarChart();
        chart.Label = new("Number of words for each letter");

        chart.UseValueFormatter(d => d.ToString("F0"));

        var results = wordStore.Process(static (_, words) => words.Count);

        foreach(var (i, c, color) in IWordProcessor.ColorsByLetter())
        {
            var count = results[i];
            chart.AddItem(c.ToString(), count, color);
        }

        AnsiConsole.Write(chart);
        AnsiConsole.WriteLine();

        return default;
    }
}
