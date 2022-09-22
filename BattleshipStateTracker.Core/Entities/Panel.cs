﻿using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    public class Panel : IPanel
    {
        private const int dimension = 10;
        public Panel()
        {
            Points = new List<IPoint>();
            for (uint i = 1; i <= dimension; i++)
            {
                for (uint j = 1; j <= dimension; j++)
                {
                    Points.Add(new Point(i, j));
                }
            }

            Ships = new List<IShip>();
        }

        public IList<IPoint> Points { get; }

        public IList<IShip> Ships { get; set; }

        public IList<IPoint> AvailablePoints => Points.Where(p => p.OccupancyStatus == PointOccupancyStatus.Available).ToList();
        public IList<IPoint> UnAvailablePoints => Points.Where(p => p.OccupancyStatus == PointOccupancyStatus.Occupied).ToList();
        public IList<IPoint> AttackedPoints => Points.Where(p => p.AttackStatus == PointAttackStatus.Attacked).ToList();
        public IList<IPoint> NotAttackedPoints => Points.Where(p => p.AttackStatus == PointAttackStatus.NotAttacked).ToList();
        public bool AreAllShipsSunk => (UnAvailablePoints is not null) && (UnAvailablePoints.Count > 0)
                                       && UnAvailablePoints.All(point => point.AttackResultStatus == PointAttackedResultStatus.Hit);

        /// <summary>
        /// Checks if ship already placed on panel or not.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool IfShipPlacedBefore(IShip ship)
        {
            bool checkShipPlacedBefore = Ships.Any(s => s.Id == ship.Id);
            return checkShipPlacedBefore;
        }

        /// <summary>
        /// Place ship to current panel in an intended point.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Returns true after adding ship to panel or false if not able to add.</returns>
        public string PlaceShip(IShip ship)
        {
            if (AvailablePoints.Count == 0)
            {
                return PlaceShipResult.NoAvailablePoints;
            }
            else if (IfShipPlacedBefore(ship))
            {
                return PlaceShipResult.ShipAlreadyPlaced;
            }

            IList<IPoint> points = AvailablePoints.ReturnAdjacentAvailablePoints(ship.Length);

            if (points.Count == 0)
            {
                return PlaceShipResult.NotEnoughAvailablePoints;
            }

            Points.Where(point => points.IsAnyMatchPoint(point)).ToList().ForEach(p => p.ShipId = ship.Id);
            Ships.Add(ship);

            return PlaceShipResult.Successful;
        }

        /// <summary>
        /// Shows attack result to a point in the panel.
        /// </summary>
        /// <param name="attackPoint">The point which attack will be on it.</param>
        /// <returns>Returns attack result string.</returns>
        public string Attack(IPoint attackPoint)
        {
            IPoint? point;

            if (NotAttackedPoints.Count == 0)
            {
                return PanelAttackedResult.NoAttackPointAvailable;
            }

            point = AttackedPoints.GetMatchPoint(attackPoint);
            if (point is not null)
            {
                return PanelAttackedResult.AttackPointAlreadyBeenAttacked;
            }

            point = NotAttackedPoints.GetMatchPoint(attackPoint);

            if (point is null)
            {
                return PanelAttackedResult.AttackPointNotFound;
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

