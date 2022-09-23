namespace BattleshipStateTracker.Core
{
    public class Game : IGame
    {
        public Game()
        {
            FirstPlayer = new Player("FirstPlayer");
            SecondPlayer = new Player("SecondPlayer");
        }

        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }
    }
}
