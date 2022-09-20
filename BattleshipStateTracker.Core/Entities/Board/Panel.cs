﻿using static BattleshipStateTracker.Core.StringConstants;

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
        /// Return match available points in panel with given points.
        /// </summary>
        /// <param name="points">list of points to find their match in panel.</param>
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
        /// Gets points to place a ship on panel starting in an intended point.
        /// </summary>
        /// <param name="ship">The ship which is intended to place in panel</param>
        /// <param name="startPoint">The intended point as start point for ship to place</param>
        /// <returns>Returns points which ship could be placed on them in panel.</returns>
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
        /// Place ship to current panel in an intended point.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Returns true after adding ship to panel or false if not able to add.</returns>
        public bool PlaceShipOnPanel(IShip ship
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

            //Points.Where(point => points.CheckPointsContainPointExtension(point)).ToList().ForEach(p => p.ShipId = ship.Id);
            points.ToList().ForEach(p => p.ShipId = ship.Id);
            Ships.Add(ship);

            return true;
        }

        /// <summary>
        /// Place ship to current panel starting in a random point.
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

            IPoint? point = PointsUtilies.CheckPointsContainPoint(AllNotAttackedPoints, attackPoint);

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

