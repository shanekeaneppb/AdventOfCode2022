using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day7
{
    class Directory
    {
        public string Name;
        public Directory Parent = null;
        public List<Directory> Directories = new List<Directory>();
        public int Size = 0;

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

        public static void Part11()
        {
            string cd = "$ cd ..";
            string ls = "$ ls";
            string dir = "dir hkl";
            string file = "62596 h.lst";
            Regex cdRegex = new Regex(@"[$] cd (\S+)");
            Regex lsRegex = new Regex(@"[$] ls");
            Regex dirRegex = new Regex(@"dir (\S+)");
            Regex fileRegex = new Regex(@"(\d+) \S+");
            Match match;
            match = cdRegex.Match(cd);
            var c = match.Groups[1];
            match = lsRegex.Match(ls);
            match = dirRegex.Match(dir);
            match = fileRegex.Match(file);
            Console.WriteLine();
        }
        public static void Part1()
        {
            List<string> lines = new List<string>();
            List<Directory> smallDirs = new List<Directory> ();
            string ln;
            int dirSize;
            int threshold = 100000;
            Directory root = new Directory();
            root.Name = "/";
            Directory cd = null, temp = null;
            Match match;

            Regex cdRegex = new Regex(@"[$] cd (\S+)");
            Regex lsRegex = new Regex(@"[$] ls");
            Regex dirRegex = new Regex(@"dir (\S+)");
            Regex fileRegex = new Regex(@"(\d+) (\S+)");

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day7/input.txt"))
            {
                
                while((ln = reader.ReadLine()) != null)
                {
                    lines.Add(ln);
                }
            }
            
            foreach(string line in lines)
            {
                //Console.WriteLine($"Line = {line}");
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
                        //Console.WriteLine($"dirSize = {dirSize}");
                        if(dirSize <= threshold)
                            smallDirs.Add(cd);
                        cd = cd.Parent;
                        cd.Size += dirSize;
                    }
                    else
                    {
                        temp = cd ?? new Directory();
                        cd = new Directory();
                        cd.Parent = temp;
                        cd.Name = match.Groups[1].ToString();
                    }
                }
                match = lsRegex.Match(line);
                if (match.Success)
                {
                    //Console.WriteLine();
                    continue;
                }
                match = dirRegex.Match(line);
                if (match.Success)
                {
                   // Console.WriteLine();
                    continue;
                }
                match = fileRegex.Match(line);
                if (match.Success)
                {
                    cd.Size += Convert.ToInt32(match.Groups[1].ToString());
                }
                //Console.WriteLine($"cd = {cd}");
                //Console.WriteLine();
            }
            while(cd.Parent != null)
            {
                dirSize = cd.Size;
                if (dirSize <= threshold)
                    smallDirs.Add(cd);
                cd = cd.Parent;
                cd.Size += dirSize;
            }


            int totalSize = 0;
            foreach (Directory dir in smallDirs)
                totalSize += dir.Size;
            Console.Write($"Day 7, Part 1 Solution: {totalSize}");
        }
        public static void Part2()
        {
            List<string> lines = new List<string>();
            List<Directory> directories = new List<Directory>();
            string ln;
            int dirSize;
            int totalSpace = 70000000;
            int requiredSpace = 30000000;
            int spaceToDelete;
            Directory root = new Directory();
            root.Name = "/";
            Directory cd = null, temp = null;
            Match match;

            Regex cdRegex = new Regex(@"[$] cd (\S+)");
            Regex lsRegex = new Regex(@"[$] ls");
            Regex dirRegex = new Regex(@"dir (\S+)");
            Regex fileRegex = new Regex(@"(\d+) (\S+)");

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day7/input.txt"))
            {

                while ((ln = reader.ReadLine()) != null)
                {
                    lines.Add(ln);
                }
            }

            foreach (string line in lines)
            {
                //Console.WriteLine($"Line = {line}");
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
                        //Console.WriteLine($"dirSize = {dirSize}");
                        if (dirSize <= threshold)
                            smallDirs.Add(cd);
                        cd = cd.Parent;
                        cd.Size += dirSize;
                    }
                    else
                    {
                        temp = cd ?? new Directory();
                        cd = new Directory();
                        cd.Parent = temp;
                        cd.Name = match.Groups[1].ToString();
                    }
                }
                match = lsRegex.Match(line);
                if (match.Success)
                {
                    //Console.WriteLine();
                    continue;
                }
                match = dirRegex.Match(line);
                if (match.Success)
                {
                    // Console.WriteLine();
                    continue;
                }
                match = fileRegex.Match(line);
                if (match.Success)
                {
                    cd.Size += Convert.ToInt32(match.Groups[1].ToString());
                }
                //Console.WriteLine($"cd = {cd}");
                //Console.WriteLine();
            }
            while (cd.Parent != null)
            {
                dirSize = cd.Size;
                if (dirSize <= threshold)
                    smallDirs.Add(cd);
                cd = cd.Parent;
                cd.Size += dirSize;
            }


            int totalSize = 0;
            foreach (Directory dir in smallDirs)
                totalSize += dir.Size;
            Console.Write($"Day 6, Part 2 Solution: ");
        }
    }
}