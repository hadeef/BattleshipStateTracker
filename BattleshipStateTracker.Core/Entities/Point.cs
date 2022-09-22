using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    public class Point : IPoint
    {
        public Point(uint row, uint column)
        {
            Row = row;
            Column = column;
            AttackStatus = PointAttackStatus.NotAttacked;
        }

        public uint Column { get; }
        public uint Row { get; }
        public string OccupancyStatus
        {
            get
            {
                if (ShipId is null)
                {
                    return PointOccupancyStatus.Available;
                }
                else
                {
                    return PointOccupancyStatus.Occupied;
                }
            }
        }
        public string AttackStatus { get; set; }
        public string AttackResultStatus
        {
            get
            {
                if (AttackStatus == PointAttackStatus.NotAttacked)
                {
                    return PointAttackedResultStatus.NotAttacked;
                }
                else if (ShipId is null)
                {
                    return PointAttackedResultStatus.Miss;
                }
                else
                {
                    return PointAttackedResultStatus.Hit;
                }
            }
        }
        public Guid? ShipId { get; set; }

    }
}
