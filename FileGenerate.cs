using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TiVo
{
    public class FileGenerate
    {
        Random rnd = new Random();
        readonly int _lineCount;
        string[] _words;

        public FileGenerate(int lineCount)
        {
            _lineCount = lineCount;

            _words = Enumerable.Range(0, lineCount / 10)
                .Select(i => new string(Enumerable.Range(0, rnd.Next(20, 200)).Select(t => (char)rnd.Next('A', 'Z')).ToArray())).ToArray();
        }

        public string SaveFile()
        {
            var fn = $"L{_lineCount}.txt";

            using var fstream = new StreamWriter(fn);
            for (int i = 0; i < _lineCount; i++)
            {
                fstream.WriteLine($"{GenerateNumber()}. {GenerateString()}");
            }

            return fn;
        }

        private string GenerateString() => _words[rnd.Next(0, _words.Length)];

        private int GenerateNumber() => rnd.Next(0, 10000);
    }
}
