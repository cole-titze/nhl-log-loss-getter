using System;
using Entities.DbModels;

namespace DataAccess.LogLossRepository
{
	public interface ILogLossGameRepository
	{
        Task AddLogLossGames(IEnumerable<DbLogLossGame> games);
        bool DoesLogLossExistById(int id);
    }
}

