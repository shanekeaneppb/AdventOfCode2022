using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day16
{
    public class Valve
    {
        public string Name = null;
        public int FlowRate;
        public List<Valve> Neighbours;

        public Valve()
        {
            Neighbours = new();
        }

        public Valve(string name, int flowRate)
        {
            Name = name;
            FlowRate = flowRate;
            Neighbours = new();
        }

        public void AddNeighbours(string[] neighbourNames, Dictionary<string, Valve> existingValves)
        {
            string neighbour;
            foreach(string n in neighbourNames)
            {
                neighbour = n.Trim();
                if(existingValves.ContainsKey(neighbour))
                    this.Neighbours.Add(existingValves[neighbour]);
                else
                {
                    existingValves.Add(neighbour, new Valve());
                    this.Neighbours.Add(existingValves[neighbour]);
                }
            }
        }
    }

    
}
