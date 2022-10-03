using System;
using Entities.DbModels;

namespace DataAccess.LogLossRepository
{
	public interface ILogLossGameRepository
	{
        Task AddLogLossGames(IEnumerable<DbGameLogLosses> games);
        Task<bool> DoesLogLossExistById(int id);

    }
}

