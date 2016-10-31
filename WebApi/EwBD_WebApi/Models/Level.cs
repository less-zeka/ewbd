using System.Collections.Generic;

namespace EwBD_WebApi.Models
{
    public class Level
    {
        public int LevelNr { get; set; }

        public List<Vector3> DiamondPositions { get; set; }

        public List<Vector3> RockPositions { get; set; }
    }
}