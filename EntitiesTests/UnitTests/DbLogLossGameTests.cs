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
                myBookieLogLoss = 2,
                pinnacleLogLoss = 1.2,
                betOnlineLogLoss = .98,
                bet365LogLoss = .65,
                modelLogLoss = .54323322,
            };

            var clonedGame = new DbLogLossGame();
            clonedGame.Clone(logLossGame);

            clonedGame.gameId.Should().Be(logLossGame.gameId);
            clonedGame.bovadaLogLoss.Should().Be(logLossGame.bovadaLogLoss);
            clonedGame.myBookieLogLoss.Should().Be(logLossGame.myBookieLogLoss);
            clonedGame.pinnacleLogLoss.Should().Be(logLossGame.pinnacleLogLoss);
            clonedGame.betOnlineLogLoss.Should().Be(logLossGame.betOnlineLogLoss);
            clonedGame.bet365LogLoss.Should().Be(logLossGame.bet365LogLoss);
            clonedGame.modelLogLoss.Should().Be(logLossGame.modelLogLoss);
        }
    }
}
