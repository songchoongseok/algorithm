using System.Security.Principal;

namespace Algo;
public class Program
{
    static int n = 0;
    static int result = 99999;
    static int[] dx = { 1, 0, -1, 0 };
    static int[] dy = { 0, 1, 0, -1 };
    static int[,] map = new int[51, 51];
    static List<(int,int)> homes = new();

    static void Main(string[] args)
    {
        var chickens = new List<(int, int)>();

        var input = Console.ReadLine()!;
        n = Convert.ToInt32(input.Split(' ')[0]);
        var k = Convert.ToInt32(input.Split(' ')[1]);

        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine()!;

            var tokens = input.Split(' ');
            for (int j = 0; j < tokens.Count(); j++)
            {
                var value = Convert.ToInt32(tokens[j]);
                map[i, j] = value;

                if (value == 1)
                    homes.Add((i, j));
                else if (value == 2)
                    chickens.Add((i, j));
            }
        }

        SelectChickens(chickens, 0, 0, k);
        Console.WriteLine(result);
    }

    static int GetValue(int x, int y)
    {
        int[,] check = new int[51, 51];
        var q = new Queue<(int, int, int)>();
        q.Enqueue((x, y, 0));

        while (q.Count > 0)
        {
            var now = q.Peek();
            q.Dequeue();

            if (map[now.Item1, now.Item2] == 2)
                return now.Item3;

            for (int i = 0; i < 4; i++)
            {
                var nx = now.Item1 + dx[i];
                var ny = now.Item2 + dy[i];

                if (0 <= nx && nx < n && 0 <= ny && ny < n)
                {
                    if (check[nx, ny] == 1)
                        continue;

                    check[nx, ny] = 1;
                    q.Enqueue((nx, ny, now.Item3 + 1));
                }
            }
        }

        return 999999;
    }

    static void SelectChickens(List<(int, int)> chickens, int idx, int select, int k)
    {
        if (idx == chickens.Count && select == k)
        {
            var sum = 0;
            foreach(var home in homes)
                sum += GetValue(home.Item1, home.Item2);
            
            result = result > sum ? sum : result;
            return;
        }

        if(idx == chickens.Count)
            return;
            
        if(select < k )
            SelectChickens(chickens, idx + 1, select + 1, k);

        map[chickens[idx].Item1 , chickens[idx].Item2] = 0;
        SelectChickens(chickens, idx + 1, select, k);
        map[chickens[idx].Item1 , chickens[idx].Item2] = 2;
    }
}