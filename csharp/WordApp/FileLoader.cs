using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordApp;

internal class FileLoader
{
    public static void Load(string filename, Action<Word> wordHandler)
    {
        var fileContents = File.ReadAllBytes(filename);
        
        ReadOnlySpan<byte> separator = [(byte)'\r', (byte)'\n'];
        foreach(var range in fileContents.AsSpan().Split(separator))
        {
            var start = range.Start.Value;
            var end = range.End.Value;

            var bytes = new Memory<byte>(fileContents, start, end-start);
            if(bytes.Length == 0) continue;

            var word = new Word(bytes);
            wordHandler(word);
        }
    }
}
