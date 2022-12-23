﻿using System.Collections.Generic;

namespace AdventOfCode2022.Day20
{
    public class Day20Solutions
    {
        public static void Part1()
        {
            int[] indices = new int[] { 1000, 2000, 3000 };
            (MyLinkedList list, List<int> originalOrder) = LoadFile("test.txt");
            // 3859 is incorrect. Answer is too high
            // 1769 is incorrect. Answer is too low
            
            DecryptMessage(list, originalOrder);
            Console.WriteLine(list);
            //int groveCoordinatesSum = GetGroveCoordinatesSum(list, indices);

            //Console.WriteLine($"Day 20, Part 1 Solution: {groveCoordinatesSum}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 20, Part 2 Solution: ");
        }

        private static (MyLinkedList, List<int>) LoadFile(string file)
        {
            List<int> list;
            MyLinkedList myList = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day20/" + file))
            {
                list = reader.ReadToEnd().Trim().Split("\r\n").Select(p => Convert.ToInt32(p)).ToList();
            }
            foreach (int i in list)
            {
                myList.Add(i);
            }
            return (myList, list);
        }

        private static void DecryptMessage(MyLinkedList list, List<int> originalOrder)
        {
            int length = list.Length;
            int currentNumber;
            int stepsToMove;
            //Console.WriteLine(list);
            for (int i = 0; i < length; i++)
            {
                currentNumber = originalOrder[i];
                list.Move(currentNumber);
                //Console.WriteLine(list);
                //Console.WriteLine();
            }
        }
        private static int GetGroveCoordinatesSum(List<int> list, int[] indices)
        {
            int zeroIndex = list.IndexOf(0);
            int groveCoordinatesSum = 0;
            foreach (int index in indices)
                groveCoordinatesSum += list[(zeroIndex + index) % list.Count];
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