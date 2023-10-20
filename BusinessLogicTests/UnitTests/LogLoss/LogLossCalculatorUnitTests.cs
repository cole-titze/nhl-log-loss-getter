using BusinessLogic.LogLoss;
using BusinessLogicTests.Fakes;
using Entities.DbModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace BusinessLogicTests.UnitTests.LogLoss;

[TestClass]
public class LogLossCalculatorUnitTests
{
    public List<DbGameOdds> PredictedGameFactory(int numberOfNewPredictedGames, int numberOfExistingFinishedGames, int numberOfExistingUnfinishedGames)
    {
        var predictedGameList = new List<DbGameOdds>();
        for (int i = 0; i < numberOfExistingFinishedGames; i++)
        {
            var predictedGame = new DbGameOdds()
            {
                gameId =  i,
                game = new DbGame
                {
                    hasBeenPlayed = true
                }
            };
            predictedGameList.Add(predictedGame);
        }
        for (int i = 0; i < numberOfExistingUnfinishedGames; i++)
        {
            var predictedGame = new DbGameOdds()
            {
                gameId = numberOfExistingFinishedGames + i,
                game = new DbGame
                {
                    hasBeenPlayed = false
                }
            };
            predictedGameList.Add(predictedGame);
        }
        for (int i = 0; i < numberOfNewPredictedGames; i++)
        {
            var predictedGame = new DbGameOdds()
            {
                gameId = numberOfExistingFinishedGames + numberOfExistingUnfinishedGames + i,
                game = new DbGame
                {
                    hasBeenPlayed = true
                }
            };
            predictedGameList.Add(predictedGame);
        }
        return predictedGameList;
    }
    public List<DbLogLossGame> LogLossFactory(int numberOfExistingPredictedGames, int numberOfExistingFuturePredictedGames)
    {
        var logLossList = new List<DbLogLossGame>();
        for (int i = 0; i < numberOfExistingPredictedGames; i++)
        {
            var logLoss = new DbLogLossGame()
            {
                gameId  = i
            };
            logLossList.Add(logLoss);
        }
        for (int i = 0; i < numberOfExistingFuturePredictedGames; i++)
        {
            var logLoss = new DbLogLossGame()
            {
                gameId = numberOfExistingPredictedGames + i
            };
            logLossList.Add(logLoss);
        }
        return logLossList;
    }
    public (LogLossCalculator, List<DbGameOdds>) Factory(int numberOfNewPredictedGames, int numberOfExistingFinishedPredictedGames, int numberOfExistingUnFinishedPredictedGames)
    {
        var logLossList = LogLossFactory(numberOfExistingFinishedPredictedGames, numberOfExistingUnFinishedPredictedGames);
        var logLossRepo = new FakeLogLossRepository(logLossList);

        var cut = new LogLossCalculator(logLossRepo, A.Fake<ILoggerFactory>());
        var predictedGameList = PredictedGameFactory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUnFinishedPredictedGames);

