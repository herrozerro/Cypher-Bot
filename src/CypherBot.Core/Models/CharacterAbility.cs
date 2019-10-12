using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class CharacterAbility
    {
        public int CharacterAbilityId { get; set; }
        public int CharacterId { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Character Character { get; set; }
    }
}
