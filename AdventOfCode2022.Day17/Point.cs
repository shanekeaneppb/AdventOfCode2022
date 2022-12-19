namespace AdventOfCode2022.Day17
{
    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveDown()
        { 
            Y -= 1;
        }

        public void MoveRight()
        {
   
            X += 1;
        }
        public void MoveLeft()
        {

            X -= 1;
        }
        public override string ToString() => $"({X}, {Y})";

        public override bool Equals(object? obj)
        {
            try
            {
                Point p = obj as Point;
                return (p.X == X && p.Y == Y);
            }
            catch
            {
                return false;
            }
        }

    }

}
