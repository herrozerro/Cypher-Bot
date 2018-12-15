using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class CharacterRecoveryRoll
    {
        public int RecoveryRollId { get; set; }
        public string RollName { get; set; }
        public bool IsUsed { get; set; }
    }
}
