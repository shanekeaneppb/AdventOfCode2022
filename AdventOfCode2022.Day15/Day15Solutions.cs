using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day15
{
    public class Day15Solutions
    {
        public class Point
        {
            public int X;
            public int Y;
            private Point _beacon = null;
            private int _distanceToBeacon;

            public Point Beacon 
            {
                get
                {
                    return _beacon;
                }
                set
                {
                    _beacon = value;
                    _distanceToBeacon = this.DistanceTo(_beacon);
                }
            }
            public Point(int x, int y)
            {
                X = x; Y = y;
            }
            public int DistanceTo(Point other)
            {
                return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
            }

            public bool IsInRange(Point other)
            {
                int distanceToOther =  this.DistanceTo(other);

                if (distanceToOther <= _distanceToBeacon)
                    return true;
                return false;
            }

            public override bool Equals(Object other)
            {
                try
                {
                    Point p = (Point)other;
                    return ((this.X == p.X) && (this.Y == p.Y));
                }
                catch
                {
                    return false;
                }
            }
        }
        public static void Part1()
        {
            int row = 10;
            List<Point> sensors = new();
            List<Point> beacons = new();
            Regex regex = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
            Match match;
            Point sensor, beacon;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day15/" + "test.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    match = regex.Match(line);
                    if(!match.Success)
                    {
                        continue;
                    }
                    sensor = new Point(Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[2].Value));
                    beacon = new Point(Convert.ToInt32(match.Groups[3].Value), Convert.ToInt32(match.Groups[4].Value));
                    sensor.Beacon = beacon;
                    sensors.Add(sensor);
                    beacons.Add(beacon);
                }
            }
            Point p;
            int cannotConatinBeacon = 0;
            bool inRange;
            for(int i = -4; i <= 26; i++)
            {
                inRange = false;
                p = new Point(i, row);
                if ((sensors.Contains(p)) || (beacons.Contains(p)))
                {
                    Console.Write("B");
                    continue;
                }
                foreach(var s in sensors)
                {
                    if (s.IsInRange(p))
                    {
                        inRange = true;
                        break;
                    }
                }
                if(inRange)
                    Console.Write("#");
                else
                    Console.Write(".");
                cannotConatinBeacon += inRange ? 1 : 0; 
            }
            Console.WriteLine($"Day 15, Part 1 Solution: {cannotConatinBeacon}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 15, Part 1 Solution: ");
        }

        private static HashSet<Point> GetTakenPoints(Point sensor, Point beacon)
        {
            int distanceToBeacon = sensor.DistanceTo(beacon);
            HashSet<Point> takenPositions = new();
            for(int x = 0; x < distanceToBeacon; x++)
            {
                for(int y = 0; y < distanceToBeacon - x; y++)
                {
                    takenPositions.Add(new Point(sensor.X + x, sensor.Y + y));
                    takenPositions.Add(new Point(sensor.X + x, sensor.Y - y));
                    takenPositions.Add(new Point(sensor.X - x, sensor.Y + y));
                    takenPositions.Add(new Point(sensor.X - x, sensor.Y - y));
                }
            }
            return takenPositions;
        }

        private static void PrintResults(HashSet<Point> sensors, HashSet<Point> beacons, HashSet<Point> takenPositions, 
                                            int minX, int maxX, int minY, int maxY)
        {
            Point temp;
            int y, x;
            Console.Write("   ");
            minX = -4;
            maxX = 27;
            minY = -2;
            maxY = 23;
            for (x = minX; x < maxX; x++)
            {
                if(x == minX)
                    Console.Write(Math.Abs(x));
                else if(x%5 == 0)
                {
                    if(x == 0)
                        Console.Write("0");
                    else
                        Console.Write("5");
                }
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
            for (y = minY; y < maxY; y++)
            {
                Console.Write($"{Math.Abs(y):D2} ");
                for (x = minX; x < maxX; x++)
                {
                    temp = new Point(x, y);
                    if (sensors.Contains(temp))
                        Console.Write("S");
                    else if (beacons.Contains(temp))
                        Console.Write("B");
                    else if (takenPositions.Contains(new Point(x, y)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        public static (int, int, int, int) SetNewMinMax(Point sensor, Point beacon, int minX, int minY, int maxX, int maxY)
        {
            int newMinX = minX, newMinY = minY, newMaxX = maxX, newMaxY = maxY;

            if(sensor.X < minX)
                newMinX = sensor.X;
            if(beacon.X < minX)
                newMinX = beacon.X;

            if (sensor.Y < minY)
                newMinY = sensor.Y;
            if (beacon.Y < minY)
                newMinY = beacon.Y;

            if (sensor.X > maxX)
                newMaxX = sensor.X;
            if (beacon.X > maxX)
                newMaxX = beacon.X;

            if (sensor.Y > maxY)
                newMaxY = sensor.Y;
            if (beacon.Y > maxY)
                newMaxY = beacon.Y;


            return (newMinX, newMinY, newMaxX, newMaxY);
        }

    }
}