using CypherBot.Core.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class TypeAbility : IAbility
    {
        public int TypeAbilityId { get; set; }
        public int TypeId { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        public Type Type { get; set; }
    }
}
