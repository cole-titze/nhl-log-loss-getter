using Entities.DbModels;

namespace DataAccess.LogLossRepository
{
    /// <summary>
    /// Data access for games log loss
    /// </summary>
    public class LogLossGameRepository : ILogLossGameRepository
    {
        private readonly GameDbContext _dbContext;
        private readonly IEnumerable<DbLogLossGame> _cachedLogLossGames;
        public LogLossGameRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
            _cachedLogLossGames = _dbContext.LogLossGame.ToList();
        }
        /// <summary>
        /// Adds the games log loss value if it does not exist. Otherwise updates it.
        /// </summary>
        /// <param name="games">The game's log loss to add or update</param>
        /// <returns></returns>
        public async Task AddUpdateLogLossGames(IEnumerable<DbLogLossGame> games)
        {
            var addList = new List<DbLogLossGame>();
            var updateList = new List<DbLogLossGame>();
            foreach (var game in games)
            {
                var dbGame = _cachedLogLossGames.FirstOrDefault(i => i.gameId == game.gameId);
                if (dbGame == null)
                {
                    addList.Add(game);
                }
                else
                {
                    dbGame.Clone(game);
                    updateList.Add(dbGame);
                }
            }
            await _dbContext.LogLossGame.AddRangeAsync(addList);
            _dbContext.LogLossGame.UpdateRange(updateList);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Checks if a log loss exists by the game id in the cache
        /// </summary>
        /// <param name="id">Id of the game log loss to check for</param>
        /// <returns>True if the log loss exists, otherwise false</returns>
        public bool DoesLogLossExistById(int id)
        {
            var game = _cachedLogLossGames.FirstOrDefault(i => i.gameId == id);
            if (game == null)
                return false;
            return true;
        }
    }
}
