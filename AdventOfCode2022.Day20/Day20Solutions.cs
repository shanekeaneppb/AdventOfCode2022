using System.Collections.Generic;

namespace AdventOfCode2022.Day20
{
    public class Day20Solutions
    {
        public static void Part1()
        {
            int[] indices = new int[] { 1000, 2000, 3000 };
            List<int> list = LoadFile("test.txt");
            // 3859 is incorrect. Answer is too high
            // 1769 is incorrect. Answer is too low
            int[] originalOrder = list.ToArray();

            DecryptMessage(list, originalOrder);

            int groveCoordinatesSum = GetGroveCoordinatesSum(list, indices);

            Console.WriteLine($"Day 20, Part 1 Solution: {groveCoordinatesSum}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 20, Part 2 Solution: ");
        }

        private static List<int> LoadFile(string file)
        {
            List<int> list;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day20/" + file))
            {
                list = reader.ReadToEnd().Trim().Split("\r\n").Select(p => Convert.ToInt32(p)).ToList();
            }
            return list;
        }

        private static void DecryptMessage(List<int> list, int[] originalOrder)
        {
            int length = originalOrder.Length;
            int currentPosition;
            int currentNumber;
            int newPosition;
            int multiplier;
            for(int i = 0; i < length; i++)
            {
                currentNumber = originalOrder[i];
                currentPosition = list.IndexOf(currentNumber);
                multiplier = Math.Abs(currentNumber/length);
                newPosition = ((currentPosition + currentNumber) + (multiplier + 1)*length) % length;

                // Moving right
                if (currentNumber > 0)
                {
                    if (newPosition == length - 1)
                    {
                        list.Insert(0, currentNumber);
                        list.RemoveAt(currentPosition + 1);
                    }
                    else if (newPosition == 0)
                    {
                        list.Insert(1, currentNumber);
                        list.RemoveAt(currentPosition + 1);
                    }
                    else if (newPosition > currentPosition)
                    {
                        list.Insert(newPosition + 1, currentNumber);
                        list.RemoveAt(currentPosition);
                    }
                    else if (newPosition < currentPosition)
                    {
                        list.Insert(newPosition + 1, currentNumber);
                        list.RemoveAt(currentPosition + 1);
                    }
                }

                // Moving left
                if (currentNumber < 0)
                {
                    if (newPosition == length - 1)
                    {
                        list.Insert(length - 1, currentNumber);
                        list.RemoveAt(currentPosition);
                    }
                    else if (newPosition == 0)
                    {
                        list.Add(currentNumber);
                        list.RemoveAt(currentPosition);
                    }
                    else if (newPosition > currentPosition)
                    {
                        list.Insert(newPosition, currentNumber);
                        list.RemoveAt(currentPosition);
                    }
                    else if (newPosition < currentPosition)
                    {
                        list.Insert(newPosition, currentNumber);
                        list.RemoveAt(currentPosition + 1);
                    }
                }
            }
        }
        private static int GetGroveCoordinatesSum(List<int> list, int[] indices)
        {
            int zeroIndex = list.IndexOf(0);
            int groveCoordinatesSum = 0;
            foreach(int index in indices)
                groveCoordinatesSum += list[(zeroIndex + index)%list.Count];
            return groveCoordinatesSum;
        }
    }
}


//if ((currentPosition + currentNumber < 0))// 
//{
//    list.RemoveAt(currentPosition);
//    list.Insert(newPosition, currentNumber);
//}
//else if ((currentPosition + currentNumber) > length)
//{
//    list.Insert(
//        wPosition + 1, currentNumber);
//    list.RemoveAt(currentPosition + 1);
//}
//else if (newPosition == 0)
//{
//    list.Add(currentNumber);
//    list.RemoveAt(currentPosition);
//}
//else if (newPosition == length)
//{
//    list.Insert(0, currentNumber);
//    list.RemoveAt(currentPosition);
//}
//else
//{
//    list.RemoveAt(currentPosition);
//    list.Insert(newPosition, currentNumber);
//}