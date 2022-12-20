namespace AdventOfCode2022.Day16
{
    public partial class Day16Solutions
    {
        public class ValveInfo
        {
            public int Distance;
            public int FlowRate;

            public ValveInfo()
            { }
            public ValveInfo(int distance, int flowRate)
            {
                Distance = distance;
                FlowRate = flowRate;
            }
        }
    }
}