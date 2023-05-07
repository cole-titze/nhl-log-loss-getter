using DataAccess.PredictedGameRepository;
using DataAccess;
using DataAccess.LogLossRepository;
using BusinessLogic.LogLoss;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Entry
{
    public class LogLossGetter
    {
        private readonly ILogger<LogLossGetter> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public LogLossGetter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<LogLossGetter>();
        }
        public async Task Main(string gamesConnectionString)
        {
            // Run Data Collection
            var watch = Stopwatch.StartNew();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            _logger.LogTrace("Starting Log Loss Calculations");
            var gameDbContext = new GameDbContext(gamesConnectionString);
            var predictedGameRepo = new PredictedGameRepository(gameDbContext);
            var logLossRepo = new LogLossGameRepository(gameDbContext);
            var logLossCalculator = new LogLossCalculator(logLossRepo, _loggerFactory);

            var predictedGames = await predictedGameRepo.GetAllPredictedGames();
            var gameLogLosses = logLossCalculator.Calculate(predictedGames);
            await logLossRepo.AddUpdateLogLossGames(gameLogLosses);

            watch.Stop();
            var elapsedTime = watch.Elapsed;
            var minutes = elapsedTime.TotalMinutes.ToString();
            _logger.LogTrace("Completed Log Loss Calculations in " + minutes + " minutes");
        }
    }
}