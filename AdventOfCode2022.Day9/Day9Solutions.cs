using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day9
{
    public class Day9Solutions
    {
        struct Position
        {
            public int x;
            public int y;
        }
        public static void Part1()
        {
            int numberOfVisits = TwoKnots("input.txt");
            Console.Write($"Day 9, Part 1 Solution: {numberOfVisits}");
        }
        public static void Part2()
        {
            int numberOfVisits = SimulateRope("input.txt");
            Console.Write($"Day 9, Part 2 Solution: {numberOfVisits}");
        }

        private static int TwoKnots(string file)
        {
            int neighbourhoodSize = 1, moveDistance;
            string moveDirection;

            HashSet<Position> visitLocations = new HashSet<Position>();

            Position head = new Position { x = 0, y = 0 };
            Position tail = new Position { x = 0, y = 0 };
            visitLocations.Add(tail);

            Regex regex = new Regex(@"(.) (\d+)");
            GroupCollection matchVars;

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day9/" + file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    matchVars = regex.Match(line).Groups;
                    moveDirection = matchVars[1].Value;
                    moveDistance = Convert.ToInt32(matchVars[2].Value);
                    for (; moveDistance > 0; moveDistance--)
                    {
                        head = MoveByOne(head, moveDirection);
                        if (IsNeighboring(head, tail, neighbourhoodSize))
                            continue;
                        tail = MoveToNeighbourhood(head, tail);
                        visitLocations.Add(tail);
                    }
                }
            }
            return visitLocations.Count;
        }
        private static int SimulateRope(string file)
        {
            int i, moveDistance, ropeLength = 10, neighbourhoodSize = 1;
            string moveDirection;
            Position[] rope = new Position[ropeLength];

            HashSet<Position> visitLocations = new HashSet<Position>();

            for(i = 0; i < ropeLength ; i++)
            {
                rope[i] = new Position { x = 0, y = 0 };
            }

            visitLocations.Add(rope[ropeLength-1]);

            Regex regex = new Regex(@"(.) (\d+)");
            GroupCollection matchVars;

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day9/" + file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    matchVars = regex.Match(line).Groups;
                    moveDirection = matchVars[1].Value;
                    moveDistance = Convert.ToInt32(matchVars[2].Value);
                    for (; moveDistance > 0; moveDistance--)
                    {
                        rope[0] = MoveByOne(rope[0], moveDirection);

                        for (i = 1; i < ropeLength; i++)
                        {
                            if (IsNeighboring(rope[i - 1], rope[i], neighbourhoodSize))
                                continue;
                            rope[i] = MoveToNeighbourhood(rope[i - 1], rope[i]);
                        }
                        visitLocations.Add(rope[ropeLength-1]);
                    }
                }
            }
            return visitLocations.Count;
        }

        private static bool IsNeighboring(Position head, Position tail, int neighbourhoodSize)
        {
            for(int i = -neighbourhoodSize; i <= neighbourhoodSize; i++)
            {
                for(int j = -neighbourhoodSize; j <= neighbourhoodSize; j++)
                {
                    if(((head.x + i) == tail.x) && ((head.y + j) == tail.y))
                        return true;
                }
            }
            return false;
        }

        private static Position MoveToNeighbourhood(Position head, Position tail)
        {
            if((head.x > tail.x) && (head.y > tail.y))
            {
                tail.x++;
                tail.y++;
            }
            else if ((head.x < tail.x) && (head.y > tail.y))
            {
                tail.x--;
                tail.y++;
            }
            else if ((head.x < tail.x) && (head.y < tail.y))
            {
                tail.x--;
                tail.y--;
            }
            else if ((head.x > tail.x) && (head.y < tail.y))
            {
                tail.x++;
                tail.y--;
            }
            else if((head.x == tail.x) && (head.y > tail.y))
                tail.y = head.y - 1;
            else if ((head.x == tail.x) && (head.y < tail.y))
                tail.y = head.y + 1;

            else if ((head.y == tail.y) && (head.x > tail.x))
                tail.x = head.x - 1;
            else if ((head.y == tail.y) && (head.x < tail.x))
                tail.x = head.x + 1;

            return tail;
        }

        private static Position MoveByOne(Position head, string direction)
        {
            switch (direction)
            {
                case ("U"):
                    head.y++;
                    break;
                case ("D"):
                    head.y --;
                    break;
                case ("L"):
                    head.x--;
                    break;
                case ("R"):
                    head.x++;
                    break;
            }
            return head;
        }

        private static void MoveRope(Position[] rope, string direction)
        {
            int ropeLength = rope.Length;
            Position from = rope[0];
            Position to = rope[0];
            switch (direction)
            {
                case ("U"):
                    rope[0].y += 1;
                    break;
                case ("D"):
                    rope[0].y -= 1;
                    break;
                case ("L"):
                    rope[0].x -= 1;
                    break;
                case ("R"):
                    rope[0].x += 1;
                    break;
            }
            for (int i = 1; i < ropeLength; i++)
            {
                from = rope[i];
                rope[i] = to;
                to = from;
            }
        }

        private static void PrintGrid(Position[] arr)
        {
            int gridSize = 11;

            for(int i = -gridSize/2; i < gridSize/2 + 1; i++)
            {
                for(int j = -gridSize / 2; j < gridSize / 2 + 1; j++)
                {
                    for(int k = 0; k < arr.Length; k++)
                    {
                        if (arr[k].x == i && arr[k].y == j)
                            Console.Write(k + " ");
                        else
                            Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}