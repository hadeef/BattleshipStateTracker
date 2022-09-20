﻿// See https://aka.ms/new-console-template for more information

using BattleshipStateTracker.Core;

IPoint point = new Point(1, 1);

//IList<IPoint>? points = PointsUtilies.ReturnAjacentPoints(point, 3, MovingDirectionOnPoints.RowBackward);

Panel? panel = new(10, 10);
Destroyer? ship = new Destroyer();

panel.PlaceShipOnPanel(ship);

IList<IPoint>? av = panel.AllAvailablePoints;
IList<IPoint>? av2 = panel.AllUnAvailablePoints;
bool av3 = panel.AreAllShipsSunk;

List<IPoint>? av4 = panel.Points.Where(p => p.ShipId != null).ToList();

Console.WriteLine("Hello, World!");

