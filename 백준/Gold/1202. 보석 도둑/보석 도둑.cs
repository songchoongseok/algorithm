using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine();

        var n = Convert.ToInt32(input.Split(" ")[0]);
        var k = Convert.ToInt32(input.Split(" ")[1]);

        var gems = new List<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine();

            var m = Convert.ToInt32(input.Split(" ")[0]);
            var v = Convert.ToInt32(input.Split(" ")[1]);
            gems.Add((m, v));
        }

        var bags = new List<int>();
        for (int i = 0; i < k; i++)
        {
            input = Console.ReadLine();

            var c = Convert.ToInt32(input);
            bags.Add(c);
        }

        gems = gems.OrderBy(x => (x.Item1, x.Item2)).ToList();
        bags = bags.OrderBy(x => x).ToList();

        long sum = 0;
        var idx = 0;
        var pq = new PriorityQueue<int, int>();
        for(int i=0; i< bags.Count; i++)
        {
            var bag = bags[i];
            while (idx < n && bag >= gems[idx].Item1)
            {
                pq.Enqueue(gems[idx].Item2, -gems[idx].Item2);
                idx++;
            }

            if (pq.Count > 0)
            {
                sum += pq.Peek();
                pq.Dequeue();
            }
        }

        Console.WriteLine(sum);
    }
}