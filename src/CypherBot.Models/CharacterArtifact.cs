using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class CharacterArtifact
    {
        public int ArtifactId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }

        public int Level { get; set; }
        public int LevelDie { get; set; }
        public int LevelBonus { get; set; }

        public string Effect { get; set; }
        public string Source { get; set; }
        public string Depletion { get; set; }

        public Character Character { get; set; }
    }
}
