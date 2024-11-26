using System;

var numbers = Console.ReadLine()!.Split(" ");

var algorithm = new Algorithm();

Console.WriteLine(algorithm.Minus(Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1])));

public class Algorithm
{
    public int Minus(int a, int b) => a - b;

}