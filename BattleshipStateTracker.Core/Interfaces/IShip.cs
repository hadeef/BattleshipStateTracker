namespace BattleshipStateTracker.Core
{
    public interface IShip
    {
        Guid Id { get; }
        uint Hits { get; set; }
        bool IsSunk { get; }
        uint Length { get; }
        string? Name { get; }
    }
}