using System.Collections.Generic;

namespace AdventOfCode2022.Day20
{
    public class Day20Solutions
    {
        public static void Part1()
        {
            int[] indices = new int[] { 1000, 2000, 3000 };
            List<int> list = LoadFile("input.txt");
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
            int index;
            int value;
            for(int i = 0; i < originalOrder.Length; i++)
            {
                value = originalOrder[i];
                index = list.IndexOf(value);
                if(value > 0)
                {
                    while(value > 0)
                    {
                        index = MoveRight(list, index);
                        value--;
                    }
                }
                else if (value < 0)
                {
                    value *= -1;
                    while (value > 0)
                    {
                        index = MoveLeft(list, index);
                        value--;
                    }
                }

            }
        }

        private static int MoveRight(List<int> list, int index)
        {
            int temp;
            if (index == list.Count - 2)
            {
                temp = list[index];
                for (int i = list.Count - 2; i > 0; i--)
                {
                    list[i] = list[i - 1];
                }
                list[0] = temp;
                return 0;
            }
            if (index == list.Count - 1)
            {
                temp = list[index];
                for (int i = list.Count - 1; i > 1; i--)
                {
                    list[i] = list[i - 1];
                }
                list[1] = temp;
                return 1;
            }
            temp = list[index];
            list[index] = list[index + 1];
            list[index + 1] = temp;
            return ++index;
        }
        private static int MoveLeft(List<int> list, int index)
        {
            int temp;
            if (index == 0)
            {
                temp = list[index];
                for (int i = 0; i < list.Count - 2; i++)
                {
                    list[i] = list[i + 1];
                }
                list[list.Count - 2] = temp;
                return list.Count - 2;
            }
            if (index == 1)
            {
                temp = list[index];
                for(int i = 1; i < list.Count - 1; i++)
                {
                    list[i] = list[i + 1];
                }
                list[list.Count - 1] = temp;
                return list.Count - 1;
            }
            temp = list[index];
            list[index] = list[index - 1];
            list[index - 1] = temp;
            return --index;
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