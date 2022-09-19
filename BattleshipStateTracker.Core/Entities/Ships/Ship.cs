namespace BattleshipStateTracker.Core
{
    public class Ship : IShip
    {
        public Ship(string name, uint length)
        {
            Id = new Guid();
            Name = name;
            Length = length;
        }

        public Guid Id { get; }
        public string Name { get; }
        public uint Length { get; }
        public uint Hits { get; set; }
        public bool IsSunk => Hits >= Length;
    }
}
