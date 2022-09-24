using BattleshipStateTracker.Core;
using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTracker
{
    public static class InputProcessor
    {
        public static string ProcessAttack(string userInput)
        {
            string[] coordinates = userInput.Split(',');
            if (coordinates.Length != 2)
            {
                return CoordinateCheckResult.TwoInputCoordinatesNeeded;
            }

            bool isXInteger = uint.TryParse(coordinates[0], out uint x);
            bool isYInteger = uint.TryParse(coordinates[1], out uint y);

            if (!(isXInteger && isYInteger))
            {
                return CoordinateCheckResult.InputNotNumber;
            }

            IGame game = new Game();
            IPoint point = new Point(x, y);

            string attackedResult = game.SecondPlayer.Board.Attack(point);

            return attackedResult;

        }
    }
}
