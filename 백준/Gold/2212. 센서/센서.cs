using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        int k = Convert.ToInt32(Console.ReadLine());

        var datas = SplitDatas(Console.ReadLine()!);

        var diff = new List<int>();
        int hap = 0;
        for (int i = 0; i < n - 1; i++)
        {
            diff.Add(datas[i + 1] - datas[i]);
            hap += datas[i + 1] - datas[i];
        }

        diff.Sort();
        int idx = diff.Count - 1;
        for (int i = 0; i < k - 1; i++)
            if(idx >= 0)
                hap -= diff[idx--];

        Console.WriteLine(hap);
    }

    static List<int> SplitDatas(string datas)
    {
        var splittedDatas = new List<int>();
        var tokens = datas.Split(' ');
        foreach (var token in tokens)
            splittedDatas.Add(Convert.ToInt32(token));

        splittedDatas.Sort();
        return splittedDatas;
    }
}