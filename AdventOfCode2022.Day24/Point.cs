using System.ComponentModel;

namespace AdventOfCode2022.Day24
{
    public enum Direction
    {
        N, S, E, W, None
    }
    public class Point
    {
        public int Row;
        public int Column;
        public Direction Direction;

        public Point(int row, int column)
        {
            Row = row;
            Column = column;
            Direction = Direction.None;
        }
        public Point(int row, int column, char direction)
        {
            Row = row;
            Column = column;
            Direction = GetDirection(direction);
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
            return (Row, Column, Direction).GetHashCode();
        }

        private Direction GetDirection(char c)
        {
            switch (c)
            {
                case '>':
                    return Direction.E;
                case '<':
                    return Direction.W;
                case 'v':
                    return Direction.S;
                case '^':
                    return Direction.N;
            }
            return Direction.None;
        }
    }
}