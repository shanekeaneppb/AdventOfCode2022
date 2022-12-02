namespace AdventOfCode2022.Day2
{ 
    enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }



    public class Day2Solutions
    {
        public static void Part1()
        {
            Dictionary<string, Choice> OpponentStrat = new Dictionary<string, Choice>()
            {
                {"A", Choice.Rock},
                {"B", Choice.Paper},
                {"C", Choice.Scissors}
            };

            Dictionary<string, Choice> PlayerStrat = new Dictionary<string, Choice>()
            {
                {"X", Choice.Rock},
                {"Y", Choice.Paper},
                {"Z", Choice.Scissors}
            };
            int playerPoints = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day2/input.txt"))
            {
                string[] choices = new string[2];
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    choices = ln.Split();
                    playerPoints += Play(OpponentStrat[choices[0]], PlayerStrat[choices[1]]);
                }
                file.Close();
            }
            Console.WriteLine($"Day 2, Part 1 Solution:");
            Console.WriteLine($"Score = {playerPoints}");
        }
        public static void Part2()
        {
            Dictionary<string, Choice> OpponentStrat = new Dictionary<string, Choice>()
            {
                {"A", Choice.Rock},
                {"B", Choice.Paper},
                {"C", Choice.Scissors}
            };

            Dictionary<string, Choice> PlayerStrat = new Dictionary<string, Choice>()
            {
                {"X", Choice.Rock},
                {"Y", Choice.Paper},
                {"Z", Choice.Scissors}
            };
            int playerPoints = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day2/input.txt"))
            {
                string[] choices = new string[2];
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    choices = ln.Split();
                    playerPoints += Play(OpponentStrat[choices[0]], PlayerStrat[choices[1]]);
                }
                file.Close();
            }
            Console.WriteLine($"Day 2, Part 1 Solution:");
            Console.WriteLine($"Score = {playerPoints}");
        }

        private static int Play(Choice opponent, Choice player)
        {
            int score = 0;
            if(player == Choice.Rock)
            {
                score += 1;
                switch (opponent)
                {
                    case Choice.Rock:
                        score += 3;
                        break;
                    case Choice.Scissors:
                        score += 6;
                        break;
                }   
            }

            else if (player == Choice.Paper)
            {
                score += 2;
                switch (opponent)
                {
                    case Choice.Rock:
                        score += 6;
                        break;
                    case Choice.Paper:
                        score += 3;
                        break;
                }
            }

            else if (player == Choice.Scissors)
            {
                score += 3;
                switch (opponent)
                {
                    case Choice.Paper:
                        score += 6;
                        break;
                    case Choice.Scissors:
                        score += 3;
                        break;
                }
            }
            return score;
        }
    }
}