var boardSize = 5;

var boardNumbers = InputBoardNumbers(boardSize);
var callNumbers = InputCallNumbers(boardSize);

var bingo = new Bingo(boardSize, boardNumbers);
for (int repeatCount = 0; repeatCount < callNumbers.Count; repeatCount++)
{
    bingo.RemoveNumber(callNumbers[repeatCount]);

    if (bingo.IsBingo())
    {
        Console.WriteLine(bingo.CallBingo());
        break;
    }

}

static List<int> InputBoardNumbers(int boardSize)
{
    var boardNumbers = new List<int>();
    for (int size = 0; size < boardSize; size++)
        boardNumbers.AddRange(Console.ReadLine()!.Split().Select(x => Convert.ToInt32(x)));

    return boardNumbers;
}

static List<int> InputCallNumbers(int boardSize)
{
    var callNumbers = new List<int>();
    for (int size = 0; size < boardSize; size++)
        callNumbers.AddRange(Console.ReadLine()!.Split().Select(x => Convert.ToInt32(x)));

    return callNumbers;
}

public class Bingo
{
    private int _boardSize;
    private List<List<int>> _board = new();
    private int _removeCount = 0;

    public Bingo(int boardSize, List<int> boardNumbers)
    {
        _boardSize = boardSize;

        CreateBoard(boardNumbers);
    }

    private void CreateBoard(List<int> boardNumbers)
    {
        _board = new List<List<int>>();

        var numberIndex = 0;
        for (int size = 0; size < _boardSize; size++)
        {
            _board.Add(new());

            var repeatCount = _boardSize;
            while (repeatCount > 0)
            {
                _board[size].Add(boardNumbers[numberIndex++]);
                repeatCount--;
            }
        }
    }

    public void RemoveNumber(int number)
    {
        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                if (_board[i][j] == number)
                {
                    _removeCount++;
                    _board[i][j] = -1;
                    break;
                }
            }
        }
    }

    public bool IsBingo()
    {
        var bingoCount = CheckHorizontalBingo();
        bingoCount += CheckVerticalBingo();
        bingoCount += CheckDiagonalBingo();

        return bingoCount >= 3;
    }

    public int CallBingo()
        => _removeCount;

    private int CheckDiagonalBingo()
    {
        var bingoCount = 0;

        var bingo = true;
        for (int i = 0; i < _boardSize; i++)
        {
            if (_board[i][i] != -1)
            {
                bingo = false;
                break;
            }
        }
        bingoCount = bingo ? bingoCount + 1 : bingoCount;

        bingo = true;
        for (int i = 0; i < _boardSize; i++)
        {
            if (_board[i][_boardSize - i - 1] != -1)
            {
                bingo = false;
                break;
            }
        }
        bingoCount = bingo ? bingoCount + 1 : bingoCount;

        return bingoCount;
    }

    private int CheckVerticalBingo()
    {
        var bingoCount = 0;
        for (int i = 0; i < _boardSize; i++)
        {
            var bingo = true;
            for (int j = 0; j < _boardSize; j++)
            {
                if (_board[j][i] != -1)
                {
                    bingo = false;
                    break;
                }
            }

            bingoCount = bingo ? bingoCount + 1 : bingoCount;
        }

        return bingoCount;
    }

    private int CheckHorizontalBingo()
    {
        var bingoCount = 0;
        for (int i = 0; i < _boardSize; i++)
        {
            var bingo = true;
            for (int j = 0; j < _boardSize; j++)
            {
                if (_board[i][j] != -1)
                {
                    bingo = false;
                    break;
                }
            }

            bingoCount = bingo ? bingoCount + 1 : bingoCount;
        }

        return bingoCount;
    }
}