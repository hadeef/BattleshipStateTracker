namespace BattleshipStateTracker.Core
{
    public class Player : IPlayer
    {
        public Player(string name)
        {
            Name = name;
            Board = new Board();
            Board.PlaceShip(new Destroyer());
        }

        public string Name { get; }
        public IBoard Board { get; }
    }
}
