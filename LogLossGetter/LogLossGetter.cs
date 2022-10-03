using System;
using System.Threading.Tasks;
using DataAccess.PredictedGameRepository;
using DataAccess;
using Microsoft.Extensions.Logging;
using DataAccess.LogLossRepository;

namespace LogLoss
{
    public class LogLossGetter
    {
        public async Task Main(ILogger logger, string gamesConnectionString)
        {
            // Run Data Collection
            logger.LogInformation("Starting Log Loss Calculation");

            var gameDbContext = new GameDbContext(gamesConnectionString);
            var predictedGameRepo = new PredictedGameRepository(gameDbContext);
            var logLossRepo = new LogLossGameRepository(gameDbContext);
            var logLossCalculator = new LogLossCalculator(logLossRepo);

            var predictedGames = await predictedGameRepo.GetAllPredictedGames();
            var gameLogLosses = await logLossCalculator.Calculate(predictedGames);
            await logLossRepo.AddLogLossGames(gameLogLosses);
            
            logger.LogInformation("Completed Log Loss Calculation");
        }
    }
}