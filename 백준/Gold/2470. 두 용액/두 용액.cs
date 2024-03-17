using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine();
        int n = Convert.ToInt32(input);

        input = Console.ReadLine()!;
        var values = input.Split(' ');

        var listA = new List<int>();
        foreach (var value in values)
        {
            var x = Convert.ToInt32(value);
            listA.Add(x);

        }
        listA.Sort();

        var results = Solve(listA);
        results.Sort();
        Console.WriteLine($"{results[0]} {results[1]}");
    }

    static List<int> Solve(List<int> listA)
    {
        int l = 0;
        int r = listA.Count - 1;
        int ret = Int32.MaxValue;

        var results = new List<int>();
        while (l < r)
        {
            var hap = Math.Abs(listA[l] + listA[r]);
            if (ret > hap)
            {
                ret = hap;
                results = new List<int>() { listA[l], listA[r] };
            }

            if(Math.Abs(listA[l]) < listA[r])
                r--;
            else 
                l++;
        }

        return results;
    }
}