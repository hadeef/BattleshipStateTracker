namespace BattleshipStateTracker.Core
{
    public class Game : IGame
    {
        public Game()
        {
            FirstPlayer = new Player();
            SecondPlayer = new Player();
        }

        public IPlayer FirstPlayer { get; }
        public IPlayer SecondPlayer { get; }
    }
}
