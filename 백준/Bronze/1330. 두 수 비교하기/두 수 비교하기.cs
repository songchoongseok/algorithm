namespace Algo;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine()!;
        
        var a = Convert.ToInt32(input.Split(' ')[0]);
        var b = Convert.ToInt32(input.Split(' ')[1]);

        var result = a > b ? ">" : a < b ? "<" : "==";
        Console.WriteLine(result);
    }
}