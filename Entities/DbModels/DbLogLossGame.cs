using System.ComponentModel.DataAnnotations;

namespace Entities.DbModels
{
    public class DbLogLossGame
    {
        [Key]
        public int gameId { get; set; }
        public double bovadaLogLoss { get; set; }
        public double myBookieLogLoss { get; set; }
        public double pinnacleLogLoss { get; set; }
        public double betOnlineLogLoss { get; set; }
        public double bet365LogLoss { get; set; }
        public double modelLogLoss { get; set; }

        public void Clone(DbLogLossGame game)
        {
            gameId = game.gameId;
            bovadaLogLoss = game.bovadaLogLoss;
            myBookieLogLoss = game.myBookieLogLoss;
            pinnacleLogLoss = game.pinnacleLogLoss;
            betOnlineLogLoss = game.betOnlineLogLoss;
            bet365LogLoss = game.bet365LogLoss;
            modelLogLoss = game.modelLogLoss;
        }
    }
}
