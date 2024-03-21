using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine()!;
        int n = Convert.ToInt32(input.Split(' ')[0]);
        int k = Convert.ToInt32(input.Split(' ')[1]);

        input = Console.ReadLine()!;
        var tokens = input.Split(' ');
        var belt = new List<int>();
        var robot = new List<int>();
        foreach (var token in tokens)
        {
            belt.Add(Convert.ToInt32(token));
            robot.Add(0);
        }

        int idx = 0;
        while (k > 0)
        {
            MoveBelt(belt, robot, n);
            k -= MoveRobot(belt, robot, n);
            k -= UpRobot(belt, robot);

            idx++;
        }

        Console.WriteLine(idx);
    }

    static int UpRobot(List<int> belt, List<int> robot)
    {
        if (belt[0] > 0)
        {
            belt[0]--;
            robot[0] = 1;

            return belt[0] == 0 ? 1 : 0;    
        }

        return 0;
    }

    static int MoveRobot(List<int> belt, List<int> robot, int n)
    {
        var zeroCnt = 0;
        robot[n - 1] = 0;

        for (int i = n - 2; i >= 0; i--)
        {
            if (belt[i + 1] > 0 && robot[i + 1] == 0 && robot[i] == 1)
            {
                belt[i + 1]--;

                robot[i] = 0;
                robot[i + 1] = 1;

                if (belt[i + 1] == 0)
                    zeroCnt++;
            }
        }

        robot[n - 1] = 0;
        return zeroCnt;
    }

    static void MoveBelt(List<int> belt, List<int> robot, int n)
    {
        var x = belt[2 * n - 1];
        belt.Insert(0, x);
        belt.RemoveAt(2 * n);

        x = robot[2 * n - 1];
        robot.Insert(0, x);
        robot.RemoveAt(2 * n);
    }
}