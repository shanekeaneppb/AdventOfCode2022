namespace AdventOfCode2022.Day23
{
    public class CounterDict
    {
        private Dictionary<Point, int> _dict = new();

        public void Add(Point key)
        {
            if(_dict.ContainsKey(key))
                _dict[key] += 1;
            else
            {
                _dict.Add(key, 1);
            }
        }

        public int this[Point p]
        {
            get { return _dict[p]; }
        }
    }
}