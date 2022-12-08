using System.Data;

namespace AdventOfCode2022.Day8
{
    public class Day8Solutions
    {
        public static void Part1()
        {
            int visibleTrees = 0;
            var grid = BuildForestGrid("input.txt");
            int gridWidth = grid.Count, gridHeight = grid.First().Length;

            for (int row = 0; row < gridWidth; row++)
            {
                for (int column = 0; column < gridHeight; column++)
                {
                    if (row == 0 || row == gridWidth - 1 || column == 0 || column == gridHeight - 1)
                    {
                        visibleTrees++;
                        continue;
                    }
                    visibleTrees += !IsHidden(grid, row, column) ? 1 : 0;
                }
            }
            Console.Write($"Day 8, Part 1 Solution: {visibleTrees}");
        }
        public static void Part2()
        {
            int maxScenicScore = 0;
            var grid = BuildForestGrid("input.txt");
            int gridWidth = grid.Count, gridHeight = grid.First().Length;

            for (int row = 0; row < gridWidth; row++)
            {
                for (int column = 0; column < gridHeight; column++)
                {
                    if (row == 0 || row == gridWidth - 1 || column == 0 || column == gridHeight - 1)
                    {
                        continue;
                    }
                    maxScenicScore = Math.Max(maxScenicScore, GetScenicScore(grid, row, column));
                }
            }
            Console.Write($"Day 8, Part 2 Solution: {maxScenicScore}");
        }

        private static bool IsHidden(List<int[]> grid, int row, int column)
        {
            int i, gridWidth = grid.Count, gridHeight = grid.First().Length, currentSize = grid[row][column];
            bool hiddenAbove = false, hiddenBelow = false, hiddenLeft = false, hiddenRight = false;
            // Left of tree
            for(i = 0; i < column; i++)
            {
                if (grid[row][i] >= currentSize)
                {
                    hiddenLeft = true;
                    break;
                }
            }
            // Right of tree
            for (i = column+1; i < gridWidth; i++)
            {
                if (grid[row][i] >= currentSize)
                {
                    hiddenRight = true;
                    break;
                }
            }
            // Above tree
            for (i = 0; i < row; i++)
            {
                if (grid[i][column] >= currentSize)
                {
                    hiddenAbove = true;
                    break;
                }
            }
            // Below tree
            for (i = row+1; i < gridHeight; i++)
            {
                if (grid[i][column] >= currentSize)
                {
                    hiddenBelow = true;
                    break;
                }
            }
            return (hiddenAbove && hiddenBelow && hiddenLeft && hiddenRight);
        }

        private static int GetScenicScore(List<int[]> grid, int row, int column)
        {
            int i, viewIndex = 1, scenicScore = 1, gridWidth = grid.Count, gridHeight = grid.First().Length, currentSize = grid[row][column];
            // Going left
            for (i = column - 1; i >= 0; i--)
            {
                viewIndex = column - i;
                if (grid[row][i] >= currentSize)
                {
                    break;
                }
            }
            scenicScore *= viewIndex;
            //Going right
            for (i = column + 1; i < gridWidth; i++)
            {
                viewIndex = i - column;
                if (grid[row][i] >= currentSize)
                {
                    break;
                }
            }
            scenicScore *= viewIndex;
            //Going up
            for (i = row - 1; i >= 0; i--)
            {
                viewIndex = row - i;
                if (grid[i][column] >= currentSize)
                {
                    break;
                }
            }
            scenicScore *= viewIndex;
            //Going down
            for (i = row + 1; i < gridHeight; i++)
            {
                viewIndex = i - row;
                if (grid[i][column] >= currentSize)
                {
                    break;
                }
            }
            scenicScore *= viewIndex;
            return scenicScore;
        }

        private static List<int[]> BuildForestGrid(string file)
        {
            List<int[]> grid = new List<int[]>();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day8/" + file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    grid.Add(line.ToArray().Select(c => Convert.ToInt32(c)).ToArray());
                }
            }
            return grid;
        }
    }
}