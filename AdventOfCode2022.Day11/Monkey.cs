using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day11
{
    public class Monkey
    {
        public static ulong WorrySuppressor;
        public static ulong GlobalModulus;
        public string Id;
        public Queue<ulong> WorryLevels = new Queue<ulong>();
        public string Operator;
        public string Increment;
        public ulong Divisor;
        public string TrueMonkey;
        public string FalseMonkey;
        public ulong TotalNumberOfInspections = 0;

        public Monkey(string id)
        {
            Id = id;
        }


        public ulong InspectWorryLevels()
        {
            TotalNumberOfInspections++;
            ulong worryLevel = WorryLevels.Dequeue();
            return worryLevel;
        }
        public ulong GetNewWorryLevel(ulong worryLevel)
        {
            ulong increment;
            if (Increment == "old")
                increment = worryLevel;
            else
                increment = Convert.ToUInt64(Increment);
            switch (Operator)
            {
                case ("+"):
                    worryLevel += increment;
                    break;
                case ("-"):
                    worryLevel -= increment;
                    break;
                case ("*"):
                    worryLevel *= increment;
                    break;
                case ("/"):
                    worryLevel /= increment;
                    break;
            }

            return (worryLevel / WorrySuppressor) % GlobalModulus;
        }

        public bool Test(ulong worryLevel)
        {
            return worryLevel % Divisor == 0;
        }

        public void ThrowAllItems(Dictionary<string, Monkey> toMonkeys)
        {
            ulong currentWorryLevel, newWorryLevel;
            while (WorryLevels.Count != 0)
            {
                currentWorryLevel = InspectWorryLevels();
                newWorryLevel = GetNewWorryLevel(currentWorryLevel);
                if (Test(newWorryLevel))
                    toMonkeys[TrueMonkey].WorryLevels.Enqueue(newWorryLevel);
                else
                    toMonkeys[FalseMonkey].WorryLevels.Enqueue(newWorryLevel);
            }
        }
    }
}
