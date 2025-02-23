using System.Collections;

var info = Input();

var commands = new List<string>();
while (info.CommandCount-- > 0)
    commands.Add(Console.ReadLine()!);

var robot = new Robot(info.MapSize);
var isSuccess = true;
foreach (var command in commands)
{
    var cmd = command.ConvertToCommand();
    if (!robot.RunCommand(cmd.CommandType, cmd.Command))
    {
        isSuccess = false;
        break;
    }
}

Console.WriteLine(isSuccess
    ? robot.GetPositionToString()
    : "-1");

static (int MapSize, int CommandCount) Input()
{
    var inputs = Console.ReadLine()!.Split();

    return new(inputs[0].ConvertToInt(), inputs[1].ConvertToInt());
}

public enum CommandType { MOVE, TURN };

static class Extension
{
    public static int ConvertToInt(this string str)
        => Convert.ToInt32(str);

    public static (CommandType CommandType, int Command) ConvertToCommand(this string str)
        => (str.Split()[0] == "MOVE"
            ? CommandType.MOVE
            : CommandType.TURN,
            str.Split()[1].ConvertToInt());
}

public class Robot
{
    private readonly int RIGHT = 1;
    private readonly int LEFT = 0;

    private record Position(int X, int Y);
    private enum Direction { East, West, South, North };


    private int _mapSize;
    private Position _position = new(0, 0);
    private Direction _direction = Direction.East;

    public Robot(int mapSize)
    {
        _mapSize = mapSize;
    }

    public string GetPositionToString()
        => $"{_position.X} {_position.Y}";

    public bool RunCommand(CommandType commandType, int command)
        => commandType switch
        {
            CommandType.MOVE => Move(command),
            CommandType.TURN => Turn(command),
            _ => false
        };

    public bool Turn(int command)
    {
        switch (_direction)
        {
            case Direction.East:
                _direction = command == RIGHT
                    ? Direction.South
                    : Direction.North;
                break;
            case Direction.West:
                _direction = command == RIGHT
                    ? Direction.North
                    : Direction.South;
                break;
            case Direction.South:
                _direction = command == RIGHT
                    ? Direction.West
                    : Direction.East;
                break;
            case Direction.North:
                _direction = command == RIGHT
                    ? Direction.East
                    : Direction.West;
                break;
            default:
                return false;
        }

        return true;
    }

    public bool Move(int command)
    {
        switch (_direction)
        {
            case Direction.East:
                if (_position.X + command >= _mapSize)
                    return false;

                _position = _position with { X = _position.X + command };
                break;
            case Direction.West:
                if (_position.X < command)
                    return false;

                _position = _position with { X = _position.X - command };
                break;
            case Direction.South:
                if (_position.Y < command)
                    return false;

                _position = _position with { Y = _position.Y - command };
                break;
            case Direction.North:
                if (_position.Y + command >= _mapSize)
                    return false;

                _position = _position with { Y = _position.Y + command };
                break;
            default:
                return false;
        }

        return true;
    }
}