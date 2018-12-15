using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CypherBot.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }



        public static async Task<Models.Character> GetCharacter(string Name)
        {
            var chr = new Models.Character();

            chr.Name = Name;

            chr.MightPool = 10;
            chr.SpeedPool = 10;
            chr.IntPool = 10;

            chr.Tier = 1;
            chr.RecoveryDie = 6;
            chr.RecoveryMod = 0;

            chr.Player = "TestPlayer#1234";
            chr.Cyphers = new List<Models.CharacterCypher>();
            chr.RecoveryRolls = new List<Models.CharacterRecoveryRoll>();

            chr.RecoveryRolls.Add(new Models.CharacterRecoveryRoll { IsUsed = false, RollName = "first" });
            chr.RecoveryRolls.Add(new Models.CharacterRecoveryRoll { IsUsed = false, RollName = "second" });
            chr.RecoveryRolls.Add(new Models.CharacterRecoveryRoll { IsUsed = false, RollName = "third" });
            chr.RecoveryRolls.Add(new Models.CharacterRecoveryRoll { IsUsed = false, RollName = "fourth" });

            var cyls = (IEnumerable<Models.Cypher>)await Utilities.CypherHelper.GetRandomCypherAsync(2);

            chr.Cyphers = cyls.Select(x => new Models.CharacterCypher()
            {
                CypherId = x.CypherId,
                Effect = x.Effect,
                LevelBonus = x.LevelBonus,
                LevelDie = x.LevelDie,
                Name = x.Name,
                Source = x.Source,
                Type = x.Type
            }).ToList();

            chr.Inventory = new List<Models.CharacterInventory>();
            chr.Inventory.Add(new Models.CharacterInventory { ItemName = "Backpack", Qty = 1 });
            chr.Inventory.Add(new Models.CharacterInventory { ItemName = "Torch", Qty = 5 });

            return chr;
        }
    }
}
