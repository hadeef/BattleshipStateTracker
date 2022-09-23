using BattleshipStateTracker.Core;

IGame game = new Game();

IPoint point = new Point(5, 5);
var ss = game.FirstPlayer.Board.Attack(point);

Console.WriteLine("Hello, World!");

