namespace BattleshipStateTracker.Core
{
    public class Ship : IShip
    {
        public Ship(string name, uint length)
        {
            Id = Guid.NewGuid();
            Name = name;
            Length = length;
        }

        public Guid Id { get; }
        public string Name { get; }
        public uint Length { get; }
    }
}