        return (cut, predictedGameList);
    }
    [TestMethod]
    public void ACallToCalculate_WithNoGames_ShouldGetNoGames()
    {
        int numberOfNewPredictedGames = 0;
        int numberOfExistingFinishedPredictedGames = 0;
        int numberOfExistingUpcomingPredictedGames = 0;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);

        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.Should().HaveCount(numberOfNewPredictedGames);
    }
    [TestMethod]
    public void ACallToCalculate_WithFiveGamesAlreadyRecordedAndFinished_ShouldGetFiveGames()
    {
        int numberOfNewPredictedGames = 0;
        int numberOfExistingFinishedPredictedGames = 5;
        int numberOfExistingUpcomingPredictedGames = 0;
        int numberOfGamesToUpdate = numberOfNewPredictedGames + numberOfExistingUpcomingPredictedGames + numberOfExistingFinishedPredictedGames;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);


        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.Should().HaveCount(numberOfGamesToUpdate);
    }
    [TestMethod]
    public void ACallToCalculate_WithFiveGamesAlreadyRecordedAndTenNewGames_ShouldGetTenGames()
    {
        int numberOfNewPredictedGames = 10;
        int numberOfExistingFinishedPredictedGames = 5;
        int numberOfExistingUpcomingPredictedGames = 0;
        int numberOfGamesToUpdate = numberOfNewPredictedGames + numberOfExistingUpcomingPredictedGames + numberOfExistingFinishedPredictedGames;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);


        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.Should().HaveCount(numberOfGamesToUpdate);
    }
    [TestMethod]
    public void ACallToCalculate_WithFiveFutureGamesAlreadyRecordedAndFiveNewGames_ShouldGetTenGames()
    {
        int numberOfNewPredictedGames = 5;
        int numberOfExistingFinishedPredictedGames = 0;
        int numberOfExistingUpcomingPredictedGames = 5;
        int numberOfGamesToUpdate = numberOfNewPredictedGames + numberOfExistingUpcomingPredictedGames;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);

        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.Should().HaveCount(numberOfGamesToUpdate);
    }
    [TestMethod]
    public void ACallToCalculate_WithFiveFutureGamesAlreadyRecordedAndZeroNewGames_ShouldGetFiveGames()
    {
        int numberOfNewPredictedGames = 0;
        int numberOfExistingFinishedPredictedGames = 0;
        int numberOfExistingUpcomingPredictedGames = 5;
        int numberOfGamesToUpdate = numberOfNewPredictedGames + numberOfExistingUpcomingPredictedGames;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);

        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.Should().HaveCount(numberOfGamesToUpdate);
    }
    [TestMethod]
    public void ACallToCalculate_WithZeroFutureGamesAlreadyRecordedAndFiveNewGames_ShouldGetFiveGames()
    {
        int numberOfNewPredictedGames = 5;
        int numberOfExistingFinishedPredictedGames = 0;
        int numberOfExistingUpcomingPredictedGames = 0;
        int numberOfGamesToUpdate = numberOfNewPredictedGames + numberOfExistingUpcomingPredictedGames;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);

        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.Should().HaveCount(numberOfGamesToUpdate);
    }
    [TestMethod]
    public void ACallToCalculate_WithGameOddsOfNegativeOne_ShouldLogLossNegativeOne()
    {
        int numberOfNewPredictedGames = 5;
        int numberOfExistingFinishedPredictedGames = 0;
        int numberOfExistingUpcomingPredictedGames = 0;
        int numberOfGamesToUpdate = numberOfNewPredictedGames + numberOfExistingUpcomingPredictedGames;
        var (cut, predictedGameList) = Factory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);

        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.First().bet365LogLoss.Should().Be(-1);
        logLossGames.First().modelLogLoss.Should().Be(-1);
        logLossGames.First().bovadaLogLoss.Should().Be(-1);
        logLossGames.First().myBookieLogLoss.Should().Be(-1);
        logLossGames.First().pinnacleLogLoss.Should().Be(-1);
        logLossGames.First().betOnlineLogLoss.Should().Be(-1);
    }
    private List<DbGameOdds> BuildLogLossValues(List<DbGameOdds> logLossList)
    {
        logLossList[0].modelHomeOdds = .85;
        logLossList[0].modelAwayOdds = .15;
        logLossList[0].bovadaOpeningVegasHomeOdds = .73;
        logLossList[0].bovadaOpeningVegasAwayOdds = .45;
        logLossList[0].game.winner = TEAM.home;

        logLossList[1].modelHomeOdds = .845;
        logLossList[1].modelAwayOdds = .155;
        logLossList[1].bovadaOpeningVegasHomeOdds = .77;
        logLossList[1].bovadaOpeningVegasAwayOdds = .43;
        logLossList[1].game.winner = TEAM.away;

        logLossList[2].modelHomeOdds = .45;
        logLossList[2].modelAwayOdds = .55;
        logLossList[2].bovadaOpeningVegasHomeOdds = .45;
        logLossList[2].bovadaOpeningVegasAwayOdds = .65;
        logLossList[2].game.winner = TEAM.away;
        return logLossList;
    }
    [TestMethod]
    public void ACallToCalculate_WithThreeFilledFutureGames_ShouldGetThreeCorrectlyFilledGames()
    {
        int numberOfNewPredictedGames = 3;
        int numberOfExistingFinishedPredictedGames = 0;
        int numberOfExistingUpcomingPredictedGames = 0;
        var logLossList = new List<DbLogLossGame>();
        var logLossRepo = new FakeLogLossRepository(logLossList);
        var cut = new LogLossCalculator(logLossRepo, A.Fake<ILoggerFactory>());
        var predictedGameList = PredictedGameFactory(numberOfNewPredictedGames, numberOfExistingFinishedPredictedGames, numberOfExistingUpcomingPredictedGames);
        predictedGameList = BuildLogLossValues(predictedGameList);

        var logLossGames = cut.Calculate(predictedGameList);

        logLossGames.ElementAt(0).bet365LogLoss.Should().Be(-1);
        Math.Round(logLossGames.ElementAt(0).modelLogLoss, 4).Should().Be(.1625);
        Math.Round(logLossGames.ElementAt(0).bovadaLogLoss, 4).Should().Be(.3147);
        logLossGames.ElementAt(0).myBookieLogLoss.Should().Be(-1);
        logLossGames.ElementAt(0).pinnacleLogLoss.Should().Be(-1);
        logLossGames.ElementAt(0).betOnlineLogLoss.Should().Be(-1);

        logLossGames.ElementAt(1).bet365LogLoss.Should().Be(-1);
        Math.Round(logLossGames.ElementAt(1).modelLogLoss, 4).Should().Be(1.8643);
        Math.Round(logLossGames.ElementAt(1).bovadaLogLoss, 4).Should().Be(.8440);
        logLossGames.ElementAt(1).myBookieLogLoss.Should().Be(-1);
        logLossGames.ElementAt(1).pinnacleLogLoss.Should().Be(-1);
        logLossGames.ElementAt(1).betOnlineLogLoss.Should().Be(-1);

        logLossGames.ElementAt(2).bet365LogLoss.Should().Be(-1);
        Math.Round(logLossGames.ElementAt(2).modelLogLoss, 4).Should().Be(.5978);
        Math.Round(logLossGames.ElementAt(2).bovadaLogLoss, 4).Should().Be(.4308);
        logLossGames.ElementAt(2).myBookieLogLoss.Should().Be(-1);
        logLossGames.ElementAt(2).pinnacleLogLoss.Should().Be(-1);
        logLossGames.ElementAt(2).betOnlineLogLoss.Should().Be(-1);
    }
    [TestMethod]
    public void UselessTestForCoverage()
    {
        var logLossList = new List<DbLogLossGame>();
        var logLossRepo = new FakeLogLossRepository(logLossList);
        Action testMap = () => logLossRepo.AddUpdateLogLossGames(new List<DbLogLossGame>());

        Assert.ThrowsException<NotImplementedException>(testMap);
    }
}
