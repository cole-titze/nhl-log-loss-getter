using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests
{
    [TestClass]
    public class DbLogLossGameTests
    {
        [TestMethod]
        public void ACallToClone_ShouldReturnClonedObject()
        {
            var logLossGame = new DbLogLossGame()
            {
                gameId = 3,
                bovadaLogLoss = 1,
                draftKingsLogLoss = 1.2,
                barstoolLogLoss = .98,
                betMgmLogLoss = .65,
                modelLogLoss = .54323322,
            };

            var clonedGame = new DbLogLossGame();
            clonedGame.Clone(logLossGame);

            clonedGame.gameId.Should().Be(logLossGame.gameId);
            clonedGame.bovadaLogLoss.Should().Be(logLossGame.bovadaLogLoss);
            clonedGame.barstoolLogLoss.Should().Be(logLossGame.barstoolLogLoss);
            clonedGame.draftKingsLogLoss.Should().Be(logLossGame.draftKingsLogLoss);
            clonedGame.betMgmLogLoss.Should().Be(logLossGame.betMgmLogLoss);
            clonedGame.modelLogLoss.Should().Be(logLossGame.modelLogLoss);
        }
    }
}
