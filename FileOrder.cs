namespace TiVo
{
    public class FileOrder
    {
        private readonly string _fn;

        public FileOrder(string fn)
        {
            _fn = fn;
        }

        public void Order(int partSize)
        {
            var fileParts = SplitFile(partSize).Select(s => new FilePart(s)).ToList();
            using var writer = new StreamWriter("result.txt");
            while (fileParts.Any())
            {
                var part = fileParts[0];
                writer.WriteLine(part.currentLine);
                if (!part.Next())
                    fileParts.Remove(part);
                ReOrder(fileParts);
            }

        }

        private void ReOrder(List<FilePart> fileParts)
        {
            if (fileParts.Count < 2)
                return;
            if (fileParts[0].currentLine.CompareTo(fileParts[1].currentLine) <= 0)
                return;
            int n = 0;
            for (n = 1; n < fileParts.Count; n++)
            {
                if (fileParts[0].currentLine.CompareTo(fileParts[n].currentLine) > 0)
                {
                    break;
                }
            }
            if (n >= fileParts.Count)
                n = fileParts.Count - 1;

            var tmp = fileParts[0];
            fileParts[0] = fileParts[n];
            fileParts[n] = tmp;
        }

        private string[] SplitFile(int partSize)
        {
            List<string> files = new();
            int n = 0;

            using var reader = new StreamReader(_fn);
            while (!reader.EndOfStream)
            {
                List<Line> lines = new();
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

            return files.ToArray();
        }
    }
}
