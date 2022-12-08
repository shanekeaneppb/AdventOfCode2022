using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day7
{
    class Directory : IComparable<Directory>
    {
        public string Name;
        public Directory Parent = null;
        public List<Directory> Directories = new List<Directory>();
        public int Size = 0;

        public int CompareTo(Directory other)
        {
            return this.Size - other.Size;
        }

        public override string ToString()
        {
            string parentName = "";
            if(this.Parent != null)
                parentName = this.Parent.Name;
            return $"{Name}: (size={Size}, parent={parentName})";
        }

        
    }

    public class Day7Solutions
    {
        public static void Part1()
        {
            int threshold = 100000;
            int totalSize = 0;
            var directories = BuildDirectoryTree("input.txt");
            foreach(int size in (directories.Select(dir => dir.Size).Where(s => s < threshold)))
                totalSize += size;
            Console.Write($"Day 7, Part 1 Solution: {totalSize}");
        }
        public static void Part2()
        {
            int totalSpace = 70000000;
            int requiredSpace = 30000000;
            int dirSize, availableSpace, spaceToDelete;
            Directory directoryToDelte = null;

            List<Directory> directories = BuildDirectoryTree("input.txt");

            directories.Sort();
            // Since directories are now sorted, this takes the size of the root directory from the total space available
            availableSpace = totalSpace - directories[directories.Count-1].Size;
            spaceToDelete = requiredSpace - availableSpace;

            foreach (Directory dir in directories)
            {
                if (dir.Size >= spaceToDelete)
                {
                    directoryToDelte = dir;
                    break;
                }

            }
            // Checks that a directory was actually selected
            if (directoryToDelte == null)
                directoryToDelte = directories[directories.Count - 1];

            Console.Write($"Day 7, Part 1 Solution: {directoryToDelte.Size}");
        }
        
        private static List<Directory> BuildDirectoryTree(string inputFile)
        {
            List<Directory> directories = new List<Directory>();
            int dirSize;
            // setup the root directory
            Directory root = new Directory();
            root.Name = "/";
            directories.Add(root);
            Directory cd = null, temp = null;
            Match match;
            // Regex expression to match different commands (dir and ls commands can just be skipped)
            Regex cdRegex = new Regex(@"[$] cd (\S+)");
            Regex fileRegex = new Regex(@"(\d+) (\S+)");

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day7/" + inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    match = cdRegex.Match(line);
                    if (match.Success)
                    {
                        if (match.Groups[1].ToString() == "/")
                        {
                            cd = root;
                        }
                        else if (match.Groups[1].ToString() == "..")
                        {
                            dirSize = cd.Size;
                            cd = cd.Parent;
                            cd.Size += dirSize;
                        }
                        else
                        {
                            temp = cd ?? new Directory();
                            cd = new Directory();
                            directories.Add(cd);
                            cd.Parent = temp;
                            cd.Name = match.Groups[1].ToString();
                        }
                    }
                    match = fileRegex.Match(line);
                    if (match.Success)
                    {
                        cd.Size += Convert.ToInt32(match.Groups[1].ToString());
                    }
                }
            }
            // This travels back up the tree to root, updating the size of any enclosing directories missed above
            while (cd.Parent != null)
            {
                dirSize = cd.Size;
                cd = cd.Parent;
                cd.Size += dirSize;
            }

            return directories;
        }
    }
}