using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddLogLossGames(IEnumerable<DbLogLossGame> games)
        {
            await _dbContext.LogLossGame.AddRangeAsync(games);
            await _dbContext.SaveChangesAsync();
        }
        public bool DoesLogLossExistById(int id)
        {
            var game = _cachedLogLossGames.FirstOrDefault(i => i.id == id);
            if (game == null)
                return false;
            return true;
        }
    }
}
