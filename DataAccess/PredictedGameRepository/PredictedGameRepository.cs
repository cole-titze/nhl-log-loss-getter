using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.PredictedGameRepository
{
    public class PredictedGameRepository : IPredictedGameRepository
    {
        private readonly GameDbContext _dbContext;
        public PredictedGameRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DbGameOdds>> GetAllPredictedGames()
        {
            return await _dbContext.GameOdds.Include(x => x.game).ToListAsync();
        }
    }
}
