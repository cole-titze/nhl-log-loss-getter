using Entities.DbModels;

namespace DataAccess.LogLossRepository
{
    public class LogLossGameRepository : ILogLossGameRepository
    {
        private readonly GameDbContext _dbContext;
        private readonly IEnumerable<DbLogLossGame> _cachedLogLossGames;
        public LogLossGameRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
            _cachedLogLossGames = _dbContext.LogLossGame.ToList();
        }
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
        public bool DoesLogLossExistById(int id)
        {
            var game = _cachedLogLossGames.FirstOrDefault(i => i.gameId == id);
            if (game == null)
                return false;
            return true;
        }
    }
}
