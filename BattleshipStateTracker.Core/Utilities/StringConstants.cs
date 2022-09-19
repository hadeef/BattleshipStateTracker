namespace BattleshipStateTracker.Core
{
    internal class StringConstants
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
            public const string NoAttackPointAvailable = "Attacked Result: No attack point available!";
            public const string AttackPointNotFoundOrAvailable = "Attacked Result: Attack point not found or not available!";

            public const string ItWasAHit = "Attacked Result: It was a hit!";
            public const string ItWasAMiss = "Attacked Result: It was a miss!";
            public const string ItWasASink = "Attacked Result: It was a sink!";
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
