using System;
using System.Collections.Generic;
using System.Text;
using CypherBot.Core.Models.Abstractions;

namespace CypherBot.Core.Models
{
    public class UnidentifiedArtifact : IArtifact
    {
        public int UnidentifiedArtifactId { get; set; }
        public string UnidentifiedArtifactKey { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Genre { get; set; }
        public string Quirk { get; set; }

        public bool IsIdentified { get; set; }

        public int Level { get; set; }
        public int LevelDie { get; set; }
        public int LevelBonus { get; set; }

        public string Effect { get; set; }
        public string Source { get; set; }
        public string Depletion { get; set; }
    }
}
