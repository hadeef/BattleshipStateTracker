namespace BattleshipStateTracker.Core
{
    public interface IGame
    {
        IPlayer FirstPlayer { get; }
        IPlayer SecondPlayer { get; }
    }
}