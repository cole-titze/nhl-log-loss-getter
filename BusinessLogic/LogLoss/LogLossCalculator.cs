using DataAccess.LogLossRepository;
using Entities.DbModels;

namespace BusinessLogic.LogLoss
{
	public class LogLossCalculator
	{
        private readonly ILogLossGameRepository _logLossGameRepository;
        public LogLossCalculator(ILogLossGameRepository logLossGameRepository)
        {
            _logLossGameRepository = logLossGameRepository;
        }
        private double CalculateLogLoss(double homeOdds, double awayOdds, TEAM winner)
        {
            // Invalid odds return -1
            if (awayOdds == 0 || homeOdds == 0 || awayOdds == -1 || homeOdds == -1)
                return -1;
            return -(((int)winner) * Math.Log(awayOdds) + (1 - ((int)winner)) * Math.Log(homeOdds));
        }
		public IEnumerable<DbLogLossGame> Calculate(IEnumerable<DbGameOdds> games)
		{
            var logLosses = new List<DbLogLossGame>();
            foreach(var game in games)
            {
                if (_logLossGameRepository.DoesLogLossExistById(game.gameId) && game.game.hasBeenPlayed == true)
                    continue;
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
            return logLosses;
        }
	}
}
