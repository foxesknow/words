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

        var keepPrompting = true;
        while(keepPrompting)
        {
            AnsiConsole.MarkupLine($"[red]0. Exit[/]");
            AnsiConsole.MarkupLine($"[green]1. Length breakdown[/]");
            AnsiConsole.MarkupLine($"[green]2. First letter distribution[/]");
            AnsiConsole.MarkupLine($"[green]3. Word count[/]");

            var option = AnsiConsole.Ask<int>("Select option");
            switch(option)
            {
                default:
                    break;

                case 0:
                    keepPrompting = false;
                    break;

                case 1:
                {
                    var processor = new LengthWordProcessor(words);
                    await processor.Process();
                    break;
                }

                case 2:
                {
                    var processor = new FirstLetterDistributionWordProcessor(words);
                    await processor.Process();
                    break;
                }

                case 3:
                {
                    var processor = new CountWordProcessor(words);
                    await processor.Process();
                    break;
                }
            }
        }
    }
}
