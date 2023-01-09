using System;
using System.Collections.Generic;

namespace AdventOfCode2022.Day23
{
    public class Day23Solutions
    {
        public static void Part1()
        {
            var elves = LoadElves("input.txt");
            int numberOfIterations = 10;
            for (int i = 0; i < numberOfIterations; i++)
            {
                MoveElves(elves, i);
            }
            int emptyLocations = GetEmptyLocationsInRectangle(elves);
            Console.WriteLine($"Day 23, Part 1 Solution: {emptyLocations}");
        }
        public static void Part2()
        {
            var elves = LoadElves("input.txt");
            int i = 0;
            while(MoveElves(elves, i))
            {
                i++;   
            }
            Console.WriteLine($"Day 23, Part 2 Solution: {i+1}");
        }

        private static HashSet<Point> LoadElves(string file)
        {
            HashSet<Point> elves = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day23/" + file))
            {
                string line;
                int row = 0, column;
                while((line = reader.ReadLine()) != null)
                {
                    for(column = 0; column < line.Length; column++)
                        if (line[column] == '#')
                            elves.Add(new Point(row, column));
                    row++;
                }
            }
            return elves;
        }

        private static bool MoveElves(HashSet<Point> elves, int roundNumber)
        {
            Dictionary<Point, Point> elfvesProposedTiles = new();
            CounterDict proposedTiles = new();
            Point proposedTile;

            foreach(var elf in elves)
            {
                proposedTile = ProposeTile(elf, elves, roundNumber);
                if (proposedTile != null)
                {
                    elfvesProposedTiles.Add(elf, proposedTile);
                    proposedTiles.Add(proposedTile);
                }

            }
            foreach(var pair in elfvesProposedTiles)
            {
                if (proposedTiles[pair.Value] == 1)
                {
                    elves.Remove(pair.Key);
                    elves.Add(pair.Value);
                }
            }
            return (elfvesProposedTiles.Count > 0);
        }

        private static Point? ProposeTile(Point elf, HashSet<Point> elves, int roundNumber)
        {
            if (IsAlone(elf, elves))
                return null;
            int startIndex = roundNumber % 4;
            Point proposedTile;
            List<Func<Point, HashSet<Point>, Point>> funcs = new()
            {
                NorthProposal,
                SouthProposal,
                WestProposal,
                EastProposal
            };
            for(int i = 0; i < 4; i++)
                if((proposedTile = funcs[(startIndex + i) % 4](elf, elves)) != null)
                    return proposedTile;
            return null;
        }

        private static bool IsAlone(Point elf, HashSet<Point> elves)
        {
            Point point;
            for(int row = elf.Row-1; row <= elf.Row + 1; row++)
            {
                for (int column = elf.Column - 1; column <= elf.Column + 1; column++)
                {
                    point = new(row, column);
                    if ((elves.Contains(point)) && (!point.Equals(elf)))
                        return false;
                }
            }
            return true;
        }
        private static Point NorthProposal(Point elf, HashSet<Point> elves)
        {
            if (
                (!elves.Contains(new (elf.Row - 1, elf.Column - 1))) 
                && (!elves.Contains(new(elf.Row - 1, elf.Column))) 
                && (!elves.Contains(new(elf.Row - 1, elf.Column + 1)))
                )
                return new Point(elf.Row - 1, elf.Column);
            return null;
        }
        private static Point SouthProposal(Point elf, HashSet<Point> elves)
        {
            if (
                (!elves.Contains(new(elf.Row + 1, elf.Column - 1)))
                && (!elves.Contains(new(elf.Row + 1, elf.Column)))
                && (!elves.Contains(new(elf.Row + 1, elf.Column + 1)))
                )
                return new Point(elf.Row + 1, elf.Column);
            return null;
        }
        private static Point EastProposal(Point elf, HashSet<Point> elves)
        {
            if (
                (!elves.Contains(new(elf.Row - 1, elf.Column + 1)))
                && (!elves.Contains(new(elf.Row, elf.Column + 1)))
                && (!elves.Contains(new(elf.Row + 1, elf.Column + 1)))
                )
                return new Point(elf.Row, elf.Column + 1);
            return null;
        }
        private static Point WestProposal(Point elf, HashSet<Point> elves)
        {
            if (
                (!elves.Contains(new(elf.Row - 1, elf.Column - 1)))
                && (!elves.Contains(new(elf.Row, elf.Column - 1)))
                && (!elves.Contains(new(elf.Row + 1, elf.Column - 1)))
                )
                return new Point(elf.Row, elf.Column - 1);
            return null;
        }

        private static int GetEmptyLocationsInRectangle(HashSet<Point> elves)
        {
            int minRow, maxRow, minColumn, maxColumn;

            minRow = elves.Select(p => p.Row).Min();
            maxRow = elves.Select(p => p.Row).Max();
            minColumn = elves.Select(p => p.Column).Min();
            maxColumn = elves.Select(p => p.Column).Max();

            int rowRange = maxRow - minRow + 1;
            int columnRange = maxColumn - minColumn + 1;

            int totalArea = columnRange * rowRange;

            int emptyLocations = totalArea - elves.Count;

            return emptyLocations;
        }
        private static void PrintElves(HashSet<Point> elves)
        {
            int minRow, maxRow, minColumn, maxColumn;

            minRow = elves.Select(p => p.Row).Min();
            maxRow = elves.Select(p => p.Row).Max();
            minColumn = elves.Select(p => p.Column).Min();
            maxColumn = elves.Select(p => p.Column).Max();

            for (int row = minRow; row <= maxRow; row++)
            {
                for (int column = minColumn; column <= maxColumn; column++)
                {
                    if(elves.Contains(new(row, column)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    
}