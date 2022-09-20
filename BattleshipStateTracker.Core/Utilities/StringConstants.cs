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

        public static class PanelAttackedResult
        {
            public const string NoAttackPointAvailable = "Attack Result: No attack point available!";
            public const string AttackPointNotFoundOrNotAvailable = "Attack Result: Attack point not found or not available!";

            public const string ItWasAHit = "Attack Result: It was a hit!";
            public const string ItWasAMiss = "Attack Result: It was a miss!";
            public const string AllShipsSunk = "Attack Result: All the ships have sunk. Player has lost the game!";
        }

        public static class MovingDirectionOnPoints
        {
            public const string ColumnForward = "Column Forward";
            public const string ColumnBackward = "Column Backward";
            public const string RowForward = "Row Forward";
            public const string RowBackward = "Row Backward";
        }
    }
}
