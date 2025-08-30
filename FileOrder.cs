using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TiVo
{
    public static class FileOrder
    {
        public static void Order(int partSize, TextWriter writer, StreamReader reader)
        {
            var fileParts = SplitFile(partSize, reader);

            var comparer = Comparer<Line>.Create((a, b) => a.CompareTo(b));
            var queue = new PriorityQueue<FilePart, Line>(comparer);

            foreach (var fn in fileParts)
            {
                var fp = new FilePart(fn);
                if (!fp.Next()) continue;
                var line = fp.CurrentLine;
                if (line == null) continue;
                queue.Enqueue(fp, line);
            }

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var fp, out var currentLine);
                if (fp == null || currentLine == null) continue;
                writer.WriteLine(currentLine);
                if (!fp.Next())
                {
                    fp.Dispose();
                    continue;
                }
                var line = fp.CurrentLine;
                if (line == null)
                {
                    fp.Dispose();
                    continue;
                }
                queue.Enqueue(fp, line);
            }
        }

        private static List<string> SplitFile(int partSize, StreamReader reader)
        {
            List<string> files = [];
            int n = 0;

            while (!reader.EndOfStream)
            {
                List<Line> lines = [];
                for (int i = 0; i < partSize; i++)
                {
                    if (reader.EndOfStream)
                        break;
                    var s = reader.ReadLine();
                    if (!string.IsNullOrEmpty(s))
                        lines.Add(new Line(s));
                }
                lines.Sort();
                var fn = $"P{++n}.txt";
                files.Add(fn);

                using var writer = new StreamWriter(fn);
                foreach (var line in lines)
                    writer.WriteLine(line);
            }

            return files;
        }
    }
}
