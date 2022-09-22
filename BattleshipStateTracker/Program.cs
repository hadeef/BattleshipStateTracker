using BattleshipStateTracker.Core;

IPoint point = new Point(1, 1);

Panel panel = new();

for (int i = 1; i < 21; i++)
{
    Destroyer? ship = new();
    string? ss = panel.PlaceShip(ship);
}

IList<IPoint> av = panel.AvailablePoints;
IList<IPoint> av2 = panel.UnAvailablePoints;
bool av3 = panel.AreAllShipsSunk;

List<IPoint>? av4 = panel.Points.Where(p => p.ShipId != null).ToList();

string attackResult = panel.Attack(point);

Console.WriteLine("Hello, World!");

