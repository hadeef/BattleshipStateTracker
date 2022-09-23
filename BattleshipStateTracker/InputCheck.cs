using BattleshipStateTracker.Core;
using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker
{
    public static class InputCheck
    {
        public static string check(int x)
        {
            if (Enumerable.Range(1, Board.dimension).Contains(x))
            {
                return CoordinateCheckResult.InputInRange;
            }

            return CoordinateCheckResult.InputNotInRange;
        }
    }
}
