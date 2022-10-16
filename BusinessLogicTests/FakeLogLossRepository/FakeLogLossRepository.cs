using System;
using DataAccess.LogLossRepository;
using Entities.DbModels;

namespace BusinessLogicTests.Fakes
{
    public class FakeLogLossRepository : ILogLossGameRepository
    {
        private IList<DbLogLossGame> _logLosses = new List<DbLogLossGame>();
        public FakeLogLossRepository(List<DbLogLossGame> logLosses)
        {
            _logLosses = logLosses;
        }

        public Task AddUpdateLogLossGames(IEnumerable<DbLogLossGame> games)
        {
            throw new NotImplementedException();
        }

        public bool DoesLogLossExistById(int id)
        {
            var game = _logLosses.Where(i => i.id == id).FirstOrDefault();
            if (game == null)
                return false;
            return true;
        }
    }
}

