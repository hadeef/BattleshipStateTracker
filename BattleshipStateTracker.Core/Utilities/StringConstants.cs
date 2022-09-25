namespace BattleshipStateTracker.Core
{
    public class StringConstants
    {
        public static class ShipTypes
        {
            public const string Destroyer = "Destroyer";
            public const string Battleship = "Battleship";
            public const string Unknown = "Unknown";
        }

        public static class PointOccupancyStatus
        {
            public const string Available = "Available";
            public const string Occupied = "Occupied";
        }

        public static class PointAttackStatus
        {
            public const string Attacked = "Attacked";
            public const string NotAttacked = "NotAttacked";
        }

        public static class PointAttackedResultStatus
        {
            public const string Hit = "Hit";
            public const string Miss = "Miss";
            public const string NotAttacked = "NotAttacked";
        }

        public static class BoardAttackedResult
        {
            public const string AttackPointNotInDimensionRange = "At least one of attack point coordinates are not in dimension range";
            public const string AttackPointNotFound = "Attack Result: Attack point not found!";
            public const string AttackPointAlreadyBeenAttacked = "Attack Result: Attack point already been attacked!";

            public const string ItWasAHit = "Attack Result: It was a hit!";
            public const string ItWasAMiss = "Attack Result: It was a miss!";
            public const string AllShipsSunk = "Attack Result: It was a hit!. All opponent ships have sunk. You won the game!";
        }

        public static class PlaceShipResult
        {
            public const string ShipLengthNotInDimensionRange = "Ship Length is not in dimension range";
            public const string NoAvailablePoints = "No available points";
            public const string NotEnoughAvailablePoints = "Not enough available points";
            public const string ShipAlreadyPlaced = "Ship already placed";
            public const string Successful = "Successful";
        }

        public static class CoordinateCheckResult
        {
            public const string InputNotNumber = "At least one of input coordinates are not number!";
            public const string TwoInputCoordinatesNeeded = "Two input coordinates needed!";
        }
    }
}
