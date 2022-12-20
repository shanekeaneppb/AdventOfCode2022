namespace AdventOfCode2022.Day15
{
    public struct Sensor
    {
        public int X;
        public int Y;
        public Point Beacon;
        public int DistToBeacon;

        public Sensor(int x, int y, Point beacon)
        {
            X = x;
            Y = y;
            Beacon = beacon;
            DistToBeacon = Math.Abs(X - Beacon.X) + Math.Abs(Y - Beacon.Y);
        }

        public bool IsInRange(Point point) =>  (Math.Abs(this.X - point.X) + Math.Abs(this.Y - point.Y)) <= DistToBeacon;
        
        public override string ToString() => $"({X}, {Y})";
    }
}