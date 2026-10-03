namespace WordApp;

using Spectre;
using Spectre.Console;

// https://spectreconsole.net/

internal class Program
{
    static void Main(string[] args)
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

        AnsiConsole.Markup($"[green]Loaded {words.Count} words[/]");
    }
}
