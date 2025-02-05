var boardSize = Console.ReadLine()!
        .Split()
        .Select(x => Convert.ToInt32(x))
        .ToList();

var board = InputBoard(boardSize[0]);

var chessBoard = new ChessBoard(board);
var service = new CreateChessBoardService();

Console.WriteLine(service.GetMinDrawCount(chessBoard));

static List<string> InputBoard(int rows)
{
    var board = new List<string>();
    while (rows > 0)
    {
        board.Add(Console.ReadLine()!);
        rows--;
    }

    return board;
}

public class ChessBoard
{
    private List<string> _chessBoard;
    public ChessBoard(List<string> chessBoard)
    {
        _chessBoard = chessBoard;
    }

    public (int Rows, int Cols) GetSize()
        => (_chessBoard.Count, _chessBoard[0].Length);

    public List<string>? CutBoardOf(int startRow, int startCol, int size)
    {
        if (startCol + size > _chessBoard[0].Length || startRow + size > _chessBoard.Count)
            return null;

        var board = new List<string>();
        for (int row = startRow; row < startRow + size; row++)
            board.Add(_chessBoard[row].Substring(startCol, size));

        return board;
    }

    public List<string> GetBoard()
        => _chessBoard.ToList();
}

public class CreateChessBoardService
{
    private const int SIZE = 8;

    public int GetMinDrawCount(ChessBoard chessBoard)
    {
        var minDrawCount = Int32.MaxValue;

        var size = chessBoard.GetSize();
        for (int row = 0; row <= size.Rows - SIZE; row++)
        {
            for (int col = 0; col <= size.Cols - SIZE; col++)
            {
                var board = chessBoard.CutBoardOf(row, col, SIZE);
                if (board is null)
                    continue;

                minDrawCount = Math.Min(minDrawCount, GetDrawCount(board));
            }
        }

        return minDrawCount;
    }

    private int GetDrawCount(List<string> board)
    {
        var currentColor = 'W';
        var countOfStartWhite = 0;

        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (board[row][col] != currentColor)
                    countOfStartWhite++;

                currentColor = ChangeColor(currentColor);
            }

            currentColor = ChangeColor(currentColor);
        }

        return Math.Min(countOfStartWhite, GetCountOfStartBlack(countOfStartWhite));
    }

    private char ChangeColor(char color)
        => color == 'W' ? 'B' : 'W';

    private int GetCountOfStartBlack(int countOfStartWhite)
        => SIZE * SIZE - countOfStartWhite;
}