using CypherBot.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CypherBot.Tests
{
    [TestClass]
    public class BotTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
        public static async Task<Character> GetCharacter(string Name)
        {
            var chr = new Character();

            chr.Name = Name;

            chr.MightPool = 10;
            chr.SpeedPool = 10;
            chr.IntPool = 10;

            chr.Tier = 1;
            chr.RecoveryDie = 6;
            chr.RecoveryMod = 0;

            chr.Player = "TestPlayer#1234";
            chr.Cyphers = new List<CharacterCypher>();
            chr.RecoveryRolls = new List<CharacterRecoveryRoll>();

            chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "first" });
            chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "second" });
            chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "third" });
            chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "fourth" });

            var cyls = (IEnumerable<Cypher>)await Utilities.CypherHelper.GetRandomCypherAsync(2);

            chr.Cyphers = cyls.Select(x => new CharacterCypher()
            {
                CypherId = x.CypherId,
                Effect = x.Effect,
                LevelBonus = x.LevelBonus,
                LevelDie = x.LevelDie,
                Name = x.Name,
                Source = x.Source,
                Type = x.Type
            }).ToList();

            chr.Inventory = new List<CharacterInventory>();
            chr.Inventory.Add(new CharacterInventory { ItemName = "Backpack", Qty = 1 });
            chr.Inventory.Add(new CharacterInventory { ItemName = "Torch", Qty = 5 });

            return chr;
        }
    }
}
