using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    internal static class PointsUtilies
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
        public static bool CheckPointsContainPointExtension(this IList<IPoint>? points, IPoint point)
        {
            if (points == null)
            {
                return false;
            }

            bool isPointscontainPoint = points.FirstOrDefault(p => p.Row == point.Row && p.Column == point.Column) is not null;
            return isPointscontainPoint;
        }

        /// <summary>
        /// Return match available points from points list.
        /// </summary>
        /// <param name="mainPoints">The list of points to look in.</param>
        /// <param name="point">given point</param>
        /// <returns></returns>
        public static IPoint? GetMatchAndAvailablePoint(this IList<IPoint>? mainPoints, IPoint? point)
        {
            if (mainPoints == null
                || mainPoints.Count == 0
                || point == null)
            {
                return null;
            }

            IPoint? Point = mainPoints.FirstOrDefault(p => p.Row == point.Row
                                                        && p.Column == point.Column
                                                        && p.OccupancyStatus == PointOccupancyStatus.Available);

            return Point;
        }

        /// <summary>
        /// Return match available points from points list.
        /// </summary>
        /// <param name="mainPoints">The list of points to look in.</param>
        /// <param name="subPoint">The list of given points</param>
        /// <returns></returns>
        public static IList<IPoint>? GetMatchAndAvailablePoints(this IList<IPoint>? mainPoints, IList<IPoint>? subPoint)
        {
            if (mainPoints == null
                || mainPoints.Count == 0
                || subPoint == null
                || subPoint.Count == 0)
            {
                return null;
            }

            IList<IPoint> matchPoints = new List<IPoint>();

            foreach (IPoint point in subPoint)
            {
                IPoint? Point = mainPoints.FirstOrDefault(p => p.Row == point.Row
                                                        && p.Column == point.Column
                                                        && p.OccupancyStatus == PointOccupancyStatus.Available);

                if (Point is not null)
                {
                    matchPoints.Add(point);
                }
            }

            if (matchPoints.Count == subPoint.Count)
            {
                return matchPoints;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns adjacent points which are available in intended length.
        /// </summary>
        /// <param name="Points">The list of points to look in</param>
        /// <param name="length">Length of adjacent points</param>
        /// <returns>Returns list of points.</returns>
        public static IList<IPoint>? ReturnAdjacentAvailablePoints(this IList<IPoint>? Points, uint length)
        {
            if (Points == null || Points.Count == 0 || length == 0)
            {
                return null;
            }

            IPoint startPoint;
            Random? random = new();
            IList<int> selectedRandomIndexes = new List<int>();

        SelectIndex:

            int index = random.Next(Points.Count - 1);
            if (!selectedRandomIndexes.Contains(index))
            {
                selectedRandomIndexes.Add(index);
            }
            else
            {
                goto SelectIndex;
            }

            startPoint = Points[index];

            IList<IPoint>? matchPoints;
            IList<IPoint> points = new List<IPoint>();
            IPoint Point;

            //direction is column forward
            for (uint i = 0; i < length; i++)
            {
                Point = new Point(startPoint.Row, startPoint.Column + i);
                points.Add(Point);
            }

            matchPoints = Points.GetMatchAndAvailablePoints(points);
            if ((matchPoints is not null) && matchPoints.Count == length) //Return only if long enough!.
            {
                return matchPoints;
            }

            //direction is column backward
            for (uint i = 0; i < length; i++)
            {
                if (startPoint.Column > i) // checking column number not getting negative.
                {
                    Point = new Point(startPoint.Row, startPoint.Column - i);
                    points.Add(Point);
                }
                else
                {
                    break;
                }
            }

            matchPoints = Points.GetMatchAndAvailablePoints(points);
            if ((matchPoints is not null) && matchPoints.Count == length) //Return only if long enough!.
            {
                return matchPoints;
            }

            //direction is row forward
            for (uint i = 0; i < length; i++)
            {
                Point = new Point(startPoint.Row + 1, startPoint.Column);
                points.Add(Point);
            }

            matchPoints = Points.GetMatchAndAvailablePoints(points);
            if ((matchPoints is not null) && matchPoints.Count == length) //Return only if long enough!.
            {
                return matchPoints;
            }

            //direction is row backward
            for (uint i = 0; i < length; i++)
            {
                if (startPoint.Row > i) // checking row number not getting negative.
                {
                    Point = new Point(startPoint.Row - 1, startPoint.Column);
                    points.Add(Point);
                }
                else
                {
                    break;
                }
            }

            matchPoints = Points.GetMatchAndAvailablePoints(points);
            if ((matchPoints is not null) && matchPoints.Count == length) //Return only if long enough!.
            {
                return matchPoints;
            }

            //If matchPoints is null try new random startPoint. 
            if (selectedRandomIndexes.Count < Points.Count)
            {
                goto SelectIndex;
            }

            return null;
        }
    }
}
