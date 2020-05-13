using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class CharacterPool
    {
        public int PoolId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int PoolIndex { get; set; }
        public int PoolMax { get; set; }
        public int PoolCurrentVaue { get; set; }

        public Character Character { get; set; }
    }
}
