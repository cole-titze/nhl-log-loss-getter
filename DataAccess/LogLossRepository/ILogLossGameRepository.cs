using System;
using Entities.DbModels;

namespace DataAccess.LogLossRepository
{
	public interface ILogLossGameRepository
	{
        Task AddUpdateLogLossGames(IEnumerable<DbLogLossGame> games);
        bool DoesLogLossExistById(int id);
    }
}

