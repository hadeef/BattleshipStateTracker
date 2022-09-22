﻿using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Checks is there any match point in points list.
        /// </summary>
        /// <param name="points">The list of points to  look in.</param>
        /// <param name="point">The looking point</param>
        /// <returns></returns>
        public static bool IsAnyMatchPoint(this IList<IPoint> points, IPoint point)
        {
            if (points.Count == 0 || points == null)
            {
                return false;
            }

            bool isAnyMatchPoint = points.Any(p => p.Row == point.Row && p.Column == point.Column);
            return isAnyMatchPoint;
        }

        /// <summary>
        /// Return match point from points list.
        /// </summary>
        /// <param name="points">The list of points to  look in.</param>
        /// <param name="point">The looking point</param>
        /// <returns></returns>
        public static IPoint? GetMatchPoint(this IList<IPoint> points, IPoint point)
        {
            if (points.Count == 0 || points == null)
            {
                return null;
            }

            IPoint? Point = points.FirstOrDefault(p => p.Row == point.Row && p.Column == point.Column);
            return Point;
        }

        /// <summary>
        /// Return match available points from points list.
        /// </summary>
        /// <param name="mainPoints">The list of points to look in.</param>
        /// <param name="point">given point</param>
        /// <returns></returns>
        public static IPoint? GetMatchAndAvailablePoint(this IList<IPoint> mainPoints, IPoint point)
        {
            if (mainPoints.Count == 0 || point == null)
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
        public static IList<IPoint> GetMatchAndAvailablePoints(this IList<IPoint> mainPoints, IList<IPoint> subPoint)
        {
            IList<IPoint> matchPoints = new List<IPoint>();

            if (mainPoints.Count == 0 || subPoint.Count == 0)
            {
                return matchPoints;
            }

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

            return matchPoints;
        }

        /// <summary>
        /// Returns adjacent points which are available in intended length.
        /// </summary>
        /// <param name="Points">The list of points to look in</param>
        /// <param name="length">Length of adjacent points</param>
        /// <returns>Returns list of points.</returns>
        public static IList<IPoint> ReturnAdjacentAvailablePoints(this IList<IPoint> Points, uint length)
        {
            IPoint startPoint;
            Random? random = new();
            IList<IPoint> matchPoints = new List<IPoint>();
            IList<int> selectedRandomIndexes = new List<int>();

            if (Points.Count == 0 || length == 0 || Points.Count < length)
            {
                return matchPoints;
            }

            for (int i = 0; i < Points.Count; i++)
            {
                selectedRandomIndexes.Add(i);
            }

            do
            {
                int index = random.Next(selectedRandomIndexes.Count - 1);
                int pointSelectIndex = selectedRandomIndexes[index]; //New random index to select random startPoint.
                selectedRandomIndexes.RemoveAt(index);

                startPoint = Points[pointSelectIndex];

                IList<IPoint> points = new List<IPoint>();
                IPoint Point;

                //direction is column forward
                for (uint i = 0; i < length; i++)
                {
                    Point = new Point(startPoint.Row, startPoint.Column + i);
                    points.Add(Point);
                }

                matchPoints = Points.GetMatchAndAvailablePoints(points);
                if (matchPoints.Count == length) //Return only if long enough!.
                {
                    return matchPoints;
                }
                else
                {
                    matchPoints.Clear();
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
                if (matchPoints.Count == length) //Return only if long enough!.
                {
                    return matchPoints;
                }
                else
                {
                    matchPoints.Clear();
                }

                //direction is row forward
                for (uint i = 0; i < length; i++)
                {
                    Point = new Point(startPoint.Row + 1, startPoint.Column);
                    points.Add(Point);
                }

                matchPoints = Points.GetMatchAndAvailablePoints(points);
                if (matchPoints.Count == length) //Return only if long enough!.
                {
                    return matchPoints;
                }
                else
                {
                    matchPoints.Clear();
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
                if (matchPoints.Count == length) //Return only if long enough!.
                {
                    return matchPoints;
                }
                else
                {
                    matchPoints.Clear();
                }

            } while (selectedRandomIndexes.Count > 0); //If matchPoints is null try new random startPoint. 

            return matchPoints;
        }
    }
}
