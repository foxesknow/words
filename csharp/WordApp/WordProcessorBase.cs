using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WordApp;

internal class WordProcessorBase
{
    protected static (TimeSpan Elapsed, R Result) Measure<T, R>(T state, Func<T, R> function)
    {
        var start = Stopwatch.GetTimestamp();
        var result = function(state);
        var stop = Stopwatch.GetTimestamp();

        var elapsed = Stopwatch.GetElapsedTime(start, stop);
        return (elapsed, result);
    }

    protected static void ReportTimeTaken(TimeSpan elapsed)
    {
        AnsiConsole.WriteLine($"Time taken = {elapsed.TotalMilliseconds} ms");
    }
}
