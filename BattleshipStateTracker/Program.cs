using BattleshipStateTracker;
using BattleshipStateTracker.Core;
using static BattleshipStateTracker.Core.StringConstants;

IGame game = new Game();

Console.WriteLine(game.FirstPlayer.Name + ":Enter attack point X coordinate:");

int x;
string inputCheckResult;
bool isInputInteger = int.TryParse(Console.ReadLine(), out x);

if (!isInputInteger)
{
    Console.WriteLine(CoordinateCheckResult.InputNotNumber + ":Enter attack point X coordinate:");
}
else
{
    inputCheckResult = InputCheck.check(x);
    if (inputCheckResult == CoordinateCheckResult.InputNotInRange)
    {
        Console.WriteLine(CoordinateCheckResult.InputNotInRange + ":Enter attack point X coordinate:");
    }
    else
    {
        Console.WriteLine(game.FirstPlayer.Name + ":Enter attack point Y coordinate:");
    }
}

//IPoint point = new Point(5, 5);
//string? ss = game.FirstPlayer.Board.Attack(point);

//Console.WriteLine("Hello, World!");

//Console.WriteLine("Enter your age:");
//int age = Console.ReadLine();

