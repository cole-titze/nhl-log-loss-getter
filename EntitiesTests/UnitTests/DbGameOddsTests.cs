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
                bovadaHomeOdds = .77,
                bovadaAwayOdds = .33,
                betMgmAwayOdds = .23,
                betMgmHomeOdds = .88,
                draftKingsAwayOdds = .21,
                draftKingsHomeOdds = .91,
                barstoolAwayOdds = .23,
                barstoolHomeOdds = .98,
                modelHomeOdds = .78,
                modelAwayOdds = .22,
            };

            game.modelHomeOdds.Should().Be(.78);
        }
    }
}
