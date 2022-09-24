using BattleshipStateTracker.Core;
using Moq;

namespace BattleshipStateTrackerUnitTest
{
    [TestClass]
    public class BoardTests
    {
        private MockRepository mockRepository;

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
        public void PlaceShip_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Board? board = CreateBoard();
            IShip ship = null;

            // Act
            string? result = board.PlaceShip(
                ship);

            // Assert
            Assert.Fail();
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Attack_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Board? board = CreateBoard();
            IPoint attackPoint = null;

            // Act
            string? result = board.Attack(
                attackPoint);

            // Assert
            Assert.Fail();
            mockRepository.VerifyAll();
        }
    }
}
