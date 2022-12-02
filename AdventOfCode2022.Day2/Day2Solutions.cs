namespace AdventOfCode2022.Day2
{ 
    enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    enum Outcome
    {
        Win = 1,
        Lose = 2,
        Draw = 3
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
                string[] inputs = new string[2];
                string ln; 
                Choice opponentChoice;
                Choice playerChoice;

                while ((ln = file.ReadLine()) != null)
                {
                    inputs = ln.Split();
                    opponentChoice = OpponentStrat[inputs[0]];
                    playerChoice = PlayerStrat[inputs[1]];
                    playerPoints += Play(opponentChoice, playerChoice);
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

            Dictionary<string, Outcome> OutcomeStrat = new Dictionary<string, Outcome>()
            {
                {"X", Outcome.Lose},
                {"Y", Outcome.Draw},
                {"Z", Outcome.Win}
            };
            int playerPoints = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day2/input.txt"))
            {
                string[] inputs = new string[2];
                string ln;
                Outcome gameOutcome;
                Choice opponentChoice;
                Choice playerChoice;

                while ((ln = file.ReadLine()) != null)
                {
                    inputs = ln.Split();
                    opponentChoice = OpponentStrat[inputs[0]];
                    gameOutcome = OutcomeStrat[inputs[1]];
                    playerChoice = GetPlayerChoice(opponentChoice, gameOutcome);
                    playerPoints += Play(opponentChoice, playerChoice);
                }
                file.Close();
            }
            Console.WriteLine($"Day 2, Part 2 Solution:");
            Console.WriteLine($"Score = {playerPoints}");
        }

        private static int Play(Choice opponentChoice, Choice playerChoice)
        {
            int score = 0;
            if(playerChoice == Choice.Rock)
            {
                score += 1;
                switch (opponentChoice)
                {
                    case Choice.Rock:
                        score += 3;
                        break;
                    case Choice.Scissors:
                        score += 6;
                        break;
                }   
            }

            else if (playerChoice == Choice.Paper)
            {
                score += 2;
                switch (opponentChoice)
                {
                    case Choice.Rock:
                        score += 6;
                        break;
                    case Choice.Paper:
                        score += 3;
                        break;
                }
            }

            else if (playerChoice == Choice.Scissors)
            {
                score += 3;
                switch (opponentChoice)
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

        private static Choice GetPlayerChoice(Choice opponentChoice, Outcome outcome)
        {
            Choice playerChoice = Choice.Rock;

            if (opponentChoice == Choice.Rock)
            {
                switch (outcome)
                {
                    case Outcome.Win:
                        playerChoice = Choice.Paper;
                        break;
                    case Outcome.Lose:
                        playerChoice = Choice.Scissors;
                        break;
                    case Outcome.Draw:
                        playerChoice = opponentChoice;
                        break;
                }
            }

            else if (opponentChoice == Choice.Paper)
            {
                switch (outcome)
                {
                    case Outcome.Win:
                        playerChoice = Choice.Scissors;
                        break;
                    case Outcome.Lose:
                        playerChoice = Choice.Rock;
                        break;
                    case Outcome.Draw:
                        playerChoice = opponentChoice;
                        break;
                }
            }

            else if (opponentChoice == Choice.Scissors)
            {
                switch (outcome)
                {
                    case Outcome.Win:
                        playerChoice = Choice.Rock;
                        break;
                    case Outcome.Lose:
                        playerChoice = Choice.Paper;
                        break;
                    case Outcome.Draw:
                        playerChoice = opponentChoice;
                        break;
                }
            }

            return playerChoice;
        }
    }
}