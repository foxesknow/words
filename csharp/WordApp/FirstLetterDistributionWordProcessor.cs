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

        Color[] colors = [Color.Green, Color.Blue, Color.Yellow, Color.Orange1, Color.Aquamarine1, Color.DeepPink1];
        
        var chart = new BarChart();
        chart.UseValueFormatter(d => d.ToString("F2") + "%");

        foreach(var (i, c, color) in IWordProcessor.ColorsByLetter())
        {
            var words = wordStore[i];
            var percentage = (words.Count / count) * 100;

            chart.AddItem(c.ToString(), percentage, color);
        }

        AnsiConsole.Write(chart);

        return default;
    }
}
