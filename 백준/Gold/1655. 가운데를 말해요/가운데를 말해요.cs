using System.Collections.Generic;

namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        var result = new List<int>();

        int n = Convert.ToInt32(Console.ReadLine()!);

        var datas = new List<int>();
        for (int i = 0; i < n; i++)
        {
            var input = Convert.ToInt32(Console.ReadLine()!);
            AddElement(datas, input);

            var idx = datas.Count % 2 == 0 ? datas.Count / 2 - 1 : datas.Count / 2;
            result.Add(datas[idx]);
        }
        var x = String.Join("\n", result);
        Console.WriteLine(x);
        // foreach (var x in result)
        //     Console.WriteLine(x);
    }

    static void AddElement(List<int> datas, int input)
    {
        int l = 0;
        int r = datas.Count - 1;

        var m = 0;
        while (l <= r)
        {
            m = (l + r) / 2;

            if (datas[m] < input)
                l = m + 1;
            else
                r = m - 1;
        }

        datas.Insert(l, input);
    }
}