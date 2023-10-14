using DataAccess.LogLossRepository;
using Entities.DbModels;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.LogLoss
{
    /// <summary>
    /// Business Logic for calculating log losses for games
    /// </summary>
	public class LogLossCalculator
	{
        private readonly ILogger<LogLossCalculator> _logger;
        private readonly ILogLossGameRepository _logLossGameRepository;
        public LogLossCalculator(ILogLossGameRepository logLossGameRepository, ILoggerFactory loggerFactory)
        {
            _logLossGameRepository = logLossGameRepository;
            _logger = loggerFactory.CreateLogger<LogLossCalculator>();
        }
        /// <summary>
        /// Given the home and away odds, calculate the log loss of the game
        /// </summary>
        /// <param name="homeOdds">Decimal percent of home team winning</param>
        /// <param name="awayOdds">Decimal percent of away team winning</param>
        /// <param name="winner">The winner of the game</param>
        /// <returns>The log loss of the game</returns>
        private double CalculateLogLoss(double homeOdds, double awayOdds, TEAM winner)
        {
            // Invalid odds return -1
            if (awayOdds == 0 || homeOdds == 0 || awayOdds == -1 || homeOdds == -1)
                return -1;
            return -(((int)winner) * Math.Log(awayOdds) + (1 - ((int)winner)) * Math.Log(homeOdds));
        }
        /// <summary>
        /// Given a list of games and their odds, calculates the log loss for each game and model
        /// </summary>
        /// <param name="games">The game and odds for each team and each model</param>
        /// <returns>A list of calculated log losses</returns>
		public IEnumerable<DbLogLossGame> Calculate(IEnumerable<DbGameOdds> games)
		{
            var logLosses = new List<DbLogLossGame>();
            foreach(var game in games)
            {
                var logLoss = new DbLogLossGame()
                {
                    gameId = game.gameId,
                    modelLogLoss = CalculateLogLoss(game.modelHomeOdds, game.modelAwayOdds, game.game.winner),
                    bovadaLogLoss = CalculateLogLoss(game.bovadaOpeningVegasHomeOdds, game.bovadaOpeningVegasAwayOdds, game.game.winner),
                    myBookieLogLoss = CalculateLogLoss(game.myBookieOpeningVegasHomeOdds, game.myBookieOpeningVegasAwayOdds, game.game.winner),
                    pinnacleLogLoss = CalculateLogLoss(game.pinnacleOpeningVegasHomeOdds, game.pinnacleOpeningVegasAwayOdds, game.game.winner),
                    betOnlineLogLoss = CalculateLogLoss(game.betOnlineOpeningVegasHomeOdds, game.betOnlineOpeningVegasAwayOdds, game.game.winner),
                    bet365LogLoss = CalculateLogLoss(game.bet365OpeningVegasHomeOdds, game.bet365OpeningVegasAwayOdds, game.game.winner)
                };
                logLosses.Add(logLoss);
            }
            _logger.LogInformation("Number of Game's Log Losses calculated: " + logLosses.Count.ToString());

            return logLosses;
        }
	}
}
