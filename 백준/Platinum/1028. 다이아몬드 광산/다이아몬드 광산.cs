using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Algo;
public class Program
{
    static Queue<(int, int, int, int, bool)> queue = new Queue<(int, int, int, int, bool)>();

    static void Main(string[] args)
    {
        var result = 0;
        var input = Console.ReadLine();

        var x = Convert.ToInt32(input!.Split(" ")[0]);
        var y = Convert.ToInt32(input!.Split(" ")[1]);
        var maxSize = x < y ? (x + 1) / 2 : (y + 1) / 2;

        int[,] map = new int[750, 750];
        for (int i = 0; i < x; i++)
        {
            input = Console.ReadLine();
            for (int j = 0; j < input!.Length; j++)
            {
                if (input[j] == '1')
                    map[i, j] = 1;
            }
        }

        // Console.WriteLine("maxSize: " + maxSize);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (map[i, j] == 1)
                {
                    var size = CheckSize(map, x, y, i, j, result);
                    result = size > result ? size : result;

                    if(maxSize == result)
                    {
                        Console.WriteLine(result);
                        return;
                    }

                }
            }
        }

        Console.WriteLine(result);
    }

    static int CheckSize(int[,] map, int x, int y, int posX, int posY, int result)
    {
        queue.Clear();
        queue.Enqueue((posX, posY, posY, 1, true));
        queue.Enqueue((posX, posY, posY, 1, false));

        var maxSize = 0;
        while (queue.Count > 0)
        {
            var now = queue.Peek();
            queue.Dequeue();

            if (now.Item2 == now.Item3 && now.Item5)
            {
                maxSize = maxSize < now.Item4 ? now.Item4 : maxSize;
                continue;
            }

            if (!now.Item5)
            {
                var nextX = now.Item1 + 1;
                var ly = now.Item2 - 1;
                var ry = now.Item3 + 1;

                if (nextX >= x || ly < 0 || ry >= y)
                    continue;

                if (map[nextX, ly] == 0 || map[nextX, ry] == 0)
                    continue;

                if(result < now.Item4 + 1)
                    queue.Enqueue((nextX, ly, ry, now.Item4 + 1, true));
                queue.Enqueue((nextX, ly, ry, now.Item4 + 1, false));
            }
            else
            {
                var nextX = now.Item1 + 1;
                var ly = now.Item2 + 1;
                var ry = now.Item3 - 1;

                if (nextX >= x)
                    continue;

                if (map[nextX, ly] == 0 || map[nextX, ry] == 0)
                    continue;

                queue.Enqueue((nextX, ly, ry, now.Item4, true));
            }
        }

        return maxSize;
    }
}