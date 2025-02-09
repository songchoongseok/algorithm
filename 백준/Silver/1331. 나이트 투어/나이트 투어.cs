using System.Reflection.Metadata;

var inputCount = 36;
var knightMovings = new List<string>();

for (int i = 0; i < inputCount; i++)
    knightMovings.Add(Console.ReadLine()!);

var knight = new Knight(knightMovings[0]);
var chessBoard = new ChessBoard(knight);

var isValid = true;
for (int i = 1; i < inputCount; i++)
{
    if (!chessBoard.MoveKnight(knightMovings[i]))
    {
        isValid = false;
        break;
    }
}



Console.WriteLine(isValid && knight.Move(knightMovings[0]) ? "Valid" : "Invalid");

public class ChessBoard
{
    private readonly int CHECKED = 1;
    private readonly int SIZE = 6;

    private int[,] _board;
    private Knight _knight;

    public ChessBoard(Knight knight)
    {
        _board = new int[SIZE, SIZE];
        _knight = knight;

        PutKnight(_knight);
    }

    public bool MoveKnight(string knightMoving)
    {
        if (_knight.Move(knightMoving) && PutKnight(_knight))
            return true;

        return false;
    }

    private bool PutKnight(Knight knight)
    {
        if (_board[knight.CurrentPos.X, knight.CurrentPos.Y] == CHECKED)
            return false;

        _board[knight.CurrentPos.X, knight.CurrentPos.Y] = CHECKED;
        return true;
    }
}

public class Knight
{
    private int[] _dx = new int[] { 1, -1, 2, -2, 1, -1, 2, -2 };
    private int[] _dy = new int[] { 2, 2, 1, 1, -2, -2, -1, -1 };

    public record Position(int X, int Y);
    public Position CurrentPos;

    public Knight(string knightMoving)
    {
        CurrentPos = ConvertToPosition(knightMoving);
    }

    public bool Move(string knightMoving)
    {
        var nextPos = ConvertToPosition(knightMoving);
        if (!ValidateMove(nextPos))
            return false;

        CurrentPos = nextPos;

        return true;
    }

    private bool ValidateMove(Position nextPos)
    {
        for (int i = 0; i < _dx.Length; i++)
            if (nextPos == new Position(CurrentPos.X + _dx[i], CurrentPos.Y + _dy[i]))
                return true;

        return false;
    }

    private Position ConvertToPosition(string knightMoving)
    {
        var x = Convert.ToInt32(knightMoving[0] - 'A');
        var y = Convert.ToInt32(knightMoving[1] - '1');

        return new(x, y);
    }
}