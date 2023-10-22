using System.ComponentModel.DataAnnotations;

namespace Entities.DbModels
{
    public class DbLogLossGame
    {
        [Key]
        public int gameId { get; set; }
        public double draftKingsLogLoss { get; set; }
        public double bovadaLogLoss { get; set; }
        public double betMgmLogLoss { get; set; }
        public double barstoolLogLoss { get; set; }
        public double modelLogLoss { get; set; }

        public void Clone(DbLogLossGame game)
        {
            gameId = game.gameId;
            draftKingsLogLoss = game.draftKingsLogLoss;
            bovadaLogLoss = game.bovadaLogLoss;
            betMgmLogLoss = game.betMgmLogLoss;
            barstoolLogLoss = game.barstoolLogLoss;
            modelLogLoss = game.modelLogLoss;
        }
    }
}
