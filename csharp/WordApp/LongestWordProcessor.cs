using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordApp;

internal class LongestWordProcessor(IReadOnlyWordStore wordStore) : WordProcessorBase, IWordProcessor
{
    public ValueTask Process()
    {
        var table = new Table();
        table.Title = new("Longest word for each letter");

        table.AddColumn("Letter");
        table.AddColumn("Length");

        // It's quicker without the parallel!
        var (elapsed, lengths) = Measure(wordStore, static wordStore => 
                                 {
                                    return wordStore.Indexes()
                                                    //.AsParallel()
                                                    .Select((words, index) => (index, count: words.Max(static word => word.Length)))
                                                    .OrderBy(data => data.index)
                                                    .ToList();
                                 });

        foreach(var (index, count) in lengths)
        {
            char c = (char)('A' + index);
            table.AddRow(c.ToString(), count.ToString());
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine($"Time taken = {elapsed.TotalMilliseconds} ms");

        return default;
    }
}
