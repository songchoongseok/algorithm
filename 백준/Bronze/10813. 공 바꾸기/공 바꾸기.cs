using System;

var inputs = Console.ReadLine()!.Split(" ");

var basketSize = Convert.ToInt32(inputs[0]);
var changeTestCount = Convert.ToInt32(inputs[1]);

var baskets = new Baskets(basketSize);
for (int testCount = 0; testCount < changeTestCount; testCount++)
{
    var basketNumbers = Console.ReadLine()!.Split(" "); //항상 할당하고 있음. 고민 필요

    baskets.ExchangeBall(Convert.ToInt32(basketNumbers[0]),
        Convert.ToInt32(basketNumbers[1]));
}

Console.WriteLine(baskets.ToString());

public class Baskets
{
    private List<int> _baskets;

    public Baskets(int basketSize)
    {
        _baskets = Enumerable.Range(1, basketSize).ToList();
    }

    public void ExchangeBall(int basketA, int basketB)
    {
        var ballOfBasketA = _baskets[basketA - 1];

        _baskets[basketA - 1] = _baskets[basketB - 1];
        _baskets[basketB - 1] = ballOfBasketA;
    }

    public override string ToString()
        => string.Join(" ", _baskets);
}