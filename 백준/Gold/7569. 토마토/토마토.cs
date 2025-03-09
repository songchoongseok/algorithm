
const int IMPOSSIBLE = -1;

var boxSize = InputBoxSize();
var tomatoBox = InputTomatoes(boxSize);

var day = tomatoBox.PassedAllDays();
Console.WriteLine(tomatoBox.CheckAllRipened()
    ? day
    : IMPOSSIBLE);

static BoxSize InputBoxSize()
{
    var inputs = Console.ReadLine()!.Split();

    return new(inputs[2].ConvertToInt(),
        inputs[0].ConvertToInt(),
        inputs[1].ConvertToInt());
}

static TomatoBox InputTomatoes(BoxSize boxSize)
{
    var tomatoBox = new TomatoBox(boxSize);

    for (int curHeight = 0; curHeight < boxSize.Height; curHeight++)
        for (int curLength = 0; curLength < boxSize.Length; curLength++)
        {
            var tomatoes = Console.ReadLine()!
                .Split()
                .Select(x => x.ConvertToInt())
                .ToList();

            tomatoBox.SetTomatoLine(curHeight, curLength, tomatoes);
        }

    return tomatoBox;
}

public record BoxSize(int Height, int Width, int Length);

static class Extension
{
    public static int ConvertToInt(this string str)
        => Convert.ToInt32(str);
}

public class TomatoBox
{
    public record Position(int Height, int Width, int Length);

    private readonly List<Position> dPositions = new()
    {
        new(0,0,1),
        new(0,0,-1),
        new(0,1,0),
        new(0,-1,0),
        new(1,0,0),
        new(-1,0,0)
    };

    private BoxSize _boxSize = null!;
    private Tomato[,,] _tomatoes = null!;

    public enum Status { Ripe = 1, Normal = 0, None = -1 };

    public TomatoBox(BoxSize boxSize)
    {
        _boxSize = boxSize;
        _tomatoes = new Tomato[boxSize.Height,
            boxSize.Width,
            boxSize.Length];
    }

    public void SetTomatoLine(int height, int length, List<int> tomatoes)
    {
        for (int curWidth = 0; curWidth < _boxSize.Width; curWidth++)
            _tomatoes[height, curWidth, length] = new Tomato(tomatoes[curWidth], new(height, curWidth, length));
    }

    public int PassedAllDays()
    {
        var maxDay = -1;
        var ripenedTomatoes = GetRipenedTomatoes();

        while (ripenedTomatoes.Count > 0)
        {
            var tomato = ripenedTomatoes.Dequeue();
            maxDay = Math.Max(tomato.RipenedDay, maxDay);

            var nowPos = tomato.Pos;
            foreach (var dPos in dPositions)
            {
                var neighborPos = new Position(nowPos.Height + dPos.Height,
                    nowPos.Width + dPos.Width,
                    nowPos.Length + dPos.Length);


                if (IsInBox(neighborPos))
                {
                    var neighborTomato = _tomatoes[neighborPos.Height, neighborPos.Width, neighborPos.Length];

                    if (neighborTomato.IsNormalTomato())
                    {
                        neighborTomato.Ripe(tomato.RipenedDay + 1);
                        ripenedTomatoes.Enqueue(_tomatoes[neighborPos.Height, neighborPos.Width, neighborPos.Length]);
                    }

                }
            }
        }

        return maxDay;
    }


    public bool CheckAllRipened()
    {
        for (int curHeight = 0; curHeight < _boxSize.Height; curHeight++)
            for (int curWidth = 0; curWidth < _boxSize.Width; curWidth++)
                for (int curLength = 0; curLength < _boxSize.Length; curLength++)
                    if (_tomatoes[curHeight, curWidth, curLength].Status == Status.Normal)
                        return false;

        return true;
    }

    private bool IsInBox(Position pos)
        => 0 <= pos.Height && pos.Height < _boxSize.Height
            && 0 <= pos.Width && pos.Width < _boxSize.Width
            && 0 <= pos.Length && pos.Length < _boxSize.Length;

    private Queue<Tomato> GetRipenedTomatoes()
    {
        var tomatoes = new Queue<Tomato>();
        for (int curHeight = 0; curHeight < _boxSize.Height; curHeight++)
            for (int curWidth = 0; curWidth < _boxSize.Width; curWidth++)
                for (int curLength = 0; curLength < _boxSize.Length; curLength++)
                {
                    if (_tomatoes[curHeight, curWidth, curLength].Status == Status.Ripe)
                        tomatoes.Enqueue(_tomatoes[curHeight, curWidth, curLength]);
                }

        return tomatoes;
    }

    public class Tomato
    {
        public Position Pos;
        public Status Status { get; set; } = Status.Normal;
        public int RipenedDay { get; private set; }

        public Tomato(int status, Position position)
        {
            Status = (Status)status;
            Pos = position;

            RipenedDay = Status == Status.Ripe
                            ? 0
                            : -1;
        }

        public void Ripe(int day)
        {
            Status = Status.Ripe;
            RipenedDay = day;
        }

        public bool IsNormalTomato()
            => Status == Status.Normal
                && RipenedDay == -1;
    }
}