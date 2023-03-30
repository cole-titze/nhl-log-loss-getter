using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests
{
    [TestClass]
    public class DbGameOddsTests
    {
        [TestMethod]
        public void FutureTestsHere()
        {
            var game = new DbGameOdds()
            {
                gameId = 1,
                bovadaOpeningVegasHomeOdds = .77,
                bovadaOpeningVegasAwayOdds = .33,
                myBookieOpeningVegasAwayOdds = .23,
                myBookieOpeningVegasHomeOdds = .88,
                pinnacleOpeningVegasAwayOdds = .21,
                pinnacleOpeningVegasHomeOdds = .91,
                betOnlineOpeningVegasAwayOdds = .88,
                betOnlineOpeningVegasHomeOdds = .32,
                bet365OpeningVegasAwayOdds = .23,
                bet365OpeningVegasHomeOdds = .98,
                modelHomeOdds = .78,
                modelAwayOdds = .22,
            };

            game.modelHomeOdds.Should().Be(.78);
        }
    }
}
