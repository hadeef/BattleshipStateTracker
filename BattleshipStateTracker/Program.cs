using BattleshipStateTracker;
using BattleshipStateTracker.Core;

IGame game = new Game();

Console.WriteLine(game.FirstPlayer.Name + ":Enter attack point X and Y coordinates seperated with `,` (like X,Y):");

string userInput = Console.ReadLine() ?? string.Empty;

string result = InputProcessor.ProcessAttack(userInput);

Console.WriteLine(result);

