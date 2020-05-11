using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherBot.Core.Models;
using DSharpPlus.CommandsNext;

namespace CypherBot.Data
{
    public static class CharacterList
    {
        public static List<Character> Characters { get; } = new List<Character>();

        public static async Task<Character> GetCurrentPlayersCharacterAsync(CommandContext ctx)
        {
            var chr = Characters.FirstOrDefault(x => x.Player == ctx.Member.Username + ctx.Member.Discriminator);

            if (chr == null)
            {
                await ctx.RespondAsync("Hey!  you don't have any characters!");
            }

            return chr;
        }

        public static async Task<string> GetCurrentCharacterCyphersAsync(CommandContext ctx)
        {
            var chr = await GetCurrentPlayersCharacterAsync(ctx);

            var response = "Here are your current cyphers:" + Environment.NewLine;

            foreach (var cypher in chr.Cyphers)
            {
                response += "**Name:** " + cypher.Name + Environment.NewLine;
                response += "**Level:** " + cypher.Level + Environment.NewLine;
                response += "**Effect:** " + cypher.Effect + Environment.NewLine + Environment.NewLine;
            }

            return response;
        }

        public static async Task<string> GetCurrentCharacterArtifactsAsync(CommandContext ctx)
        {
            var chr = await GetCurrentPlayersCharacterAsync(ctx);

            var response = "Here are your current artifacts:" + Environment.NewLine;

            foreach (var artifact in chr.Artifacts)
            {
                response += "**Name:** " + artifact.Name + Environment.NewLine;
                response += "**Level:** " + artifact.Level + Environment.NewLine;
                response += "**Effect:** " + artifact.Effect + Environment.NewLine + Environment.NewLine;
            }

            return response;
        }

        public static async Task<string> GetCurrentCharacterInventoryAsync(CommandContext ctx)
        {
            var chr = await GetCurrentPlayersCharacterAsync(ctx);

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
