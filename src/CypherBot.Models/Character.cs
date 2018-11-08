using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class Character
    {
        public string Player { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
        public int XP { get; set; }

        public int MightPool { get; set; }
        public int SpeedPool { get; set; }
        public int IntPool { get; set; }

        public int RecoveryDie { get; set; }
        public int RecoveryMod { get; set; }
        public List<CharacterRecoveryRoll> RecoveryRolls { get; set; }

        public List<Cypher> Cyphers { get; set; }
        public List<CharacterInventory> Inventory { get; set; }

        public Character()
        {
            //set some defaults
            RecoveryRolls = new List<CharacterRecoveryRoll>();
            Cyphers = new List<Cypher>();
            Inventory = new List<CharacterInventory>();
        }
    }
}
