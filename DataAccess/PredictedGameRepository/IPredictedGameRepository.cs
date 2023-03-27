using Entities.DbModels;

namespace DataAccess.PredictedGameRepository
{
    public interface IPredictedGameRepository
	{
		Task<IEnumerable<DbGameOdds>> GetAllPredictedGames();
	}
}
