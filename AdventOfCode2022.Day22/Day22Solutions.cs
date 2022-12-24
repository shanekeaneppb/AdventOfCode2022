using System.Xml.Xsl;
using static AdventOfCode2022.Day22.Day22Solutions;

namespace AdventOfCode2022.Day22
{
    public class Day22Solutions
    {
        public enum Direction
        {
            N = 3,
            S = 1,
            E = 0,
            W = 2
        }


        public static HashSet<Point> Map, Walls;
        public static List<string> Instructions;
        public static void Part1()
        {
            // 86244 is incorrect. Answer is too high
            (Map, Walls, string instructions) = LoadMap("input.txt");
            Instructions = SplitDirections(instructions);
            Direction direction = Direction.E;
            Point position = SetInitialLocation();
            foreach(var instruction in Instructions)
            {
                ExecuteInstruction(instruction, ref position, ref direction);
            }
            int password = GetPassword(position, direction);
            Console.WriteLine($"Day 22, Part 1 Solution: {password}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 22, Part 2 Solution: ");
        }

        private static (HashSet<Point> map, HashSet<Point> walls, string directions) LoadMap(string file)
        {
            HashSet<Point> map = new();
            HashSet<Point> walls = new();
            string directions = "";
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day22/" + file))
            {
                string line;
                int row = 1, column = 1;
                Point point;
                while((line = reader.ReadLine()) != null)
                {
                    if(line == "")
                    {
                        directions = reader.ReadLine().Trim();
                        break;
                    }
                    column = 1;
                    foreach (char c in line)
                    {
                        if (c == ' ')
                        {
                            column++;
                            continue;
                        }
                        point = new Point(column, row);
                        map.Add(point);
                        if (c == '#')
                            walls.Add(new Point(column, row));
                        column++;
                    }
                    row++;
                }
                
            }
            return (map, walls, directions);
        }

        private static List<string> SplitDirections(string directions)
        {
            List<string> splitDirections = new();
            int i = 0;
            while(true)
            {
                if(IsDigit(directions) || (directions == "R") || (directions == "L"))
                {
                    splitDirections.Add(directions);
                    break;
                }
                if ((directions[i] == 'R') || (directions[i] == 'L'))
                {
                    splitDirections.Add(directions.Substring(0, i));
                    splitDirections.Add(directions[i].ToString());
                    directions = directions.Remove(0, i+1);
                    i = 0;
                }
                i++;
            }
            return splitDirections;
        }

        private static Direction Rotate(string instruction, Direction currentDirection)
        {
            switch (instruction)
            {
                case "R":
                    switch (currentDirection)
                    {
                        case Direction.N:
                            return Direction.E;
                        case Direction.E:
                            return Direction.S;
                        case Direction.S:
                            return Direction.W;
                        case Direction.W:
                            return Direction.N;
                    }
                    break;
                case "L":
                    switch (currentDirection)
                    {
                        case Direction.N:
                            return Direction.W;
                        case Direction.W:
                            return Direction.S;
                        case Direction.S:
                            return Direction.E;
                        case Direction.E:
                            return Direction.N;
                    }
                    break;
            }
            return currentDirection;
        }

        private static void ExecuteInstruction(string instruction, ref Point position, ref Direction direction)
        {
            if ((instruction == "R") || (instruction == "L"))
            {
                direction = Rotate(instruction, direction);
                return;
            }
            Move(instruction, ref position, direction);
        }
        private static Point SetInitialLocation()
        {
            HashSet<Point> noWalls = new (Map);
            noWalls.ExceptWith(Walls);
            int minRow = noWalls.Select(p => p.Y).Min();
            List<Point> pointsInMinRow = noWalls.Where(p => p.Y == minRow).ToList();
            int minColumnInRow = pointsInMinRow.Select(p => p.X).Min();

            return pointsInMinRow.Where(p => p.X == minColumnInRow).First();
        }

        private static void Move(string instruction, ref Point position, Direction direction)
        {
            int steps = Convert.ToInt32(instruction);
            while ((steps > 0) && (Step(ref position, direction)))
                steps--;
        }
        private static bool Step(ref Point position, Direction direction)
        {
            Point newPosition;
            if(direction == Direction.N)
            {
                newPosition = new Point(position.X, position.Y - 1);
                if (Walls.Contains(newPosition))
                    return false;
                else if (Map.Contains(newPosition))
                {
                    position = newPosition;
                    return true;
                }
                int maxRowInColumn = Map.Where(p => p.X == newPosition.X).Select(p => p.Y).Max();
                newPosition = new(position.X, maxRowInColumn);
                if (Walls.Contains(newPosition))
                    return false;
                position = newPosition;
            }
            else if (direction == Direction.S)
            {
                newPosition = new Point(position.X, position.Y + 1);
                if (Walls.Contains(newPosition))
                    return false;
                else if (Map.Contains(newPosition))
                {
                    position = newPosition;
                    return true;
                }
                int minRowInColumn = Map.Where(p => p.X == newPosition.X).Select(p => p.Y).Min();
                newPosition = new(position.X, minRowInColumn);
                if (Walls.Contains(newPosition))
                    return false;
                position = newPosition;
            }
            else if (direction == Direction.E)
            {
                newPosition = new Point(position.X + 1, position.Y);
                if (Walls.Contains(newPosition))
                    return false;
                else if (Map.Contains(newPosition))
                {
                    position = newPosition;
                    return true;
                }
                int minComumnInRow = Map.Where(p => p.Y == newPosition.Y).Select(p => p.X).Min();
                newPosition = new(minComumnInRow, newPosition.Y);
                if (Walls.Contains(newPosition))
                    return false;
                position = newPosition;
            }
            else if (direction == Direction.W)
            {
                newPosition = new Point(position.X - 1, position.Y);
                if (Walls.Contains(newPosition))
                    return false;
                else if (Map.Contains(newPosition))
                {
                    position = newPosition;
                    return true;
                }
                int maxComumnInRow = Map.Where(p => p.Y == newPosition.Y).Select(p => p.X).Max();
                newPosition = new(maxComumnInRow, position.X);
                if (Walls.Contains(newPosition))
                    return false;
                position = newPosition;
            }
            return true;
        }
        private static int GetPassword(Point position, Direction direction)
        {
            int password = (int)direction;
            password += 1000 * position.Y;
            password += 4 * position.X;

            return password;
        }

        private static bool IsDigit(string str)
        {
            foreach (char c in str)
                if (!char.IsDigit(c))
                    return false;
            return true;
        }
    }
}