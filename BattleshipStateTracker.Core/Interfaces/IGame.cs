namespace BattleshipStateTracker.Core
{
    public interface IGame
    {
        Player FirstPlayer { get; }
        Player SecondPlayer { get; }
    }
}