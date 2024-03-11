namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        int[,] d = new int[100001, 101];

        var input = Console.ReadLine()!;
        var n = Convert.ToInt32(input.Split(' ')[0]);
        var k = Convert.ToInt32(input.Split(' ')[1]);

        var datas = new List<(int, int)>();
        datas.Add((0, 0));
        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine()!;
            var w = Convert.ToInt32(input.Split(' ')[0]);
            var v = Convert.ToInt32(input.Split(' ')[1]);

            datas.Add((w, v));
        }

        for (int i = 1; i <= k; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (i < datas[j].Item1)
                    d[i, j] = d[i, j - 1];
                else
                    d[i, j] = Math.Max(d[i - datas[j].Item1, j - 1] + datas[j].Item2, d[i, j - 1]);
            }
        }

        Console.WriteLine(d[k, n]);
    }
}