using System;
using System.Collections.Generic;
using System.Text;
using CypherBot.Models.Abstractions;

namespace CypherBot.Models
{
    public class FocusAbility : IAbility
    {
        public int FocusAbilityId { get; set; }
        public int FocusId { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        public Focus Focus { get; set; }
    }
}
