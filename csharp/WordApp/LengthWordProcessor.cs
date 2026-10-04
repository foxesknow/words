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

        var table = new Table();
        table.Title = new("Word lengths");

        table.AddColumn("Length");
        table.AddColumn("Count");
        table.AddColumn("Percentage");

        double totalWords = wordStore.Count;

        for(int i = 0, length = counts.Length; i < length; i++)
        {
            var count = counts[i];
            if(count == 0) continue;

            var percentage = (count / totalWords) * 100;
            table.AddRow(i.ToString(), count.ToString(), percentage.ToString("F2"));
        }

        AnsiConsole.Write(table);
        ReportTimeTaken(elapsed);

        return default;
    }
}
