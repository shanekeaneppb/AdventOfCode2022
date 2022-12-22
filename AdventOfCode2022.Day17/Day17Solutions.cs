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
        public static char[] JetArray;
        public static int JetArraySize;
        public static void Part1()
        {

            JetArray = LoadJetArray("test.txt");
            JetArraySize = JetArray.Length;
            int maxNumRocks = 12;

            //int lcm = NumRockShapes * jetArray.Length;
            //int numOfPeriods = maxNumRocks / lcm;
            //int remainingRocks = maxNumRocks % lcm;

            //int height;

            //int periodHeight = DropRocks(jetArray, lcm);

            //height = periodHeight * numOfPeriods;

            //height += DropRocks(jetArray, remainingRocks);

            int height = DropRocks(maxNumRocks);

            Console.WriteLine($"Day 17, Part 1 Solution: {height}");
        }
        public static void Part2()
        {
            //char[] jetArray = LoadJetPattern("test.txt");
            //int maxNumRocks = NumRockShapes * jetArray.Length;
            //int height = DropRocks(jetArray, maxNumRocks);

            //Console.WriteLine($"Day 17, Part 2 Solution: {height}");
        }

        public  static char[] LoadJetArray(string file)
        {
            char[] jetPattern;
            using (StreamReader reader = new(@"../../../../AdventOfCode2022.Day17/" + file))
            {
                jetPattern = reader.ReadLine().Trim().ToArray();
            }
            return jetPattern;
        }

        

        public  static int DropRocks(int maxRocks)
        {
            int jetIndex = 0, numFallenRocks = 0, highestRock = 0;
            char currentJet;
            bool isRockBlocked = true;

            HashSet<Point> fallenRocks = new();

            Point[] rock;

            bool canMoveBlock = false;
            while (numFallenRocks < maxRocks)
            {
                //Console.WriteLine(rockCount);
                if (isRockBlocked)
                {
                    rock = GetNextRock(numFallenRocks, highestRock);
                    isRockBlocked = false;
                }
                currentJet = GetNextJet(jetIndex);
                MoveRock(rock, currentJet);
                isRockBlocked = !DropRock(rock);
                if (isRockBlocked)
                {
                    numFallenRocks++;
                    highestRock= rock.Select(p => p.Y).Max();
                }
                jetIndex++;
            }
            return highestRock;
        }

        private static char GetNextJet(int jetIndex) => JetArray[jetIndex % JetArraySize];

        private static Point[] GetNextRock(int numFallenRocks, int highestPoint)
        {
            return new Point[NumRockShapes];
        }
        //public static void UpdateRows(Rock currentRock, Dictionary<int, HashSet<int>> rows)
        //{
        //    int rowNumber;
        //    foreach(var point in currentRock.Points)
        //    {
        //        rowNumber = point.Y;
        //        if(rows.ContainsKey(rowNumber))
        //            rows[rowNumber].Add(point.X);
        //        else
        //        {
        //            rows[rowNumber] = new();
        //            rows[rowNumber].Add(point.X);
        //        }
        //    }
        //}


        //public static void PrintRow(HashSet<int> row)
        //{
        //    Console.Write("|");
        //    for(int i = 0; i < CaveWidth; i++)
        //    {
        //        if(row.Contains(i))
        //            Console.Write("#");
        //        else
        //            Console.Write(".");

        //    }
        //    Console.WriteLine("|");
        //}
        //public static void PrintRocks(Dictionary<int, HashSet<int>> rows)
        //{
        //    int i;
        //    var rowNumbers = rows.Select(p => p.Key).OrderBy(p => p).Reverse().ToList();
        //    foreach (var rowNumber in rowNumbers)
        //    {
        //        Console.Write("|");
        //        for (i = 0; i < CaveWidth; i++)
        //        {
        //            if (rows[rowNumber].Contains(i))
        //                Console.Write("#");
        //            else
        //                Console.Write(".");
        //        }
        //        Console.WriteLine("|");
        //    }
        //    Console.WriteLine("+-------+");
        //}

        //public static void Compare(Dictionary<int, HashSet<int>> rows, int half)
        //{
        //    for(int i = 1; i < half; i++)
        //    {
        //        if (RowsAreEqual(rows[i], rows[i + half]))
        //            continue;
        //        else
        //            break;
        //    }
        //}

        //public static bool RowsAreEqual(HashSet<int> a, HashSet<int> b)
        //{
        //    foreach(var entry in a)
        //    {
        //        if (!b.Contains(entry))
        //            return false;
        //    }
        //    return true;
        //}


        
    }


}