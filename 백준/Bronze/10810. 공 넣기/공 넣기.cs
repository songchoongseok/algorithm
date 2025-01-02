using System;

var inputs = Console.ReadLine()!.Split(" ");

int size = Convert.ToInt32(inputs[0]);
int workingCount = Convert.ToInt32(inputs[1]);

var algorithm = new Algorithm(size);
for(int i=0; i<workingCount; i++)
{
    inputs = Console.ReadLine()!.Split(" ");

    algorithm.PutInBall(Convert.ToInt32(inputs[0]), Convert.ToInt32(inputs[1]), Convert.ToInt32(inputs[2]));
}

Console.WriteLine(algorithm.GetBasketToString());

public class Algorithm
{
    private List<int> _basket;

    public Algorithm(int size)
    {
        _basket = Enumerable.Repeat(0, size).ToList();
    }

    public void PutInBall(int start, int end, int number)
    {
        for(int i=start-1; i<end; i++)
            _basket[i] = number;
        
    }

    public string GetBasketToString()
        => string.Join(" ", _basket);
}