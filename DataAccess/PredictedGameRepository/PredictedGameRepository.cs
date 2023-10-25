using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.PredictedGameRepository
{
    /// <summary>
    /// Data access for predicted games
    /// </summary>
    public class PredictedGameRepository : IPredictedGameRepository
    {
        private readonly GameDbContext _dbContext;
        public PredictedGameRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets all predicted games
        /// </summary>
        /// <returns>All predicted games in the database</returns>
        public async Task<IEnumerable<DbGameOdds>> GetAllPredictedGames()
        {
            return await _dbContext.GameOdds.Include(x => x.game).Where(x => x.game.hasBeenPlayed == true).ToListAsync();
        }
    }
}
