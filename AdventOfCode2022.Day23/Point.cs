using System.ComponentModel;

namespace AdventOfCode2022.Day23
{
    public class Point
    {
        public int Row;
        public int Column;

        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }

        public override bool Equals(object? obj)
        {
            try
            {
                Point p = obj as Point;
                return ((Row == p.Row) && (Column == p.Column));
            }
            catch { return false; }
        }
        public override int GetHashCode()
        {
            return (Row, Column).GetHashCode();
        }
    }
}