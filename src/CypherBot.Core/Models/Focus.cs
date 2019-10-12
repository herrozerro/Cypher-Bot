using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class Focus
    {
        public int FocusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<FocusAbility> FocusAbilities { get; set; }
    }
}
