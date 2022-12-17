namespace AdventOfCode2022.Day12
{
    public class Node : IComparable<Node>
    {
        public struct Point
        {
            public int X;
            public int Y;
        }

        public int Value;
        public Point Position;
        public List<Node> Neighbours;
        public Node CameFrom;
        public int FromStart;
        public int FromEnd;
        public int FromSum;

        public Node()
        {
            Neighbours = new List<Node>();
            Position = new Point();
        }

        public int CompareTo(Node? other)
        {
            if (FromSum < other.FromSum)
                return -1;
            else if (other.FromSum < FromSum)
                return 1;
            else if (FromEnd < other.FromEnd)
                return -1;
            else if (other.FromEnd < FromEnd)
                return 1;
            else if (FromStart < other.FromStart)
                return -1;
            else if (other.FromStart < FromStart)
                return 1;
            return 0;
        }
    }
}
