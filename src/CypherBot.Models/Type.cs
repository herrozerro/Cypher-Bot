using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int IntellectStartingPool { get; set; }
        public int MightStartingPool { get; set; }
        public int SpeedStartingPool { get; set; }

        public List<TypeAbility> TypeAbilities { get; set; }
    }
}
