using System.ComponentModel.DataAnnotations;

namespace Entities.DbModels
{
    public class DbLogLossGame
    {
        [Key]
        public int gameId { get; set; }
        public double draftKingsLogLoss { get; set; }
        public double myBookieLogLoss { get; set; }
        public double betMgmLogLoss { get; set; }
        public double modelLogLoss { get; set; }

        public void Clone(DbLogLossGame game)
        {
            gameId = game.gameId;
            draftKingsLogLoss = game.draftKingsLogLoss;
            myBookieLogLoss = game.myBookieLogLoss;
            betMgmLogLoss = game.betMgmLogLoss;
            modelLogLoss = game.modelLogLoss;
        }
    }
}
