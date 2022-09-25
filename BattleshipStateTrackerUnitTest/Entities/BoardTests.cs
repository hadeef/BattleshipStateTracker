using BattleshipStateTracker.Core;
using Moq;
using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTrackerUnitTest
{
    [TestClass]
    public class BoardTests
    {
        private MockRepository? mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private Board CreateBoard()
        {
            return new Board();
        }

        [TestMethod]
        public void PlaceShip_FillBoardWithShips_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            string shipPlacementresult = string.Empty;

            // Act
            for (int i = 0; i < 10; i++)
            {
                IShip ship = new Ship(ShipTypes.Battleship, 10);
                shipPlacementresult = board.PlaceShip(ship);
            }

            // Assert
            Assert.AreEqual(shipPlacementresult, PlaceShipResult.Successful);
            Assert.AreEqual(board.Ships.Count, 10);
            Assert.AreEqual(board.AvailablePoints.Count, 0);
            Assert.AreEqual(board.UnAvailablePoints.Count, 100);
        }

        [TestMethod]
        public void PlaceShip_AddShipWithLengthLongerThanDimension_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = new Ship(ShipTypes.Battleship, 11);

            // Act
            string shipPlacementresult = board.PlaceShip(ship);

            // Assert
            Assert.AreEqual(shipPlacementresult, PlaceShipResult.ShipLengthNotInDimensionRange);
        }

        [TestMethod]
        public void PlaceShip_AddShipToFilledBoard_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            for (int i = 0; i < 10; i++)
            {
                IShip ship = new Ship(ShipTypes.Battleship, 10);
                board.PlaceShip(ship);
            }

            // Act
            IShip battleship = new Ship(ShipTypes.Battleship, 5);
            string shipPlacementresult = board.PlaceShip(battleship);

            // Assert
            Assert.AreEqual(shipPlacementresult, PlaceShipResult.NoAvailablePoints);
        }

        [TestMethod]
        public void PlaceShip_AddShipToFilledBoard_NotEnoughAvailablePoints_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            for (int i = 0; i < 20; i++)
            {
                IShip ship = new Destroyer();
                board.PlaceShip(ship);
            }

            // Act
            IShip battleship = new Ship(ShipTypes.Battleship, 5);
            string shipPlacementresult = board.PlaceShip(battleship);

            // Assert
            Assert.AreEqual(shipPlacementresult, PlaceShipResult.NotEnoughAvailablePoints);
        }

        [TestMethod]
        public void PlaceShip_AddShipTwiceToBoard_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = new Destroyer();
            board.PlaceShip(ship);

            // Act
            string shipPlacementresult = board.PlaceShip(ship);

            // Assert
            Assert.AreEqual(shipPlacementresult, PlaceShipResult.ShipAlreadyPlaced);
        }

        [TestMethod]
        public void Attack_AttackPointNotInDimensionRange_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IPoint point = new Point(2, 11);

            // Act
            string attackResult = board.Attack(point);

            // Assert
            Assert.AreEqual(attackResult, BoardAttackedResult.AttackPointNotInDimensionRange);
        }

        [TestMethod]
        public void Attack_AttackToSamePointTwice_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = new Destroyer();
            board.PlaceShip(ship);
            IPoint point = board.UnAvailablePoints.First();
            string attackResult = board.Attack(point);

            // Act
            attackResult = board.Attack(point);

            // Assert
            Assert.AreEqual(attackResult, BoardAttackedResult.AttackPointAlreadyBeenAttacked);
        }

        [TestMethod]
        public void Attack_AttackToAvailablePoint_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = new Destroyer();
            board.PlaceShip(ship);
            IPoint point = board.AvailablePoints.First();

            // Act
            string attackResult = board.Attack(point);

            // Assert
            Assert.AreEqual(attackResult, BoardAttackedResult.ItWasAMiss);
        }

        [TestMethod]
        public void Attack_AttackToUnAvailablePoint_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = new Destroyer();
            board.PlaceShip(ship);
            IPoint point = board.UnAvailablePoints.First();

            // Act
            string attackResult = board.Attack(point);

            // Assert
            Assert.AreEqual(attackResult, BoardAttackedResult.ItWasAHit);
        }

        [TestMethod]
        public void Attack_AttackToAllUnAvailablePoint_Pass()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = new Destroyer();
            board.PlaceShip(ship);
            List<IPoint> points = board.UnAvailablePoints.ToList();

            // Act
            string attackResult = string.Empty;
            foreach (IPoint point in points)
            {
                attackResult = board.Attack(point);
            }

            // Assert
            Assert.AreEqual(board.AreAllShipsSunk, true);
            Assert.AreEqual(attackResult, BoardAttackedResult.AllShipsSunk);
        }
    }
}
