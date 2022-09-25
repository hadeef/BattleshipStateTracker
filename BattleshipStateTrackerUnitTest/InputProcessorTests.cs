using BattleshipStateTracker;
using Moq;
using static BattleshipStateTracker.Core.StringConstants;

namespace BattleshipStateTrackerUnitTest
{
    [TestClass]
    public class InputProcessorTests
    {
        private MockRepository? mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TestMethod]
        public void ProcessAttack_OneCoordinateInput_Pass()
        {
            // Arrange
            string userInput = "5";

            // Act
            string result = InputProcessor.ProcessAttack(userInput);

            // Assert
            Assert.AreEqual(result, CoordinateCheckResult.TwoInputCoordinatesNeeded);
        }

        [TestMethod]
        public void ProcessAttack_NotNumberInput_Pass()
        {
            // Arrange
            string userInput = "5,notNumber";

            // Act
            string result = InputProcessor.ProcessAttack(userInput);

            // Assert
            Assert.AreEqual(result, CoordinateCheckResult.InputNotNumber);
        }
    }
}
