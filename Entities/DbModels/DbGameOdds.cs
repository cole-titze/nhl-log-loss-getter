using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DbModels
{
    public class DbGameOdds
    {
        [Key]
        public int gameId { get; set; }
        public double bovadaOpeningVegasHomeOdds { get; set; }
        public double bovadaOpeningVegasAwayOdds { get; set; }
        public double myBookieOpeningVegasHomeOdds { get; set; }
        public double myBookieOpeningVegasAwayOdds { get; set; }
        public double pinnacleOpeningVegasHomeOdds { get; set; }
        public double pinnacleOpeningVegasAwayOdds { get; set; }
        public double betOnlineOpeningVegasHomeOdds { get; set; }
        public double betOnlineOpeningVegasAwayOdds { get; set; }
        public double bet365OpeningVegasHomeOdds { get; set; }
        public double bet365OpeningVegasAwayOdds { get; set; }
        public double modelHomeOdds { get; set; }
        public double modelAwayOdds { get; set; }
        [ForeignKey("gameId")]
        public DbGame game { get; set; } = null!;
    }
}
