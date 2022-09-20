namespace BattleshipStateTracker.Core
{
    internal interface IPanel
    {
        uint ColumnDimension { get; }
        uint RowDimension { get; }
        IList<IPoint> Points { get; }
        IList<IShip> Ships { get; }
        IList<IPoint>? AllAvailablePoints { get; }
        IList<IPoint>? AllUnAvailablePoints { get; }
        IList<IPoint>? AllNotAttackedPoints { get; }
        bool AreAllShipsSunk { get; }

        //IList<IPoint>? GeAllAvailablePointsFromPanel();
        //IList<IPoint>? GetMatchAndAvailabePointsFromPanel(IList<IPoint>? points);
        //IList<IPoint>? GetPointsToPlaceShipOnPanelWithStartPoint(IShip ship, IPoint startPoint);
        bool PlaceShipOnPanel(IShip ship);
        bool PlaceShipOnPanel(IShip ship, IPoint startPoint);

        string Attack(IPoint startPoint);
    }
}