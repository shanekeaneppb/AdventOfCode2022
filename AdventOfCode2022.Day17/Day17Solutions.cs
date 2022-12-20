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
            int maxNumRocks = 616;

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
            int highestPoint = 0, rockCount = 0, i = 0, numJetInstructions = jetArray.Length;
            char currentJet;

            Dictionary<int, HashSet<int>> rows = new();

            Rock currentRock = new();

            bool canMoveBlock = false;
            while (rockCount < maxRocks)
            {
                //Console.WriteLine(rockCount);
                if (!canMoveBlock)
                {
                    currentRock = Rock.GetNewRock(rockCount, highestPoint);
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
                    if (rockCount % 200 == 0)
                    {

                        Console.WriteLine($"After, {rockCount} rocks, height = {highestPoint}");
                        PrintRocks(rows);
                        Console.WriteLine();
                    }
                }
                i++;
            }
            Compare(rows, maxRocks / 2);
            //PrintRow(rows[rows.Keys.Max()]);
            //Console.WriteLine();
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


        public static void PrintRow(HashSet<int> row)
        {
            Console.Write("|");
            for(int i = 0; i < CaveWidth; i++)
            {
                if(row.Contains(i))
                    Console.Write("#");
                else
                    Console.Write(".");

            }
            Console.WriteLine("|");
        }
        public static void PrintRocks(Dictionary<int, HashSet<int>> rows)
        {
            int i;
            var rowNumbers = rows.Select(p => p.Key).OrderBy(p => p).Reverse().ToList();
            foreach (var rowNumber in rowNumbers)
            {
                Console.Write("|");
                for (i = 0; i < CaveWidth; i++)
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

        public static void Compare(Dictionary<int, HashSet<int>> rows, int half)
        {
            for(int i = 1; i < half; i++)
            {
                if (RowsAreEqual(rows[i], rows[i + half]))
                    continue;
                else
                    break;
            }
        }

        public static bool RowsAreEqual(HashSet<int> a, HashSet<int> b)
        {
            foreach(var entry in a)
            {
                if (!b.Contains(entry))
                    return false;
            }
            return true;
        }


        //public static void PrintRock(Rock rock)
        //{
        //    int x, y;
        //    int maxRow = rock.GetHighestPoint(), minRow = 1;

        //    for(y = maxRow; y > 0; y--)
        //    {
        //        Console.Write("|");
        //        for (x = 0; x < CaveWidth; x++)
        //        {
        //            if (rock.Points.Contains(new Point(x, y)))
        //                Console.Write("#");
        //            else
        //                Console.Write(".");
        //        }
        //        Console.WriteLine("|");
        //    }
        //    Console.WriteLine("+-------+");
        //}

        //public static long Solve(long rocksToFall, char[] jetArray)
        //{
        //    Dictionary<(int startingRockIndex, int startingJetIndex),
        //        (int finishingRockIndex, int finishingJetIndex, int height, int numFallenRocks)> combinations = GetAllCombinations(jetArray);
        //    int rockIndex = 0, jetIndex = 0, height = 0, numFallenRocks = 0;
        //    long totalHeight = 0;
        //    while (rocksToFall > 0)
        //    {
        //        (rockIndex, jetIndex, height, numFallenRocks) = combinations[(rockIndex, jetIndex)];
        //        if ((rocksToFall - numFallenRocks) < 0)
        //            break;
        //        rocksToFall -= numFallenRocks;
        //        totalHeight += height;
        //    }
        //    totalHeight += DropNRocks(rockIndex, jetIndex, jetArray, (int)rocksToFall);

        //    return totalHeight;
        //}
        //public static Dictionary<(int startingRockIndex, int startingJetIndex),
        //    (int finishingRockIndex, int finishingJetIndex, int height, int numFallenRocks)> 
        //    GetAllCombinations(char[] jetArray)
        //{
        //    Dictionary<(int startingRockIndex, int startingJetIndex), 
        //        (int finishingRockIndex, int finishingJetIndex, int height, int numFallenRocks)> combinations = new();
        //    for(int i = 0; i < NumRockShapes; i++)
        //    { 
        //        Console.WriteLine(i);
        //        for(int j = 0; j < jetArray.Length; i++)
        //        {
        //            combinations[(i, j)] = Drop(i, j, jetArray);
        //        }
        //    }
        //    return combinations;
        //}
        //public static (int finishingRockIndex, int finishingJetIndex, int heightReached, int numFallenRocks) Drop(int startingRock, int startingJet, char[] jetArray)
        //{
        //    int highestPoint = 0, numFallenRocks = 0, rockIndex= startingRock, jetIndex = startingJet, numJetInstructions = jetArray.Length;
        //    char currentJet;

        //    Dictionary<int, HashSet<int>> rows = new();

        //    Rock currentRock = new();

        //    bool canMoveBlock = false;
        //    while (true)
        //    {
        //        if (!canMoveBlock)
        //        {
        //            currentRock = Rock.GetNewRock(rockIndex, highestPoint);
        //            canMoveBlock = true;
        //        }
        //        currentJet = jetArray[jetIndex % numJetInstructions];
        //        currentRock.MoveRightLeft(currentJet, rows);
        //        canMoveBlock = currentRock.MoveDown(rows);
        //        if (!canMoveBlock)
        //        {
        //            rockIndex++;
        //            numFallenRocks++;
        //            UpdateRows(currentRock, rows);
        //            highestPoint = rows.Select(p => p.Key).Max();
        //            if (IsNewFloor(rows))
        //                break;
        //        }
        //        jetIndex++;
        //    }
        //    return (rockIndex%NumRockShapes, jetIndex%numJetInstructions, highestPoint, numFallenRocks);
        //}

        //public static int DropNRocks(int startingRock, int startingJet, char[] jetArray, int N)
        //{
        //    int highestPoint = 0, numFallenRocks = 0, rockIndex = startingRock, jetIndex = startingJet, numJetInstructions = jetArray.Length;
        //    char currentJet;

        //    Dictionary<int, HashSet<int>> rows = new();

        //    Rock currentRock = new();

        //    bool canMoveBlock = false;
        //    while (true)
        //    {
        //        if (!canMoveBlock)
        //        {
        //            currentRock = Rock.GetNewRock(rockIndex, highestPoint);
        //            canMoveBlock = true;
        //        }
        //        currentJet = jetArray[jetIndex % numJetInstructions];
        //        currentRock.MoveRightLeft(currentJet, rows);
        //        canMoveBlock = currentRock.MoveDown(rows);
        //        if (!canMoveBlock)
        //        {
        //            rockIndex++;
        //            numFallenRocks++;
        //            UpdateRows(currentRock, rows);
        //            highestPoint = rows.Select(p => p.Key).Max();
        //            if (numFallenRocks >= N)
        //                break;
        //        }
        //        jetIndex++;
        //    }
        //    return highestPoint;
        //}
        //public static bool IsNewFloor(Dictionary<int, HashSet<int>> rows)
        //{
        //    int maxRow = rows.Select(p => p.Key).Max();
        //    int i = 0;
        //    for (i = 0; i < CaveWidth; i++)
        //    {
        //        if (rows[maxRow].Contains(i)) { Console.Write("#"); }
        //        else { Console.Write("."); }
        //    }
        //    Console.WriteLine();
        //        for (i = 0; i < CaveWidth; i++)
        //    {
        //        if (!rows[maxRow].Contains(i))
        //            return false;
        //    }
        //    return true;
        //}


    }


}