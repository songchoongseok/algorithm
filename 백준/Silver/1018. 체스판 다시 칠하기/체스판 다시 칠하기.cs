var boardSize = Console.ReadLine()!
        .Split()
        .Select(x => Convert.ToInt32(x))
        .ToList();

var board = InputBoard(boardSize[0]);

var chessBoard = new ChessBoard(board);
var service = new CreateChessBoardService();

Console.WriteLine(service.GetMinDrawCount(chessBoard));

static List<string> InputBoard(int boardSizeY)
{
    var board = new List<string>();
    while (boardSizeY > 0)
    {
        board.Add(Console.ReadLine()!);
        boardSizeY--;
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

    public (int Y, int X) GetSize()
        => (_chessBoard.Count, _chessBoard[0].Length);

    public List<string>? CutBoardOf(int startY, int startX, int size)
    {
        if (startX + size > _chessBoard[0].Length || startY + size > _chessBoard.Count)
            return null;

        var board = new List<string>();
        for (int y = startY; y < startY + size; y++)
            board.Add(_chessBoard[y].Substring(startX, size));

        return board;
    }
}

public class CreateChessBoardService
{
    private const int SIZE = 8;

    public int GetMinDrawCount(ChessBoard chessBoard)
    {
        var minDrawCount = Int32.MaxValue;

        var size = chessBoard.GetSize();
        for (int y = 0; y <= size.Y - SIZE; y++)
        {
            for (int x = 0; x <= size.X - SIZE; x++)
            {
                var board = chessBoard.CutBoardOf(y, x, SIZE);
                if (board is null)
                    continue;

                var drawCount = GetDrawCount(board);
                minDrawCount = minDrawCount > drawCount
                    ? drawCount
                    : minDrawCount;
            }
        }

        return minDrawCount;
    }

    private int GetDrawCount(List<string> board)
    {
        var startWhiteColor = 'W';
        var startBlackColor = 'B';

        var countOfStartWhite = 0;
        var countOfStartBlack = 0;
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                if (board[y][x] != startWhiteColor)
                    countOfStartWhite++;

                if (board[y][x] != startBlackColor)
                    countOfStartBlack++;

                startWhiteColor = ChangeColor(startWhiteColor);
                startBlackColor = ChangeColor(startBlackColor);
            }

            startWhiteColor = ChangeColor(startWhiteColor);
            startBlackColor = ChangeColor(startBlackColor);
        }

        return countOfStartWhite > countOfStartBlack
                ? countOfStartBlack
                : countOfStartWhite;
    }

    private char ChangeColor(char color)
        => color == 'W' ? 'B' : 'W';
}