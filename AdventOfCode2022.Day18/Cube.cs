namespace AdventOfCode2022.Day18
{
    public struct Cube
    {
        public int X;
        public int Y;
        public int Z;

        public Cube(string coord)
        {
            int[] ints = coord.Split(",").Select(n => Convert.ToInt32(n)).ToArray();
            X = ints[0];
            Y = ints[1];
            Z = ints[2];
        }
    }

}