using BattleshipStateTracker.Core;

IPoint point = new Point(1, 1);

Board board = new();

for (int i = 1; i < 21; i++)
{
    Destroyer? ship = new();
    string? ss = board.PlaceShip(ship);
}

IList<IPoint> av = board.AvailablePoints;
IList<IPoint> av2 = board.UnAvailablePoints;
bool av3 = board.AreAllShipsSunk;

List<IPoint>? av4 = board.Points.Where(p => p.ShipId != null).ToList();

string attackResult = board.Attack(point);

Console.WriteLine("Hello, World!");

