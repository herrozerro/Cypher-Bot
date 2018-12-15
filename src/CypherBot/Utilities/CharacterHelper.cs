using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.EntityFrameworkCore;


namespace CypherBot.Utilities
{
    public static class CharacterHelper
    {
        public static async Task<Models.Character> GetCurrentPlayersCharacter(CommandContext ctx)
        {
            var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.Username + ctx.Member.Discriminator);

            if (chr == null)
            {
                await ctx.RespondAsync("Hey!  you don't have any characters!");
            }

            return chr;
        }

        public static async Task<List<Models.Character>> GetCurrentPlayersCharacters(CommandContext ctx)
        {
            using (var db = new DataAccess.Repos.CypherContext())
            {
                var chars = await db.Characters
                    .Include(x => x.Cyphers)
                    .Include(x => x.Inventory)
                    .Include(x => x.RecoveryRolls).Where(x => x.Player == ctx.Member.Username + ctx.Member.Discriminator).ToListAsync();

                return chars;
            }
        }

        public static async Task<string> GetCurrentCharacterCyphers(CommandContext ctx)
        {
            var chr = await GetCurrentPlayersCharacter(ctx);

            var response = "Here are your current cyphers:" + Environment.NewLine;

            foreach (var cypher in chr.Cyphers)
            {
                response += "**Name:** " + cypher.Name + Environment.NewLine;
                response += "**Level:** " + cypher.Level + Environment.NewLine;
                response += "**Effect:** " + cypher.Effect + Environment.NewLine + Environment.NewLine;
            }

            return response;
        }

        public static async Task<string> GetCurrentCharacterInventory(CommandContext ctx)
        {
            var chr = await GetCurrentPlayersCharacter(ctx);

            var responses = new List<string>();

            responses.Add("Here is your inventory:");
            var i = 1;
            foreach (var inv in chr.Inventory)
            {
                responses.Add($"{i++}. {inv.ItemName} : {inv.Qty}x");
            }

            return string.Join(Environment.NewLine, responses);
        }



        public static void SaveCurrentCharacter(string playerId, Models.Character charToSave)
        {
            using (var db = new DataAccess.Repos.CypherContext())
            {
                var chr = db.Characters
                    .Include(x => x.Cyphers)
                    .Include(x => x.Inventory)
                    .Include(x => x.RecoveryRolls)
                    .FirstOrDefault(x => x.CharacterId == charToSave.CharacterId);

                if (chr == null)
                {
                    db.Characters.Add(charToSave);
                }
                else
                {
                    db.Entry(chr).CurrentValues.SetValues(charToSave);

                    foreach (var cy in chr.Cyphers)
                    {
                        if (!charToSave.Cyphers.Any(x => x.CypherId == cy.CypherId))
                        {
                            db.Remove(cy);
                        }

                    }
                    
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }
    }
}
