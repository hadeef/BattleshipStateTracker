namespace BattleshipStateTracker.Core
{
    public class Game : IGame
    {
        public Game()
        {
            FirstPlayer = new Player();
            SecondPlayer = new Player();
        }

        public Player FirstPlayer { get; }
        public Player SecondPlayer { get; }
    }
}
