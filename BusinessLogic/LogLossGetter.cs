using System;
using System.Threading.Tasks;
using DataAccess.PredictedGameRepository;
using DataAccess;
using Microsoft.Extensions.Logging;
using DataAccess.LogLossRepository;

namespace BusinessLogic
{
    public class LogLossGetter
    {
        public async Task Main(string gamesConnectionString)
        {
            // Run Data Collection
            Console.WriteLine("Starting Log Loss Calculation");

            var gameDbContext = new GameDbContext(gamesConnectionString);
            var predictedGameRepo = new PredictedGameRepository(gameDbContext);
            var logLossRepo = new LogLossGameRepository(gameDbContext);
            var logLossCalculator = new LogLossCalculator(logLossRepo);

            var predictedGames = await predictedGameRepo.GetAllPredictedGames();
            var gameLogLosses = logLossCalculator.Calculate(predictedGames);
            await logLossRepo.AddLogLossGames(gameLogLosses);
            
            Console.WriteLine("Completed Log Loss Calculation");
        }
    }
}