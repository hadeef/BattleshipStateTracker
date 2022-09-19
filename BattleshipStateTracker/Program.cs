// See https://aka.ms/new-console-template for more information

using BattleshipStateTracker.Core;
using static BattleshipStateTracker.Core.StringConstants;

IPoint point = new Point(2, 2);

IList<IPoint>? points = PointsUtilies.ReturnAjacentPoints(point, 3, MovingDirectionOnPoints.RowBackward);

Console.WriteLine("Hello, World!");

