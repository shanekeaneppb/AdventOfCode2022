using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022.Day17
{
    public partial class Day17Solutions
    {
        public static int NumRockShapes = 5;
        public static int LeftSpawn = 2;
        public static int VerticalSpawn = 3 + 1; // counting the floor as index 0, so add 1 to actual spawn of 3
        public static int CaveWidth = 7;

        public static void Part1()
        {

            char[] jetArray = LoadJetPattern("input.txt");
            int maxNumRocks = 2022;

            int height = DropRocks(jetArray, maxNumRocks);

            Console.WriteLine($"Day 17, Part 1 Solution: {height}");
        }
        public static void Part2()
        {
            char[] jetArray = LoadJetPattern("test.txt");
            int maxNumRocks = 2022;

            int height = DropRocks(jetArray, maxNumRocks);

            Console.WriteLine($"Day 17, Part 2 Solution: {height}");
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
            int highestPoint = 0, rockCount = 0, i = 0, numJetInstructions = jetArray.Length;
            char currentJet;

            Dictionary<int, HashSet<int>> rows = new();

            Rock currentRock = new();

            bool canMoveBlock = false;
            while (rockCount < maxRocks)
            {
                //lowestMaxHeight = highestPointsPerColumn.Select(p => p.Y).Min();
                //highestMaxHeigth = highestPointsPerColumn.Select(p => p.Y).Max();

                if (!canMoveBlock)
                {
                    currentRock = Rock.GetNewRock(rockCount, highestPoint);
                    //Console.WriteLine($"Rock number {rockCount}");
                    //PrintRock(currentRock);
                    canMoveBlock = true;
                }
                currentJet = jetArray[i % numJetInstructions];
                currentRock.MoveRightLeft(currentJet, rows);
                canMoveBlock = currentRock.MoveDown(rows);
                if (!canMoveBlock)
                {
                    rockCount++;
                    UpdateRows(currentRock, rows);
                    highestPoint = rows.Select(p => p.Key).Max();
                    //PrintRocks(rows);
                }
                i++;
            }
            //PrintRocks(rows);
            return highestPoint;
        }


        public static void UpdateRows(Rock currentRock, Dictionary<int, HashSet<int>> rows)
        {
            int rowNumber;
            foreach(var point in currentRock.Points)
            {
                rowNumber = point.Y;
                if(rows.ContainsKey(rowNumber))
                    rows[rowNumber].Add(point.X);
                else
                {
                    rows[rowNumber] = new();
                    rows[rowNumber].Add(point.X);
                }
            }
        }

        public static void PrintRocks(Dictionary<int, HashSet<int>> rows)
        {
            int i;
            var rowNumbers = rows.Select(p => p.Key).OrderBy(p => p).Reverse().ToList();
            foreach(var rowNumber in rowNumbers)
            {
                Console.Write("|");
                for(i = 0; i < CaveWidth; i++)
                {
                    if (rows[rowNumber].Contains(i))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("+-------+");
        }

        public static void PrintRock(Rock rock)
        {
            int x, y;
            int maxRow = rock.GetHighestPoint(), minRow = 1;
            
            for(y = maxRow; y > 0; y--)
            {
                Console.Write("|");
                for (x = 0; x < CaveWidth; x++)
                {
                    if (rock.Points.Contains(new Point(x, y)))
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