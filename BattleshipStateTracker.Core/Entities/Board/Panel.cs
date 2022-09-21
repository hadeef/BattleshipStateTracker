using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    public class Panel : IPanel
    {
        public Panel(uint columnDimension, uint rowDimension)
        {
            ColumnDimension = columnDimension;
            RowDimension = rowDimension;

            Points = new List<IPoint>();
            for (uint i = 1; i <= columnDimension; i++)
            {
                for (uint j = 1; j <= rowDimension; j++)
                {
                    Points.Add(new Point(i, j));
                }
            }

            Ships = new List<IShip>();
        }

        public uint ColumnDimension { get; }

        public uint RowDimension { get; }

        public IList<IPoint> Points { get; }

        public IList<IShip> Ships { get; set; }

        public IList<IPoint>? AllAvailablePoints => Points.Where(p => p.OccupancyStatus == PointOccupancyStatus.Available).ToList();
        public IList<IPoint>? AllUnAvailablePoints => Points.Where(p => p.OccupancyStatus == PointOccupancyStatus.Occupied).ToList();
        public IList<IPoint>? AllNotAttackedPoints => Points.Where(p => p.AttackStatus == PointAttackStatus.NotAttacked).ToList();

        public bool AreAllShipsSunk => (AllUnAvailablePoints is not null) && (AllUnAvailablePoints.Count > 0)
                                       && AllUnAvailablePoints.All(point => point.AttackResultStatus == PointAttackedResultStatus.Hit);

        /// <summary>
        /// Place ship to current panel in an intended point.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Returns true after adding ship to panel or false if not able to add.</returns>
        public bool PlaceShipOnPanel(IShip ship)
        {

            if ((AllAvailablePoints == null) || (AllAvailablePoints.Count == 0))
            {
                return false;
            }

            IList<IPoint>? points = AllAvailablePoints.ReturnAdjacentAvailablePoints(ship.Length);

            if (points == null)
            {
                return false;
            }

            Points.Where(point => points.IsAnyMatchPoint(point)).ToList().ForEach(p => p.ShipId = ship.Id);
            Ships.Add(ship);

            return true;
        }

        /// <summary>
        /// Shows attack result to a point in the panel.
        /// </summary>
        /// <param name="attackPoint">The point which attack will be on it.</param>
        /// <returns>Returns attack result string.</returns>
        public string Attack(IPoint attackPoint)
        {
            if (AllNotAttackedPoints == null || AllNotAttackedPoints.Count == 0)
            {
                return PanelAttackedResult.NoAttackPointAvailable;
            }

            IPoint? point = AllNotAttackedPoints.GetMatchPoint(attackPoint);

            if (point is null)
            {
                return PanelAttackedResult.AttackPointNotFoundOrNotAvailable;
            }

            point.AttackStatus = PointAttackStatus.Attacked;

            if (point.OccupancyStatus == PointOccupancyStatus.Occupied)
            {
                if (AreAllShipsSunk)
                {
                    return PanelAttackedResult.AllShipsSunk;
                }
                else
                {
                    return PanelAttackedResult.ItWasAHit;
                }
            }
            else
            {
                return PanelAttackedResult.ItWasAMiss;
            }
        }
    }
}

