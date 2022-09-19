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
        }

        public uint ColumnDimension { get; }

        public uint RowDimension { get; }

        public IList<IPoint> Points { get; set; }

        public IList<IPoint>? AllAvailablePoints => Points.Where(p => p.OccupancyStatus == PointOccupancyStatus.Available).ToList();
        public IList<IPoint>? AllNotAttackedPoints => Points.Where(p => p.AttackStatus == PointAttackStatus.NotAttacked).ToList();

        private bool ChangePointAttackStatusToAttacked(IPoint point)
        {
            Points.Where(p => p.Row == point.Row && p.Column == point.Column).
        }

        /// <summary>
        /// Return match points in panel with given points which are available.
        /// </summary>
        /// <param name="points">list of points to check</param>
        /// <returns></returns>
        private IList<IPoint>? GetMatchAndAvailabePointsFromPanel(IList<IPoint>? points)
        {
            if (points == null || points.Count == 0)
            {
                return null;
            }

            IList<IPoint> matchPoints = new List<IPoint>();

            foreach (IPoint point in points)
            {
                IPoint? Point = Points.FirstOrDefault(p => p.Row == point.Row
                                                        && p.Column == point.Column
                                                        && p.OccupancyStatus == PointOccupancyStatus.Available);

                if (Point is not null)
                {
                    matchPoints.Add(point);
                }
            }

            if (matchPoints.Count == points.Count)
            {
                return matchPoints;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets some of available points to place ship on panel. The method is private.
        /// </summary>
        /// <param name="ship">The ship is intended to place in panel</param>
        /// <param name="startPoint">The point considered and ship's first point to place</param>
        /// <returns>Returns points which ship could be placed on panel with given start point.</returns>
        private IList<IPoint>? GetPointsToPlaceShipOnPanelWithStartPoint(IShip ship, IPoint startPoint)
        {
            if (ship == null || startPoint == null)
            {
                return null;
            }

            IList<IPoint>? points, matchpoints;

            points = PointsUtilies.ReturnAjacentPoints(startPoint, ship.Length, MovingDirectionOnPoints.ColumnForward);
            matchpoints = GetMatchAndAvailabePointsFromPanel(points);
            if (matchpoints is not null)
            {
                return matchpoints;
            }

            points = PointsUtilies.ReturnAjacentPoints(startPoint, ship.Length, MovingDirectionOnPoints.ColumnBackward);
            matchpoints = GetMatchAndAvailabePointsFromPanel(points);
            if (matchpoints is not null)
            {
                return matchpoints;
            }

            points = PointsUtilies.ReturnAjacentPoints(startPoint, ship.Length, MovingDirectionOnPoints.RowForward);
            matchpoints = GetMatchAndAvailabePointsFromPanel(points);
            if (matchpoints is not null)
            {
                return matchpoints;
            }

            points = PointsUtilies.ReturnAjacentPoints(startPoint, ship.Length, MovingDirectionOnPoints.RowBackward);
            matchpoints = GetMatchAndAvailabePointsFromPanel(points);
            if (matchpoints is not null)
            {
                return matchpoints;
            }

            return null;
        }

        /// <summary>
        /// Place ship to current panel.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="startPoint">Starting point on the panel to place ship</param>
        /// <returns>Returns true after adding ship to panel or false if not able to add.</returns>
        public bool PlaceShipOnPanel(IShip ship, IPoint startPoint)
        {
            if (AllAvailablePoints == null
                || AllAvailablePoints.Count == 0
                || !PointsUtilies.CheckPointsContainPoint(AllAvailablePoints, startPoint))
            {
                return false;
            }

            IList<IPoint>? points = GetPointsToPlaceShipOnPanelWithStartPoint(ship, startPoint);

            if (points == null)
            {
                return false;
            }

            Points.Where(point => points.Contains(point)).ToList().ForEach(p => p.ShipId = ship.Id);

            return true;
        }

        /// <summary>
        /// Place ship to current panel starting in random point.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Returns true after adding ship to panel or false if not able to add.</returns>
        public bool PlaceShipOnPanel(IShip ship)
        {

            if (AllAvailablePoints == null || AllAvailablePoints.Count == 0)
            {
                return false;
            }

            do
            {
                Random? random = new();
                int index = random.Next(AllAvailablePoints.Count - 1);

                IPoint? point = AllAvailablePoints[index];
                bool isShipPlaced = PlaceShipOnPanel(ship, point);

                if (isShipPlaced)
                {
                    return true;
                }

            }
            while (AllAvailablePoints.Count > 0);

            return false;
        }

        /// <summary>
        /// Place ship to current panel.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="startPoint">Starting point on the panel to place ship</param>
        /// <returns>Returns true after adding ship to panel or false if not able to add.</returns>
        public string Attack(IShip ship, IPoint startPoint)
        {
            if (AllNotAttackedPoints == null || AllNotAttackedPoints.Count == 0)
            {
                return PanelAttackedResult.NoAttackPointAvailable;
            }
            else if (!PointsUtilies.CheckPointsContainPoint(AllNotAttackedPoints, startPoint))
            {
                return PanelAttackedResult.AttackPointNotFoundOrAvailable;
            }

        }
    }
}

