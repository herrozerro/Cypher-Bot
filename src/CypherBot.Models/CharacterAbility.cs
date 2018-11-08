using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class CharacterAbility
    {
        public int Id { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<KeyValuePair<string, string>> Bonuses { get; set; }
    }
}
