namespace AdventOfCode2022.Day25
{
    public class Day25Solutions
    {
        public static void Part1()
        {
            var snafuNumbers = LoadInput("input.txt");
            var decimalNumbers = ConvertToDecimal(snafuNumbers);
            long sum = decimalNumbers.Sum();
            string snafuNumber = ConvertToSNAFU(sum);
            Console.WriteLine($"Day 25, Part 1 Solution: {snafuNumber}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 25, Part 2 Solution: ");
        }

        public static List<string> LoadInput(string file)
        {
            List<string> input = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day25/" + file))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                    input.Add(line);
            }
            return input;
        }

        private static int ConvertToDecimal(string snafuNumber)
        {
            int decimalNumber = 0;
            char c;
            for(int i = 0; i < snafuNumber.Length; i++)
            {
                c = snafuNumber[snafuNumber.Length - (i+1)];
                switch (c)
                {
                    case '1':
                        decimalNumber += (int)Math.Pow(5, i);
                        break;
                    case '2':
                        decimalNumber += 2*(int)Math.Pow(5, i);
                        break;
                    case '-':
                        decimalNumber -= (int)Math.Pow(5, i);
                        break;
                    case '=':
                        decimalNumber -= 2 * (int)Math.Pow(5, i);
                        break;
                }
            }  
            return decimalNumber;
        }

        private static List<int> ConvertToDecimal(List<string> snafuNumbers)
        {
            List<int> decimalNumbers = new();
            foreach(var snafuNumber in snafuNumbers)
                decimalNumbers.Add(ConvertToDecimal(snafuNumber));
            return decimalNumbers;
        }

        private static string ConvertToSNAFU(long decimalNumber, Dictionary<string, string> coeffs = null)
        {
            int i = 0;
            long n, remainder;
            coeffs = (coeffs == null) ? new() : coeffs;

            if (decimalNumber == 0)
            {
                string snafuNumber = "", num;
                for(i = coeffs.Keys.Select(x => Convert.ToInt32(x)).Max(); i >= 0 ; i--)
                {
                    num = i.ToString();
                    snafuNumber += coeffs.ContainsKey(num) ? coeffs[num] : "0";
                }
                return snafuNumber;
            }
 
            while (true)
            {
                if (MaxNumber(i) >= Math.Abs(decimalNumber))
                    break;
                i++;
            }
            n = (int)Math.Pow(5, i);

            if(decimalNumber > 0)
            {
                remainder = decimalNumber - n;
                if(MaxNumber(i - 1) >= Math.Abs(remainder))
                {
                    coeffs.Add(i.ToString(), "1");
                    return ConvertToSNAFU(remainder, coeffs);
                }
                else
                {
                    coeffs.Add(i.ToString(), "2");
                    remainder = decimalNumber - 2*n;
                    return ConvertToSNAFU(remainder, coeffs);
                }
            }
            else
            {
                remainder = decimalNumber + n;
                if (MaxNumber(i - 1) >= Math.Abs(remainder))
                {
                    coeffs.Add(i.ToString(), "-");
                    return ConvertToSNAFU(remainder, coeffs);
                }
                else
                {
                    coeffs.Add(i.ToString(), "=");
                    remainder = decimalNumber + 2 * n;
                    return ConvertToSNAFU(remainder, coeffs);
                }
            }
        }

        private static int MaxNumber(int i)
        {
            if(i < 0)
                return 0;
            int maxNumber = 0;
            for (; i >= 0; i--)
            {
                maxNumber += 2*(int)Math.Pow(5, i);
            }
            return maxNumber;
        }

    }
}