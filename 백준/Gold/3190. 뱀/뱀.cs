SnakeGame snakeGame = Input();
Console.WriteLine(snakeGame.StartGame());


static SnakeGame Input()
{
    int mapSize = Console.ReadLine()!.ConvertToInt();

    List<Position> apples = new();
    int appleCount = Console.ReadLine()!.ConvertToInt();
    for (int i = 0; i < appleCount; i++)
    {
        var apple = Console.ReadLine()!.Split();
        apples.Add(new(apple[1].ConvertToInt(), apple[0].ConvertToInt()));
    }

    Dictionary<int, string> snakeDirByTime = new();
    int changeDirCount = Console.ReadLine()!.ConvertToInt();
    for (int i = 0; i < changeDirCount; i++)
    {
        var data = Console.ReadLine()!.Split();
        snakeDirByTime.Add(data[0].ConvertToInt(), data[1]);
    }

    return new(mapSize: mapSize,
        apples: apples,
        snakeDirByTime: snakeDirByTime);
}

static class Extension
{
    public static int ConvertToInt(this string str)
        => Convert.ToInt32(str);
}

public record Position(int X, int Y);

public class SnakeGame
{
    private readonly int TAIL = 0;
    private enum HeadDir
    {
        Right,
        Bottom,
        Left,
        Top
    }

    private int _mapSize;
    private List<Position> _apples;
    private IReadOnlyDictionary<int, string> _snakeDirByTime;

    private HeadDir _headDir;
    private List<Position> _snake;

    public SnakeGame(int mapSize, IReadOnlyList<Position> apples, IReadOnlyDictionary<int, string> snakeDirByTime)
    {
        _mapSize = mapSize;
        _apples = apples.ToList();
        _snakeDirByTime = snakeDirByTime;

        _snake = new List<Position>() { GetStartPosition() };
        _headDir = HeadDir.Right;
    }

    public int StartGame()
    {
        int gameTime = 0;
        while (Move())
        {
            gameTime++;

            if (_snakeDirByTime.ContainsKey(gameTime))
                _headDir = ChangeHeadDir(_headDir, _snakeDirByTime[gameTime]);
        }

        return gameTime + 1;
    }

    private bool Move()
    {
        Position head = _snake.LastOrDefault()!;
        Position nextHead = NextHead(head, _headDir);

        if (IsMapOut(nextHead) || IsTouchBody(nextHead))
            return false;

        if (_apples.Contains(nextHead) is false)
            _snake.RemoveAt(TAIL);
        else
            _apples.Remove(nextHead);
            

        _snake.Add(nextHead);
        return true;
    }

    private HeadDir ChangeHeadDir(HeadDir headDir, string dir)
        => dir switch
        {
            "L" => (HeadDir)((((Enum.GetNames(typeof(HeadDir)).Length + (int)headDir - 1)) % Enum.GetNames(typeof(HeadDir)).Length)),
            "D" => (HeadDir)((((Enum.GetNames(typeof(HeadDir)).Length + (int)headDir + 1)) % Enum.GetNames(typeof(HeadDir)).Length)),
            _ => throw new ArgumentOutOfRangeException(nameof(HeadDir))
        };

    private Position NextHead(Position head, HeadDir headDir)
        => headDir switch
        {
            HeadDir.Right => new(head.X + 1, head.Y),
            HeadDir.Left => new(head.X - 1, head.Y),
            HeadDir.Top => new(head.X, head.Y - 1),
            HeadDir.Bottom => new(head.X, head.Y + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(HeadDir))
        };

    private bool IsMapOut(Position nextPos)
        => (0 < nextPos.X && nextPos.X <= _mapSize
            && 0 < nextPos.Y && nextPos.Y <= _mapSize) is false;

    private bool IsTouchBody(Position nextPos)
        => _snake.Contains(nextPos);

    private Position GetStartPosition()
        => new(1, 1);
}