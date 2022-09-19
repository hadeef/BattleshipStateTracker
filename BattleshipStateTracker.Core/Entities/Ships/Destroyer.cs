using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker.Core
{
    public class Destroyer : Ship
    {
        public Destroyer() : base(ShipTypes.Destroyer, 2)
        {
        }
    }
}

