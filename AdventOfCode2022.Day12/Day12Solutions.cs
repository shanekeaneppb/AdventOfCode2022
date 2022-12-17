namespace AdventOfCode2022.Day12
{
    public class Day12Solutions
    {
        public static void Part1()
        {
            Node start, end;
            List<List<Node>> heightMap;
            (start, end, heightMap) = BuildHeightMap("input.txt");
            int pathLength = 0;
            if (start != null)
                pathLength = Search(start, end);
            Console.WriteLine($"Day 12, Part 1 Solution: {pathLength}");
        }
        public static void Part2()
        {
            Node start, end;
            List<List<Node>> heightMap;
            List<Node> starts = new();
            (start, end, heightMap) = BuildHeightMap("input.txt");
            List<int> pathLengths = new();
            foreach (var row in heightMap)
            {
                foreach (var node in row)
                {
                    if (node.Value == 1)
                        pathLengths.Add(Search(node, end));
                }
            }
            int shortestPath;
            shortestPath = pathLengths.Min();
            Console.WriteLine($"Day 12, Part 2 Solution: {shortestPath}");
        }

        private static (Node, Node, List<List<Node>>) ParseInputToPoints(string file)
        {
            List<List<Node>> heightMap = new List<List<Node>>();
            Node start = null, end = null;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day12/" + file))
            {
                string line;
                char[] charPoints;
                List<Node> currentLinePoints;
                Node currentNode;

                while ((line = reader.ReadLine()) != null)
                {
                    currentLinePoints = new List<Node>();
                    charPoints = line.ToArray();
                    foreach (char charPoint in charPoints)
                    {
                        currentNode = new Node();
                        if (charPoint == 'S')
                        {
                            start = currentNode;
                            currentNode.Value = 1;
                        }
                        else if (charPoint == 'E')
                        {
                            end = currentNode;
                            currentNode.Value = 26;
                        }
                        else
                            currentNode.Value = (int)charPoint - 96;

                        currentLinePoints.Add(currentNode);
                    }
                    heightMap.Add(currentLinePoints);
                }
            }
            return (start, end, heightMap);
        }

        private static (Node, Node, List<List<Node>>) BuildHeightMap(string file)
        {
            Node currentNode, start, end;
            (start, end, List<List<Node>> heightMap) = ParseInputToPoints(file);
            int i, j;
            for (i = 0; i < heightMap.Count; i++)
            {
                for (j = 0; j < heightMap[i].Count; j++)
                {
                    currentNode = heightMap[i][j];
                    currentNode.Position.X = i;
                    currentNode.Position.Y = j;
                    // Left Node
                    if (((j - 1) >= 0) && ((heightMap[i][j - 1].Value - currentNode.Value) <= 1))
                        currentNode.Neighbours.Add(heightMap[i][j - 1]);
                    // Right Node
                    if (((j + 1) < heightMap[i].Count) && ((heightMap[i][j + 1].Value - currentNode.Value) <= 1))
                        currentNode.Neighbours.Add(heightMap[i][j + 1]);

                    if (((i - 1) >= 0) && ((heightMap[i - 1][j].Value - currentNode.Value) <= 1))
                        currentNode.Neighbours.Add(heightMap[i - 1][j]);

                    if (((i + 1) < heightMap.Count) && ((heightMap[i + 1][j].Value - currentNode.Value) <= 1))
                        currentNode.Neighbours.Add(heightMap[i + 1][j]);
                }
            }
            return (start, end, heightMap);
        }

        private static int Search(Node start, Node end)
        {
            HashSet<Node> toVisit = new();
            HashSet<Node> visited = new();

            start.FromStart = 0;
            start.FromEnd = GetDistance(start, end);
            start.CameFrom = null;
            toVisit.Add(start);

            Node currentNode, temp;

            while (toVisit.Count > 0)
            {
                currentNode = toVisit.Min();
                toVisit.Remove(currentNode);
                visited.Add(currentNode);
                if (currentNode == end)
                    break;
                foreach (var neighbour in currentNode.Neighbours)
                {
                    if (visited.Contains(neighbour))
                        continue;

                    if (NewPathShorter(neighbour, currentNode, end) || !toVisit.Contains(neighbour))
                    {
                        neighbour.FromStart = currentNode.FromStart + 1;
                        neighbour.FromEnd = GetDistance(neighbour, end);
                        neighbour.FromSum = neighbour.FromStart + neighbour.FromEnd;
                        neighbour.CameFrom = currentNode;
                        toVisit.Add(neighbour);
                    }
                }
            }
            int pathLength = 0;
            currentNode = end;
            while (currentNode.CameFrom != null)
            {
                pathLength++;
                currentNode = currentNode.CameFrom;
            }
            return pathLength;
        }

        private static void UpdateDistances(Node currentNode, Node start, Node end)
        {
            currentNode.FromStart = GetDistance(currentNode, start);
            currentNode.FromEnd = GetDistance(currentNode, end);
            currentNode.FromSum = currentNode.FromStart + currentNode.FromEnd;

        }
        private static int GetDistance(Node current, Node source)
        {
            int xDist = Math.Abs(current.Position.X - source.Position.X);
            int yDist = Math.Abs(current.Position.Y - source.Position.Y);

            return xDist + yDist;
        }
        private static bool NewPathShorter(Node neighbour, Node currentNode, Node end)
        {
            int newFromSum = currentNode.FromSum + 1 + GetDistance(neighbour, end);

            if (newFromSum < neighbour.FromSum)
                return true;
            else if ((currentNode.FromStart + 1) < neighbour.FromStart)
                return true;
            return false;
        }

    }

}