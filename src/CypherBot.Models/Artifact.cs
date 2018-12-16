using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class Artifact
    {
        private int level { get; set; }
        public int ArtifactId { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }

        public int Level
        {
            get
            {
                if (level == 0)
                {
                    level = (LevelDie == 0 ? LevelBonus : new Random(Guid.NewGuid().GetHashCode()).Next() % LevelDie) + 1 + LevelBonus;
                }
                return level;
            }
        }
        public int LevelDie { get; set; }
        public int LevelBonus { get; set; }

        public string Effect { get; set; }
        public string Source { get; set; }
        public string Depletion { get; set; }
    }
}
