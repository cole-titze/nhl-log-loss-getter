using System;
namespace Entities.DbModels
{
    public class DbGame
    {
        public int id { get; set; }
        public TEAM winner { get; set; }
    }
    public enum TEAM
    {
        home = 0,
        away = 1
    }
}

