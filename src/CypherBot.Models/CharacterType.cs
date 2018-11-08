using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class CharacterType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<string, int> StartingPools { get; set; }

        public Dictionary<string, string> Bonuses { get; set; }
    }
}
