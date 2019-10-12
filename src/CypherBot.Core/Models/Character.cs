using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Player { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
        public int XP { get; set; }

        public string Descriptor { get; set; }
        public string Type { get; set; }
        public string Focus { get; set; }

        public int MightPool { get; set; }
        public int SpeedPool { get; set; }
        public int IntPool { get; set; }

        public int RecoveryDie { get; set; }
        public int RecoveryMod { get; set; }
        public List<CharacterRecoveryRoll> RecoveryRolls { get; set; }

        public List<CharacterCypher> Cyphers { get; set; }
        public List<CharacterInventory> Inventory { get; set; }
        public List<CharacterAbility> Abilities { get; set; }
        public List<CharacterArtifact> Artifacts { get; set; }

        public Character()
        {
            //set some defaults
            RecoveryRolls = new List<CharacterRecoveryRoll>();
            Cyphers = new List<CharacterCypher>();
            Inventory = new List<CharacterInventory>();
            Abilities = new List<CharacterAbility>();
            Artifacts = new List<CharacterArtifact>();
        }
    }
}
