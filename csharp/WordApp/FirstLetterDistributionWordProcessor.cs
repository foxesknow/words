using System;
using System.Collections.Generic;
using System.Text;
using Spectre;
using Spectre.Console;

namespace WordApp;

internal class FirstLetterDistributionWordProcessor(IReadOnlyWordStore wordStore) : IWordProcessor
{
    public ValueTask Process()
    {
        double count = wordStore.Count;

        var chart = new BarChart();
        chart.Label = new("Distribution of words");
        chart.UseValueFormatter(d => d.ToString("F2") + "%");

        var results = wordStore.Process(count, static (count, _, words) => (words.Count / count) * 100);

        foreach(var (i, c, color) in IWordProcessor.ColorsByLetter())
        {
            var percentage = results[i];
            chart.AddItem(c.ToString(), percentage, color);
        }

        AnsiConsole.Write(chart);
        AnsiConsole.WriteLine();

        return default;
    }
}
