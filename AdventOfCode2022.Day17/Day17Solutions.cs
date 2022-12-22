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

            JetArray = LoadJetArray("input.txt");
            JetArraySize = JetArray.Length;
            int maxNumRocks = 2022;

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

            Point[] rock = new Point[NumRockShapes];

            while (numFallenRocks < maxRocks)
            {
                //Console.WriteLine(rockCount);
                if (isRockBlocked)
                {
                    rock = GetNextRock(numFallenRocks, highestRock);
                    isRockBlocked = false;
                }
                currentJet = GetNextJet(jetIndex);
                MoveRock(rock, fallenRocks, currentJet);
                isRockBlocked = !DropRock(rock, fallenRocks);
                if (isRockBlocked)
                {
                    numFallenRocks++;
                    fallenRocks.UnionWith(rock);
                    highestRock= fallenRocks.Select(p => p.Y).Max();
                }
                jetIndex++;
            }
            return highestRock;
        }

        private static char GetNextJet(int jetIndex) => JetArray[jetIndex % JetArraySize];


        public static Point[] GetNextRock(int numFallenRocks, int highestRock)
        {
            Point[] rock = new Point[NumRockShapes];
            int rockNumber = numFallenRocks % NumRockShapes;
            int y = highestRock + VerticalSpawn;
            switch (rockNumber)
            {
                case 0:
                    rock = new Point[]
                    {
                        new Point(LeftSpawn, y),
                        new Point(LeftSpawn + 1, y),
                        new Point(LeftSpawn + 2, y),
                        new Point(LeftSpawn + 3, y)
                    };
                    break;

                case 1:
                    rock =  new Point[] 
                    {
                        new Point(LeftSpawn, y + 1),
                        new Point(LeftSpawn + 1, y + 1),
                        new Point(LeftSpawn + 2, y + 1),
                        new Point(LeftSpawn + 1, y ),
                        new Point(LeftSpawn + 1, y + 2),
                    };
                    break;

                case 2:
                    rock = new Point[] 
                    {
                        new Point(LeftSpawn, y),
                        new Point(LeftSpawn + 1, y),
                        new Point(LeftSpawn + 2, y),
                        new Point(LeftSpawn + 2, y + 1),
                        new Point(LeftSpawn + 2, y + 2),
                    };
                    break;

                case 3:
                    rock = new Point[] 
                    {
                        new Point(LeftSpawn, y),
                        new Point(LeftSpawn, y + 1),
                        new Point(LeftSpawn, y + 2),
                        new Point(LeftSpawn, y + 3),
                    };
                    break;

                case 4:
                    rock = new Point[]
                    {
                        new Point(LeftSpawn, y),
                        new Point(LeftSpawn + 1, y),
                        new Point(LeftSpawn, y + 1),
                        new Point(LeftSpawn + 1, y + 1),
                    };
                    break;
            }
            return rock;
        }


        public static void MoveRock(Point[] rock, HashSet<Point> fallenRocks, char jet)
        {
            int increment = (jet == '>') ? 1 : -1;
            int i;
            Point p = new(0,0);
            for (i = 0; i < rock.Length; i++)
            {
                p = new(rock[i].X + increment, rock[i].Y);
                if ((fallenRocks.Contains(p)) || (p.X < 0) || (p.X >= CaveWidth))
                    return;
            }
            for(i = 0; i < rock.Length; i++)
            {
                rock[i].X += increment;
            }
        }

        public static bool DropRock(Point[] rock, HashSet<Point> fallenRocks)
        {
            int i;
            Point p = new(0, 0);
            for (i = 0; i < rock.Length; i++)
            {
                p = new(rock[i].X, rock[i].Y - 1);
                if ((fallenRocks.Contains(p)) || (p.Y <= 0))
                    return false;
            }
            for (i = 0; i < rock.Length; i++)
            {
                rock[i].Y--;
            }
            return true;
        }
    }
}