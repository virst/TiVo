using System;
using System.IO;

namespace TiVo
{
    public class FilePart : IDisposable
    {
        private readonly StreamReader reader;
        public Line? CurrentLine { get; private set; }
        private readonly string _fn;

        public FilePart(string fn)
        {
            _fn = fn;
            reader = new StreamReader(fn);
            if (!Next())
                throw new Exception("broken file.");
        }

        public bool Next()
        {
            if (!reader.EndOfStream)
            {
                var s = reader.ReadLine();
                if (!string.IsNullOrEmpty(s))
                {
                    CurrentLine = new Line(s);
                    return true;
                }
            }
            CurrentLine = null;
            return false;
        }

        public override string? ToString()
        {
            return CurrentLine?.ToString();
        }

        public void Dispose()
        {
            reader.Dispose();
            File.Delete(_fn);
        }
    }
}
