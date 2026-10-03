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

        foreach(var (i, c, color) in IWordProcessor.ColorsByLetter())
        {
            var words = wordStore[i];
            var percentage = (words.Count / count) * 100;

            chart.AddItem(c.ToString(), percentage, color);
        }

        AnsiConsole.Write(chart);
        AnsiConsole.WriteLine();

        return default;
    }
}
