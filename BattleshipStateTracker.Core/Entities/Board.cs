using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    public class Board : IBoard
    {
        public const uint dimension = 10;
        public Board()
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
        /// Checks if a number is in board dimension range or not.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool IsInDimensionRange(uint number)
        {
            bool isInDimensionRange = Enumerable.Range(1, (int)dimension).Contains((int)number);
            return isInDimensionRange;
        }

        /// <summary>
        /// Checks if ship already placed on board or not.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool IfShipPlacedBefore(IShip ship)
        {
            bool checkShipPlacedBefore = Ships.Any(s => s.Id == ship.Id);
            return checkShipPlacedBefore;
        }

        /// <summary>
        /// Place ship to current board in an intended point.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Returns true after adding ship to board or false if not able to add.</returns>
        public string PlaceShip(IShip ship)
        {
            bool isShipLengthInDimensionRange = IsInDimensionRange(ship.Length);
            if (!isShipLengthInDimensionRange)
            {
                return PlaceShipResult.ShipLengthNotInDimensionRange;
            }

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
        /// Shows attack result to a point in the board.
        /// </summary>
        /// <param name="attackPoint">The point which attack will be on it.</param>
        /// <returns>Returns attack result string.</returns>
        public string Attack(IPoint attackPoint)
        {
            IPoint? point;

            bool isAttackPointColumnInDimensionRange = IsInDimensionRange(attackPoint.Column);
            bool isAttackPointRowInDimensionRange = IsInDimensionRange(attackPoint.Column);
            if (!(isAttackPointColumnInDimensionRange && isAttackPointRowInDimensionRange))
            {
                return BoardAttackedResult.AttackPointNotInDimensionRange;
            }

            if (NotAttackedPoints.Count == 0)
            {
                return BoardAttackedResult.NoAttackPointAvailable;
            }

            point = AttackedPoints.GetMatchPoint(attackPoint);
            if (point is not null)
            {
                return BoardAttackedResult.AttackPointAlreadyBeenAttacked;
            }

            point = NotAttackedPoints.GetMatchPoint(attackPoint);

            if (point is null)
            {
                return BoardAttackedResult.AttackPointNotFound;
            }

            point.AttackStatus = PointAttackStatus.Attacked;

            if (point.OccupancyStatus == PointOccupancyStatus.Occupied)
            {
                if (AreAllShipsSunk)
                {
                    return BoardAttackedResult.AllShipsSunk;
                }
                else
                {
                    return BoardAttackedResult.ItWasAHit;
                }
            }
            else
            {
                return BoardAttackedResult.ItWasAMiss;
            }
        }
    }
}

