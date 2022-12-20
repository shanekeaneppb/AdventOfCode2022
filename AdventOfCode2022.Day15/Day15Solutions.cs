using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day15
{
    public partial class Day15Solutions
    {
        public static void Part1()
        {

            HashSet<Sensor> sensors = new();
            HashSet<Beacon> beacons = new();
            Regex regex = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
            Match match;
            Sensor sensor;
            Beacon beacon;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day15/" + "test.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    match = regex.Match(line);
                    if (!match.Success)
                    {
                        continue;
                    }
                    beacon = new Beacon(Convert.ToInt32(match.Groups[3].Value), Convert.ToInt32(match.Groups[4].Value));
                    sensor = new Sensor(Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[2].Value), beacon);
                    sensors.Add(sensor);
                    beacons.Add(beacon);
                }
            }
            //int distanceToRow10;
            //int distanceRemaining;
            //int maxDistanceLeft = 0;
            //int maxDistanceRight = 0;
            //foreach (var s in sensors)
            //{
            //    distanceToRow10 = Math.Abs(s.Y - 10);
            //    distanceRemaining = s.DistToBeacon - distanceToRow10;
            //    if (distanceRemaining < 0)
            //        continue;
            //    if(s.X - distanceRemaining < maxDistanceLeft)
            //        maxDistanceLeft = s.X - distanceRemaining;
            //    if(s.X + distanceRemaining > maxDistanceRight) 
            //        maxDistanceRight = s.X + distanceRemaining;
            //}
            //Console.WriteLine($"left = {maxDistanceLeft}");
            //Console.WriteLine($"right = {maxDistanceRight}");
            int maxDistanceLeft = 0;
            int maxDistanceRight = 0;
            foreach (var s in sensors)
            {
                if (s.X - s.DistToBeacon < maxDistanceLeft)
                    maxDistanceLeft = s.X - s.DistToBeacon;
                if (s.X + s.DistToBeacon > maxDistanceRight)
                    maxDistanceRight = s.X + s.DistToBeacon;
            }
            Console.WriteLine($"left = {maxDistanceLeft}");
            Console.WriteLine($"right = {maxDistanceRight}");
            //int row = 10, takenCount= 0;
            //HashSet<Point> takenPositions = new();
            //HashSet<Point> sensors = new();
            //HashSet<Point> beacons = new();
            //Regex regex = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
            //Match match;
            //Point sensor, beacon;
            //int minX = 0, maxX = 0, minY = 0, maxY = 0;
            //using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day15/" + "test.txt"))
            //{
            //    string line;
            //    while((line = reader.ReadLine()) != null)
            //    {
            //        match = regex.Match(line);
            //        if(!match.Success)
            //        {
            //            continue;
            //        }
            //        sensor = new Point(Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[2].Value));
            //        beacon = new Point(Convert.ToInt32(match.Groups[3].Value), Convert.ToInt32(match.Groups[4].Value));
            //        sensors.Add(sensor);
            //        beacons.Add(beacon);
            //        takenPositions.Add(sensor);
            //        takenPositions.Add(beacon);
            //        takenPositions.UnionWith(GetTakenPoints(sensor, beacon));
            //        (minX, minY, maxX, maxY) = SetNewMinMax(sensor, beacon, minX, minY, maxX, maxY);
            //    }
            //}
            //minX = takenPositions.Select(point => point.X).Min();
            //minY = takenPositions.Select(point => point.Y).Min();
            //maxX = takenPositions.Select(point => point.X).Max();
            //maxY = takenPositions.Select(point => point.Y).Max();

            //PrintResults(sensors, beacons, takenPositions, minX, maxX, minY, maxY);
            //Console.WriteLine($"minX = {minX}");
            //Console.WriteLine($"maxX = {maxX}");
            //Console.WriteLine($"minY = {minY}");
            //Console.WriteLine($"maxY = {maxY}");

            //Point temp;
            //for (int i = minX; i < maxX; i++)
            //{
            //    temp = new Point(i, row);
            //    if (sensors.Contains(temp) || beacons.Contains(temp))
            //        continue;
            //    takenCount += takenPositions.Contains(temp) ? 1 : 0;
            //}
            //Console.WriteLine($"Day 15, Part 1 Solution: {takenCount}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 15, Part 1 Solution: ");
        }

    //    private static HashSet<Point> GetTakenPoints(Point sensor, Point beacon)
    //    {
    //        int distanceToBeacon = sensor.DistanceTo(beacon);
    //        HashSet<Point> takenPositions = new();
    //        for(int x = 0; x < distanceToBeacon; x++)
    //        {
    //            for(int y = 0; y < distanceToBeacon - x; y++)
    //            {
    //                takenPositions.Add(new Point(sensor.X + x, sensor.Y + y));
    //                takenPositions.Add(new Point(sensor.X + x, sensor.Y - y));
    //                takenPositions.Add(new Point(sensor.X - x, sensor.Y + y));
    //                takenPositions.Add(new Point(sensor.X - x, sensor.Y - y));
    //            }
    //        }
    //        return takenPositions;
    //    }

    //    private static void PrintResults(HashSet<Point> sensors, HashSet<Point> beacons, HashSet<Point> takenPositions, 
    //                                        int minX, int maxX, int minY, int maxY)
    //    {
    //        Point temp;
    //        int y, x;
    //        Console.Write("   ");
    //        minX = -4;
    //        maxX = 27;
    //        minY = -2;
    //        maxY = 23;
    //        for (x = minX; x < maxX; x++)
    //        {
    //            if(x == minX)
    //                Console.Write(Math.Abs(x));
    //            else if(x%5 == 0)
    //            {
    //                if(x == 0)
    //                    Console.Write("0");
    //                else
    //                    Console.Write("5");
    //            }
    //            else
    //                Console.Write(" ");
    //        }
    //        Console.WriteLine();
    //        for (y = minY; y < maxY; y++)
    //        {
    //            Console.Write($"{Math.Abs(y):D2} ");
    //            for (x = minX; x < maxX; x++)
    //            {
    //                temp = new Point(x, y);
    //                if (sensors.Contains(temp))
    //                    Console.Write("S");
    //                else if (beacons.Contains(temp))
    //                    Console.Write("B");
    //                else if (takenPositions.Contains(new Point(x, y)))
    //                    Console.Write("#");
    //                else
    //                    Console.Write(".");
    //            }
    //            Console.WriteLine();
    //        }
    //    }

    //    public static (int, int, int, int) SetNewMinMax(Point sensor, Point beacon, int minX, int minY, int maxX, int maxY)
    //    {
    //        int newMinX = minX, newMinY = minY, newMaxX = maxX, newMaxY = maxY;

    //        if(sensor.X < minX)
    //            newMinX = sensor.X;
    //        if(beacon.X < minX)
    //            newMinX = beacon.X;

    //        if (sensor.Y < minY)
    //            newMinY = sensor.Y;
    //        if (beacon.Y < minY)
    //            newMinY = beacon.Y;

    //        if (sensor.X > maxX)
    //            newMaxX = sensor.X;
    //        if (beacon.X > maxX)
    //            newMaxX = beacon.X;

    //        if (sensor.Y > maxY)
    //            newMaxY = sensor.Y;
    //        if (beacon.Y > maxY)
    //            newMaxY = beacon.Y;


    //        return (newMinX, newMinY, newMaxX, newMaxY);
    //    }

    }
}