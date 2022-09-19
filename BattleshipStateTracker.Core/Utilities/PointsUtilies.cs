using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    internal class PointsUtilies
    {
        /// <summary>
        /// Returns points adjacent to given point in given length and direction.
        /// </summary>
        /// <param name="point">The point to start</param>
        /// <param name="length">Number of adjacent points</param>
        /// <param name="direction">Direction to move from starting point</param>
        /// <returns>Returns list of points.</returns>
        public static IList<IPoint>? ReturnAjacentPoints(IPoint point, uint length, string direction)
        {
            if (point == null || length == 0 || string.IsNullOrWhiteSpace(direction))
            {
                return null;
            }

            IList<IPoint>? points = new List<IPoint>();
            IPoint Point;

            for (uint i = 0; i < length; i++)
            {
                if (direction == MovingDirectionOnPoints.ColumnForward)
                {
                    Point = new Point(point.Row, point.Column + i);
                    points.Add(Point);
                }

                else if (direction == MovingDirectionOnPoints.ColumnBackward && (point.Column > i))
                {
                    Point = new Point(point.Row, point.Column - i);
                    points.Add(Point);
                }

                else if (direction == MovingDirectionOnPoints.RowForward)
                {
                    Point = new Point(point.Row + i, point.Column);
                    points.Add(Point);
                }

                else if (direction == MovingDirectionOnPoints.RowBackward && (point.Row > i))
                {
                    Point = new Point(point.Row - i, point.Column);
                    points.Add(Point);
                }
            }

            //If not enough return null!
            if (points.Count != length)
            {
                points = null;
            }

            return points;
        }

        /// <summary>
        /// Checks if points list is including a specific point.
        /// </summary>
        /// <param name="points">The list of points to check inside</param>
        /// <param name="point">The point for checking</param>
        /// <returns></returns>
        public static IPoint? CheckPointsContainPoint(IList<IPoint> points, IPoint point)
        {
            return points.FirstOrDefault(p => p.Row == point.Row && p.Column == point.Column);
        }

    }
}
