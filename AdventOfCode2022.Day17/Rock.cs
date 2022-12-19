namespace AdventOfCode2022.Day17
{
    
    public class Rock
    {
        public static int NumRockShapes = 5;
        public static int LeftSpawn = 2;
        public static int VerticalSpawn = 3 + 1; // counting the floor as index 0, so add 1 to actual spawn of 3
        public static int CaveWidth = 7;

        public Point[] Points;

        public Rock(params Point[] points)
        {
            Points = points;
        }

        public Rock(Rock rock)
        {
            Points = rock.Points;
        }

        public Point[] GetHighestPoints()
        {
            int highestYValue = Points.Select(p => p.Y).Max();
            return Points.Where(p => p.Y == highestYValue).ToArray();
        }
        public Point[] GetLowestPoints()
        {
            int lowestYValue = Points.Select(p => p.Y).Min();
            return Points.Where(p => p.Y == lowestYValue).ToArray();
        }

        public Point[] GetRightMostPoints()
        {
            int highestXValue = Points.Select(p => p.X).Max();
            return Points.Where(p => p.X == highestXValue).ToArray();
        }

        public Point[] GetLeftMostPoints()
        {
            int lowestXValue = Points.Select(p => p.X).Min();
            return Points.Where(p => p.X == lowestXValue).ToArray();
        }

        public int GetHighestPoint() =>  Points.Select(p => p.Y).Max();
        public int GetLowestPoint() =>  Points.Select(p => p.Y).Min();
        
        public bool MoveRightLeft(char direction, Dictionary<int, HashSet<int>> rows)
        {
            if (direction == '>')
                return MoveRight(rows);
            else
                return MoveLeft(rows);
        }

        public bool MoveRight(Dictionary<int, HashSet<int>> rows)
        {
            foreach(var point in this.Points)
            {
                if((point.X + 1) >= CaveWidth)
                    return false;
                else if (!rows.ContainsKey(point.Y))
                    continue;
                else if ((rows[point.Y].Contains(point.X + 1)))
                    return false;
            }
            foreach (var point in this.Points)
            {
                point.MoveRight();
            }
            return true;
        }
        public bool MoveLeft(Dictionary<int, HashSet<int>> rows)
        {
            foreach (var point in this.Points)
            {
                if((point.X - 1) < 0)
                    return false;
                else if (!rows.ContainsKey(point.Y))
                    continue;
                else if ((rows[point.Y].Contains(point.X - 1)))
                    return false;
            }
            foreach (var point in this.Points)
            {
                point.MoveLeft();
            }
            return true;
        }

        public bool MoveDown(Dictionary<int, HashSet<int>> rows)
        {
            foreach (var point in this.Points)
            {
                if ((point.Y - 1) <= 0)
                    return false;
                else if (!rows.ContainsKey(point.Y - 1))
                    continue;
                else if ((rows[point.Y - 1].Contains(point.X)))
                    return false;
            }
            foreach (var point in this.Points)
            {
                point.MoveDown();
            }
            return true;
        }
        public static Rock GetNewRock(int rockCount, int highestPoint)
        {
            Rock rock = new();
            int rockNumber = rockCount % NumRockShapes;
            int Y = highestPoint + VerticalSpawn;
            switch (rockNumber)
            {
                case 0:
                    rock = new Rock(
                        new Point[] {
                            new Point(LeftSpawn, Y),
                            new Point(LeftSpawn + 1, Y),
                            new Point(LeftSpawn + 2, Y),
                            new Point(LeftSpawn + 3, Y)
                        }
                    );
                    break;

                case 1:
                    rock = new Rock(
                        new Point[] {
                            new Point(LeftSpawn, Y + 1),
                            new Point(LeftSpawn + 1, Y + 1),
                            new Point(LeftSpawn + 2, Y + 1),
                            new Point(LeftSpawn + 1, Y ),
                            new Point(LeftSpawn + 1, Y + 2),
                        }
                    );
                    break;

                case 2:
                    rock = new Rock(
                        new Point[] {
                            new Point(LeftSpawn, Y),
                            new Point(LeftSpawn + 1, Y),
                            new Point(LeftSpawn + 2, Y),
                            new Point(LeftSpawn + 2, Y + 1),
                            new Point(LeftSpawn + 2, Y + 2),
                        }
                    );
                    break;

                case 3:
                    rock = new Rock(
                        new Point[] {
                            new Point(LeftSpawn, Y),
                            new Point(LeftSpawn, Y + 1),
                            new Point(LeftSpawn, Y + 2),
                            new Point(LeftSpawn, Y + 3),
                        }
                    );
                    break;

                case 4:
                    rock = new Rock(
                        new Point[] {
                            new Point(LeftSpawn, Y),
                            new Point(LeftSpawn + 1, Y),
                            new Point(LeftSpawn, Y + 1),
                            new Point(LeftSpawn + 1, Y + 1),
                        }
                    );
                    break;
            }
            return rock;
        }

        public int GetHeight()
        {
            int minYValue = this.Points.Select(p => p.Y).Min();
            int maxYValue = this.Points.Select(p => p.Y).Max();

            return maxYValue - minYValue + 1;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var point in Points)
                s += point.ToString();
            return s;
        }
    }
}
