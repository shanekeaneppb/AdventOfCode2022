using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Day15
{
    public class Beacon
    {
        public int X;
        public int Y;

        public Beacon(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";
    }
}