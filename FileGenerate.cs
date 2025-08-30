using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TiVo
{
    public static class FileGenerate
    {
        private readonly static Random rnd = new();

        public static void MakeFile(int lineCount, TextWriter stream)
        {
            for (int i = 0; i < lineCount; i++)
            {
                stream.WriteLine($"{GenerateNumber()}. {GenerateString()}");
            }
        }

        public static string MakeFile(int lineCount)
        {
            var fn = $"L{lineCount}.txt";
            using var fstream = new StreamWriter(fn);

            MakeFile(lineCount, fstream);

            return fn;
        }

        private static string GenerateString() => new([.. Enumerable.Range(0, rnd.Next(20, 101)).Select(t => (char)rnd.Next('A', 'Z'))]);

        private static int GenerateNumber() => rnd.Next(0, 10000);
    }
}
