using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
    public class CharacterRecoveryRoll
    {
        public int RecoveryRollId { get; set; }
        public int CharacterId { get; set; }
        public string RollName { get; set; }
        public bool IsUsed { get; set; }

        public Character Character { get; set; }
    }
}
