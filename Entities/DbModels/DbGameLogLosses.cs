using System;
namespace Entities.DbModels
{
    public class DbLogLossGame
    {
        public int id { get; set; }
        public double bovadaLogLoss { get; set; }
        public double myBookieLogLoss { get; set; }
        public double pinnacleLogLoss { get; set; }
        public double betOnlineLogLoss { get; set; }
        public double bet365LogLoss { get; set; }
        public double modelLogLoss { get; set; }
    }
}
