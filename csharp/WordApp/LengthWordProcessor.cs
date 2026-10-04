using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal class LengthWordProcessor(IReadOnlyWordStore wordStore) : WordProcessorBase, IWordProcessor
{
    public ValueTask Process()
    {
        var (elapsed, counts) = Measure((new int[100], wordStore), static state =>
        {
            var counts = state.Item1;
            var wordStore = state.wordStore;

            foreach(var word in wordStore)
            {
                var length = word.Length;
                counts[length]++;
            }

            return counts;
        });

        var chart = new BarChart();
        chart.Label= new("Word lengths");

        double totalWords = wordStore.Count;

        foreach(var (i, color) in IWordProcessor.ColorCyle().Index().Take(counts.Length))
        {
            var count = counts[i];
            if(count == 0) continue;

            var percentage = (count / totalWords) * 100;
            var label = $"{i,2}|{percentage,5:F2}%|";
            chart.AddItem(label, count, color);
        }

        AnsiConsole.Write(chart);
        ReportTimeTaken(elapsed);

        return default;
    }
}
