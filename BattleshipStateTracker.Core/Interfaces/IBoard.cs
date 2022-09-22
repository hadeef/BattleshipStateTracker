namespace BattleshipStateTracker.Core
{
    internal interface IBoard
    {
        IList<IPoint> Points { get; }
        IList<IShip> Ships { get; }
        IList<IPoint> AvailablePoints { get; }
        IList<IPoint> UnAvailablePoints { get; }
        IList<IPoint> AttackedPoints { get; }
        IList<IPoint> NotAttackedPoints { get; }
        bool AreAllShipsSunk { get; }

        string PlaceShip(IShip ship);

        string Attack(IPoint startPoint);
    }
}