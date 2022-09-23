namespace BattleshipStateTracker.Core
{
    public interface IPlayer
    {
        string Name { get; }
        IBoard Board { get; }
    }
}