using System;

namespace TiVo
{
    public class Line : IComparable<Line>
    {
        private readonly string _originalString;
        private readonly int _dot;

        public readonly int Number;
        public ReadOnlySpan<char> Word => _originalString.AsSpan(_dot);

        public Line(string s)
        {
            _originalString = s;
            _dot = s.IndexOf('.');
            Number = int.Parse(s.AsSpan(0, _dot));
        }

        public int CompareTo(Line? other)
        {
            if (other == null) return 1;
            int result = Number.CompareTo(other.Number);
            if (result != 0)
                return result;
            return Word.SequenceCompareTo(other.Word);
        }

        public override string ToString()
        {
            return _originalString;
        }
    }
}
