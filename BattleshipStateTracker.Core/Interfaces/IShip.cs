namespace BattleshipStateTracker.Core
{
    public interface IShip
    {
        Guid Id { get; }
        uint Length { get; }
        string? Name { get; }
    }
}