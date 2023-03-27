using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests
{
    [TestClass]
    public class DbGameTests
    {
        [TestMethod]
        public void FutureTestsHere()
        {
            var game = new DbGame()
            {
                id = 1,
                hasBeenPlayed = true,
                winner = TEAM.home,
            };

            game.hasBeenPlayed.Should().BeTrue();
        }
    }
}
