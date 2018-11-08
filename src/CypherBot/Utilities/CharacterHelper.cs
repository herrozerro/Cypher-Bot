using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;


namespace CypherBot.Utilities
{
    public static class CharacterHelper
    {
        public static async Task<Models.Character> GetCurrentPlayersCharacter(CommandContext ctx)
        {
            var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.Username+ctx.Member.Discriminator);

            if (chr == null)
            {
                await ctx.RespondAsync("Hey!  you don't have any characters!");
            }

            return chr;
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
    }
}
