namespace WordApp;

using Spectre;
using Spectre.Console;

// https://spectreconsole.net/

internal class Program
{
    static async Task Main(string[] args)
    {
        var filename = "words_alpha.txt";
        var words = new WordStore();

        AnsiConsole.Status().Start("Loading words", _ =>
        {      
            FileLoader.Load(filename, word =>
            {
                words.Add(word);
            });
        });

        AnsiConsole.MarkupLine($"[green]Loaded {words.Count} words[/]");

        var processor = new FirstLetterDistributionWordProcessor(words);
        await processor.Process();
    }
}
