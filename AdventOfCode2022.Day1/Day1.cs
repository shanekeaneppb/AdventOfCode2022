namespace AdventOfCode2022.Day1
{
    public class Day1Solutions
    {

        public static void Part1()
        {
            Console.WriteLine($"Day 1, Part 1 Solution:");
            Console.WriteLine($"Max Calories = {MaxNCalories(1)}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 1, Part 1 Solution:");
            Console.WriteLine($"Max 3 Calories = {MaxNCalories(3)}");
        }

        private static int MaxNCalories(int n)
        {
            string textFile = @"../../../../AdventOfCode2022.Day1/input.txt";
            int currentElfCalories = 0;
            List<int> caloriesList = new List<int>();
            string[] caloriesLines = File.ReadAllLines(textFile);

            foreach (string calories in caloriesLines)
            {
                if (calories == "")
                {
                    caloriesList.Add(currentElfCalories);
                    currentElfCalories = 0;
                }
                else
                {
                    currentElfCalories += Convert.ToInt32(calories);
                }
            }

            int maxNCalories = (from item in caloriesList
                                orderby item descending
                                select item
                                ).Take(n).Sum();

            return maxNCalories;
        }
    }

}
