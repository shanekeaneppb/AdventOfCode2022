using System.Numerics;

namespace AdventOfCode2022.Day25
{
    public class Day25Solutions
    {
        public static void Part1()
        {
            var snafuNumbers = LoadInput("input.txt");
            var decimalNumbers = ConvertToDecimal(snafuNumbers);
            BigInteger sum = 0;
            foreach (BigInteger i in decimalNumbers)
            {
                sum += i;
            }
            string snafuNumber = ConvertToSNAFU(sum);
            Console.WriteLine($"Day 25, Part 1 Solution: {snafuNumber}");

            // 11=-2122--10-=- incorrect
            // 11=-2122--10-=-
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

        private static BigInteger ConvertToDecimal(string snafuNumber)
        {
            BigInteger decimalNumber = 0;
            char c;
            for(int i = 0; i < snafuNumber.Length; i++)
            {
                c = snafuNumber[snafuNumber.Length - (i+1)];
                switch (c)
                {
                    case '1':
                        decimalNumber += BigInteger.Pow(5, i);
                        break;
                    case '2':
                        decimalNumber += 2* BigInteger.Pow(5, i);
                        break;
                    case '-':
                        decimalNumber -= BigInteger.Pow(5, i);
                        break;
                    case '=':
                        decimalNumber -= 2 * BigInteger.Pow(5, i);
                        break;
                }
            }  
            return decimalNumber;
        }

        private static List<BigInteger> ConvertToDecimal(List<string> snafuNumbers)
        {
            List<BigInteger> decimalNumbers = new();
            foreach(var snafuNumber in snafuNumbers)
                decimalNumbers.Add(ConvertToDecimal(snafuNumber));
            return decimalNumbers;
        }

        private static string ConvertToSNAFU(BigInteger decimalNumber, Dictionary<string, string> coeffs = null)
        {
            int i = 0;
            BigInteger n, remainder;
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
            BigInteger d, m;
            while (true)
            {
                m = MaxNumber(i);
                d = BigInteger.Abs(decimalNumber) - m; 
                if (MaxNumber(i) >= BigInteger.Abs(decimalNumber))
                    break;
                i++;
            }
            n = BigInteger.Pow(5, i);

            if(decimalNumber > 0)
            {
                remainder = decimalNumber - n;
                if(MaxNumber(i - 1) >= BigInteger.Abs(remainder))
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
                if (MaxNumber(i - 1) >= BigInteger.Abs(remainder))
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

        private static BigInteger MaxNumber(int i)
        {
            if(i < 0)
                return 0;
            BigInteger maxNumber = 0;
            for (; i >= 0; i--)
            {
                maxNumber += 2*BigInteger.Pow(5, i);
            }
            return maxNumber;
        }

    }
}