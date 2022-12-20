using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day15
{
    public partial class Day15Solutions
    {
        public static void Part1()
        {
            HashSet<Sensor> sensors;
            HashSet<Point> beacons;
            (sensors, beacons) = LoadSensorsAndBeacons("input.txt");

            int row = 2000000;
            int maxDistanceLeft, maxDistanceRight;
            (maxDistanceLeft, maxDistanceRight) = GetRowYSpan(sensors, row);
            Console.WriteLine(maxDistanceLeft + " " + maxDistanceRight);
            int numPositionsCannotOccupy = 0;
            for(int i = maxDistanceLeft; i <= maxDistanceRight; i++)
            {
                if (beacons.Contains(new Point(i, row)))
                    continue;
                foreach(Sensor sensor in sensors)
                {
                    if(sensor.IsInRange(new Point(i, row)))
                    {
                        numPositionsCannotOccupy++;
                        break;
                    }
                }
            }
            Console.WriteLine(numPositionsCannotOccupy);
            
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 15, Part 1 Solution: ");
        }

        public static (HashSet<Sensor> sensors, HashSet<Point> beacons) LoadSensorsAndBeacons(string file)
        {
            HashSet<Sensor> sensors = new();
            HashSet<Point> beacons = new();
            Regex regex = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
            Match match;
            Sensor sensor;
            Point beacon;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day15/" + file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    match = regex.Match(line);
                    if (!match.Success)
                    {
                        continue;
                    }
                    beacon = new Point(Convert.ToInt32(match.Groups[3].Value), Convert.ToInt32(match.Groups[4].Value));
                    sensor = new Sensor(Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[2].Value), beacon);
                    sensors.Add(sensor);
                    beacons.Add(beacon);
                }
            }
            return (sensors, beacons);
        }
        public static (int maxDistanceLeft, int maxDistanceRight) GetRowYSpan(HashSet<Sensor> sensors, int y)
        {
            int maxDistanceLeft = 0;
            int maxDistanceRight = 0;
            int distanceToRowY, distanceRemaining;
            foreach (var sensor in sensors)
            {
                distanceToRowY = Math.Abs(sensor.Y - y);
                if (distanceToRowY > sensor.DistToBeacon)
                    continue;
                distanceRemaining = sensor.DistToBeacon - distanceToRowY;
                if (sensor.X - distanceRemaining < maxDistanceLeft)
                    maxDistanceLeft = sensor.X - distanceRemaining;
                if (sensor.X + distanceRemaining > maxDistanceRight)
                    maxDistanceRight = sensor.X + distanceRemaining;
            }

            return (maxDistanceLeft, maxDistanceRight);
        }




    }
}