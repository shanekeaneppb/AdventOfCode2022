using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022.Day17
{

    public class Day17Solutions
    {
        public static int NumRockShapes = 5;
        public static int LeftSpawn = 2;
        public static int VerticalSpawn = 3 + 1; // counting the floor as index 0, so add 1 to actual spawn of 3
        public static int CaveWidth = 7;

        public static void Part1()
        {

            char[] jetArray = LoadJetPattern("test.txt");
            int maxNumRocks = 12;

            //int lcm = NumRockShapes * jetArray.Length;
            //int numOfPeriods = maxNumRocks / lcm;
            //int remainingRocks = maxNumRocks % lcm;

            //int height;

            //int periodHeight = DropRocks(jetArray, lcm);

            //height = periodHeight * numOfPeriods;

            //height += DropRocks(jetArray, remainingRocks);

            int height = DropRocks(jetArray, maxNumRocks);

            Console.WriteLine($"Day 17, Part 1 Solution: {height}");
        }
        public static void Part2()
        {
            //char[] jetArray = LoadJetPattern("test.txt");
            //int maxNumRocks = NumRockShapes * jetArray.Length;
            //int height = DropRocks(jetArray, maxNumRocks);

            //Console.WriteLine($"Day 17, Part 2 Solution: {height}");
        }

        public  static char[] LoadJetPattern(string file)
        {
            char[] jetPattern;
            using (StreamReader reader = new(@"../../../../AdventOfCode2022.Day17/" + file))
            {
                jetPattern = reader.ReadLine().Trim().ToArray();
            }
            return jetPattern;
        }

        

        public  static int DropRocks(char[] jetArray, int maxRocks)
        {
            int rockCount = 0, jetIndex = 0, numJetInstructions = jetArray.Length, highestPoint = 0;
            char currentJet;

            HashSet<Point> occupiedPoints = new();

            Rock currentRock = new();

            bool canMoveRock = false;
            while (rockCount < maxRocks)
            {
                if (!canMoveRock)
                {
                    currentRock = Rock.GetNewRock(rockCount, highestPoint);
                    canMoveRock = true;
                }
                currentJet = jetArray[jetIndex % numJetInstructions];
                currentRock.MoveRightLeft(currentJet, occupiedPoints);
                canMoveRock = currentRock.MoveDown(occupiedPoints);
                if (!canMoveRock)
                {
                    rockCount++;
                    occupiedPoints.UnionWith(currentRock.Points);
                    highestPoint = currentRock.GetHighestPoint();
                }
                jetIndex++;
            }
            PrintRocks(occupiedPoints);
            //PrintTopNRows(occupiedPoints, 10);
            return highestPoint;
        }

        public static void PrintRocks(HashSet<Point> occupiedPoints)
        {
            int y, x;
            int n = occupiedPoints.Select(p => p.Y).Max();
            for(y = n; y > 0; y--)
            {
                Console.Write("|");
                for(x = 0; x < CaveWidth; x++)
                {
                    if (occupiedPoints.Contains(new Point(x, y)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("+-------+");
        }

        public static void PrintTopNRows(HashSet<Point> occupiedPoints, int n)
        {
            int y, x;
            int maxY = occupiedPoints.Select(p => p.Y).Max();
            int minY = maxY - n;
            for (y = maxY; y > minY; y--)
            {
                Console.Write("|");
                for (x = 0; x < CaveWidth; x++)
                {
                    if (occupiedPoints.Contains(new Point(x, y)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("+-------+");
        }




    }


}