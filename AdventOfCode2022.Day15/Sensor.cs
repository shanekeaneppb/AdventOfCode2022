namespace AdventOfCode2022.Day15
{
    public class Sensor
    {
        public int X;
        public int Y;
        public Beacon beacon;
        public int DistToBeacon;

        public Sensor(int x, int y, Beacon beacon)
        {
            X = x;
            Y = y;
            this.beacon = beacon;
            DistToBeacon = DistanceToBeacon(this.beacon);

        }
        public int DistanceToBeacon(Beacon beacon)
        {
            return Math.Abs(this.X - beacon.X) + Math.Abs(this.Y - beacon.Y);
        }
        public override string ToString() => $"({X}, {Y})";
    }
}