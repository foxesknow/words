namespace WordApp;

internal class Program
{
    static void Main(string[] args)
    {
        var filename = "words_alpha.txt";
        var words = new List<Word>(100_000);

        FileLoader.Load(filename, word =>
        {
            words.Add(word);
        });
    }
}
