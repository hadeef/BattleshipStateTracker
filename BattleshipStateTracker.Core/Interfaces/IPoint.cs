namespace BattleshipStateTracker.Core
{
    public interface IPoint
    {
        uint Row { get; }
        uint Column { get; }
        string OccupancyStatus { get; }
        string AttackStatus { get; set; }
        string AttackResultStatus { get; }
        Guid? ShipId { get; set; }

    }
}