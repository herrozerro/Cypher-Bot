using CypherBot.Core.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class Cypher : ICypher
    {
        private int _level = 0;
        public int CypherId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level
        {
            get
            {
                if (_level == 0)
                {
                    _level = (LevelDie == 0 ? LevelBonus : new Random(Guid.NewGuid().GetHashCode()).Next() % LevelDie) + 1 + LevelBonus;
                }
                return _level;
            }
        }
        public int LevelDie { get; set; }
        public int LevelBonus { get; set; }

        public IEnumerable<CypherFormOption> Forms { get; set; }

        public string Effect { get; set; }
        public IEnumerable<CypherEffectOption> EffectOptions { get; set; }

        public string Source { get; set; }
    }
}
