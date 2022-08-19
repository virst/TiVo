using System;
using System.IO;

namespace TiVo
{
    public class FilePart
    {
        StreamReader reader;
        public Line currentLine { get; private set; }

        public FilePart(string fn)
        {
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
                    currentLine = new Line(s);
                    return true;
                }
            }
            currentLine = null;
            return false;
        }

        public override string ToString()
        {
            return currentLine?.ToString();
        }
    }
}
