using System.ComponentModel.Design;

namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine()!;
        int n = Convert.ToInt32(input);

        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine()!;
            int idx = 0;
            var ret = "YES";
            var isOneZeroZero = false;
            while (input.Length > idx)
            {
                if (CheckZeroOne(input, idx))
                {
                    isOneZeroZero = false;
                    idx += 2;
                }
                else if (CheckOneZeroZero(input, idx))
                {
                    isOneZeroZero = true;
                    idx += 3;

                    var value = CheckSubOneZeroZero(input, idx);
                    if (value == -1)
                    {
                        ret = "NO";
                        break;
                    }

                    idx = value;
                }
                else if(isOneZeroZero && idx - 2 >= 0 && input[idx - 2] =='1')
                {
                    idx--;
                }
                else
                {
                    ret = "NO";
                    break;
                }
            }

            Console.WriteLine(ret);
        }
    }

    static bool CheckZeroOne(string str, int idx)
    {
        if (idx + 2 > str.Length)
            return false;

        var sub = str.Substring(idx, 2);

        if (sub == "01")
            return true;

        return false;
    }

    static bool CheckOneZeroZero(string str, int idx)
    {
        if (idx + 3 > str.Length)
            return false;

        var sub = str.Substring(idx, 3);
        if (sub == "100")
            return true;

        return false;
    }

    static int CheckSubOneZeroZero(string str, int idx)
    {
        while (str.Length > idx && str[idx] == '0')
            idx++;

        int pre = idx;
        while (str.Length > idx && str[idx] == '1')
            idx++;

        return pre < idx ? idx : -1;
    }
}