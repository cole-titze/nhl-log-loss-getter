using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.LogLossRepository
{
    public class LogLossGameRepository : ILogLossGameRepository
    {
        private readonly GameDbContext _dbContext;
        public LogLossGameRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLogLossGames(IEnumerable<DbGameLogLosses> games)
        {
            await _dbContext.GameLogLoss.AddRangeAsync(games);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> DoesLogLossExistById(int id)
        {
            var game = await _dbContext.GameLogLoss.FirstOrDefaultAsync(i => i.id == id);
            if (game == null)
                return false;
            return true;
        }
    }
}
