using System.Data;

namespace AdventOfCode2022.Day21
{
    public class Day21Solutions
    {
        public static void Part1()
        {
            // -1839865948 is incorrect. No info on whether too big or small
            Dictionary<string, string> dict = new();
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day21/" + "input.txt"))
            {
                string line, key, value;
                string[] pair;
                while ((line = file.ReadLine()) != null)
                {
                    pair = line.Trim().Split(":");
                    key = pair[0].Trim();
                    value = pair[1].Trim();
                    dict.Add(key, value);
                }

                foreach (var p in dict)
                {
                    key = p.Key;
                    value = p.Value;
                    dict[key] = EvaluateExpression(value, dict);
                }
            }
            Console.WriteLine($"Day 21, Part 1 Solution: {dict["root"]}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 21, Part 2 Solution:");
        }

        private static string EvaluateExpression(string value, Dictionary<string, string> dict)
        {
            try
            {
                Convert.ToInt64(value);
                return value;
            }
            catch { }

            string operater = GetOperator(value);
            (string leftOperand, string rightOperand) = GetOperands(value);
            long leftValue, rightValue;
            try
            {
                leftValue = Convert.ToInt64(leftOperand);
            }
            catch 
            {
                leftOperand = EvaluateExpression(dict[leftOperand], dict);
                leftValue = Convert.ToInt64(leftOperand);
            }
            try
            {
                rightValue = Convert.ToInt64(rightOperand);
            }
            catch 
            {
                rightOperand = EvaluateExpression(dict[rightOperand], dict);
                rightValue = Convert.ToInt64(rightOperand);
            }
            return ExpressionToNumber(leftValue, rightValue, operater);
        }

        private static string GetOperator(string value)
        {
            if (value.Contains('+'))
                return "+";
            else if (value.Contains('-'))
                return "-";
            else if (value.Contains('*'))
                return "*";
            else
                return "/";
        }

        private static (string, string) GetOperands(string value)
        {
            char[] separators = new char[] { '+', '-', '*', '/' };
            string[] operands = value.Split(separators);
            string leftOperand = operands[0].Trim();
            string rightOperand = operands[1].Trim();

            return (leftOperand, rightOperand);
        }

        private static string ExpressionToNumber(long leftValue, long rightValue, string operater)
        {
            long value = 0; ;
            switch (operater)
            {
                case "+":
                    value = leftValue + rightValue;
                    break;
                case "-":
                    value = leftValue - rightValue;
                    break;
                case "*":
                    value = leftValue * rightValue;
                    break;
                case "/":
                    value = leftValue / rightValue;
                    break;
            }
            return value.ToString();
        }
    }
}
