using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace CypherBot.Commands
{
    [Group("dice")]
    public class DiceCommands
    {

        [Command("roll")]
        public async Task Random(CommandContext ctx, int max)
        {
            var rnd = new Random();
            var dieroll = rnd.Next(1, max);
            await ctx.RespondAsync($"🎲 Your random number is: {dieroll}");
        }

        [Command("rollcypher")]
        public async Task CypherRoll(CommandContext ctx)
        {
            var rnd = new Random();
            var dieroll = rnd.Next(1, 20);
            var levelbeats = Math.Floor((decimal)dieroll / 3);
            await ctx.RespondAsync($"🎲 Your random number is: {dieroll} and beats level: {levelbeats}");
        }
    }
}
