﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using CypherBot.Core.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Internal;
using CypherBot.Core.Services;
using System.IO;

namespace CypherBot.Commands
{
    public class CypherCommands
    {
        public class DiceCommands
        {
            [Command("action")]
            [Aliases("act")]
            public async Task RollAction(CommandContext ctx)
            {
                var rnd = RandomGenerator.GetRandom();
                var dieroll = rnd.Next(1, 20);
                var levelbeats = Math.Floor((decimal)dieroll / 3);
                await ctx.RespondAsync($"{ctx.Member.DisplayName} 🎲 Your die roll is: {dieroll} and beats level: {levelbeats}");
            }

            [Command("rolldie")]
            public async Task Random(CommandContext ctx, int max)
            {
                var rnd = RandomGenerator.GetRandom();
                var dieroll = rnd.Next(1, max);
                await ctx.RespondAsync($"🎲 Your random number is: {dieroll}");
            }
        }

        [Group("use")]
        [Description("Commands for using things")]
        public class UseCommands
        {
            [Command("cypher")]
            public async Task UseCharacterCyphers(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); // await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);;

                if (chr == null)
                {
                    await ctx.RespondAsync("Hey!  you don't have any characters!");
                }

                var responses = new List<string>();
                responses.Add("What cypher do you want to use?" + Environment.NewLine);

                responses.Add("Here are your cyphers:");
                int i = 1;
                foreach (var cypher in chr.Cyphers)
                {
                    responses.Add($"{i++}. ");
                    responses.Add("**Name:** " + cypher.Name);
                    responses.Add("**Level:** " + cypher.Level);
                    responses.Add("**Effect:** " + cypher.Effect + Environment.NewLine);
                }

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                responses.Clear();

                if (userResponse == null)
                {
                    await ctx.RespondAsync("Sorry, you didn't respond timely enough.");
                    return;
                }

                if (!int.TryParse(userResponse.Message.Content, out int cypherUsed) || (cypherUsed <= 0 || cypherUsed >= i))
                {
                    await ctx.RespondAsync("Sorry, the response was either bad or out of the range of the cypher list.");
                    return;
                }

                var oldcypher = chr.Cyphers[cypherUsed - 1].Name;
                await ctx.RespondAsync($"Using: {oldcypher}");
                chr.Cyphers.RemoveAt(cypherUsed - 1);

                responses.Add("Here are your remaining cyphers:");
                foreach (var cypher in chr.Cyphers)
                {
                    responses.Add("**Name:** " + cypher.Name);
                }

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));
            }

            [Command("inventory")]
            [Aliases("item", "items")]
            public async Task UseInventory(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); ;

                if (chr == null)
                {
                    await ctx.RespondAsync("Hey!  you don't have any characters!");
                    return;
                }

                if (!chr.Inventory.Any())
                {
                    await ctx.RespondAsync($"{chr.Name} doesn't have any items.");
                    return;
                }

                var responses = new List<string>();
                responses.Add("What item do you want to use?" + Environment.NewLine);

                responses.Add("Here is your inventory:");
                int i = 1;
                foreach (var inv in chr.Inventory)
                {
                    responses.Add($"{i++}.  {inv.ItemName}: {inv.Qty}x");

                }

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                responses.Clear();

                if (userResponse == null)
                {
                    await ctx.RespondAsync("Sorry, you didn't respond timely enough.");
                    return;
                }

                if (!int.TryParse(userResponse.Message.Content, out int inventoryItem) || (inventoryItem <= 0 || inventoryItem >= i))
                {
                    await ctx.RespondAsync("Sorry, the response was either bad or out of the range of the list.");
                    return;
                }

                var selectedInventoryItem = chr.Inventory[inventoryItem - 1];

                if (selectedInventoryItem.Qty > 1)
                {
                    await ctx.RespondAsync("How many do you want to use?");
                    userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                    if (userResponse == null)
                    {
                        await ctx.RespondAsync("Sorry, you didn't respond timely enough.");
                        return;
                    }

                    if (!int.TryParse(userResponse.Message.Content, out int invQty) || (invQty <= 0 || invQty > selectedInventoryItem.Qty))
                    {
                        await ctx.RespondAsync("Sorry, the response was either bad or more than you have.");
                        return;
                    }

                    if (selectedInventoryItem.Qty - invQty == 0)
                    {
                        await ctx.RespondAsync("You've used it all up, do you want to remove it from your inventory? (y/n)");
                        userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                        if (userResponse == null)
                        {
                            await ctx.RespondAsync("Sorry, you didn't respond timely enough.");
                            return;
                        }

                        if (userResponse.Message.Content.ToLower() == "y")
                        {
                            chr.Inventory.RemoveAt(inventoryItem - 1);
                        }
                        else if (userResponse.Message.Content.ToLower() == "n")
                        {
                            chr.Inventory[inventoryItem - 1].Qty -= invQty;
                        }
                        else
                        {
                            await ctx.RespondAsync("Sorry, invalid response.");
                            return;
                        }
                    }
                    else
                    {
                        chr.Inventory[inventoryItem - 1].Qty -= invQty;
                    }
                }
                else
                {
                    await ctx.RespondAsync("You've used it all up, do you want to remove it from your inventory? (y/n)");
                    userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                    if (userResponse == null)
                    {
                        await ctx.RespondAsync("Sorry, you didn't respond timely enough.");
                        return;
                    }

                    if (userResponse.Message.Content.ToLower() == "y")
                    {
                        chr.Inventory.RemoveAt(inventoryItem - 1);
                    }
                    else if (userResponse.Message.Content.ToLower() == "n")
                    {
                        chr.Inventory[inventoryItem - 1].Qty = 0;
                    }
                    else
                    {
                        await ctx.RespondAsync("Sorry, invalid response.");
                        return;
                    }
                }

                await ctx.RespondAsync($"Used: {selectedInventoryItem.ItemName}");

                responses.Add("Here is your current inventory:");
                foreach (var inv in chr.Inventory)
                {
                    responses.Add($"{inv.ItemName} : {inv.Qty}x");
                }

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));
            }
        }

        [Group("add")]
        [Description("Commands for adding things")]
        public class AddCommands
        {
            [Command("cypher")]
            public async Task AddCypher(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                var rnd = RandomGenerator.GetRandom();

                if (chr == null)
                {
                    return;
                }

                var cy = await CypherHelper.GetRandomCypherAsync();

                var cypher = new CharacterCypher()
                {
                    CypherId = cy.CypherId,
                    Effect = cy.Effect,
                    LevelBonus = cy.LevelBonus,
                    LevelDie = cy.LevelDie,
                    Level = cy.Level,
                    Name = cy.Name,
                    Source = cy.Source,
                    Type = cy.Type,
                    IsIdentified = false,
                    Form = cy.Forms.ToList()[rnd.Next(0, cy.Forms.Count() - 1)].FormDescription,
                    EffectOption = cy.EffectOptions.Count() == 0 ? "" : cy.EffectOptions.ToList()[rnd.Next(0, cy.EffectOptions.Count() - 1)].EffectDescription
                };

                var responses = new List<string>();

                responses.Add($"Here is what you found:");
                responses.Add($"**Name:** {cypher.Name}");
                responses.Add($"**Level:** {cypher.Level}");
                responses.Add($"**Effect:** {cypher.Effect}");
                responses.Add($"Do you wish to keep this one? (y/n)");

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                if (userResponse.Message.Content.ToLower() == "y")
                {
                    chr.Cyphers.Add(cypher);
                    await ctx.RespondAsync($"{cypher.Name} Added!");
                }
                else if (userResponse.Message.Content.ToLower() == "n")
                {
                    await ctx.RespondAsync("It's thrown away.");
                }
                else
                {
                    await ctx.RespondAsync("Sorry, I didn't understand that.");
                }
            }

            [Command("artifact")]
            public async Task AddArtifact(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    return;
                }

                var art = await ArtifactHelper.GetRandomArtifactAsync();
                var quirk = await ArtifactHelper.GetRandomArtifactQuirkAsync();
                var artifact = new CharacterArtifact()
                {
                    ArtifactId = art.ArtifactId,
                    Effect = art.Effect,
                    LevelBonus = art.LevelBonus,
                    LevelDie = art.LevelDie,
                    Level = art.Level,
                    Name = art.Name,
                    Source = art.Source,
                    Form = art.Form,
                    Depletion = art.Depletion,
                    Genre = art.Genre,
                    IsIdentified = false,
                    Quirk = quirk.Quirk
                };

                var responses = new List<string>();

                responses.Add($"Here is what you found:");
                responses.Add($"**Name:** {artifact.Name}");
                responses.Add($"**Level:** {artifact.Level}");
                responses.Add($"**Form:** {artifact.Form}");
                responses.Add($"**Genre:** {artifact.Genre}");
                responses.Add($"**Effect:** {artifact.Effect}");
                responses.Add($"**Depletion:** {artifact.Depletion}");
                responses.Add($"**Quirk:** {artifact.Quirk}");

                responses.Add($"Do you wish to keep this one? (y/n)");

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                if (userResponse.Message.Content.ToLower() == "y")
                {
                    chr.Artifacts.Add(artifact);
                    await ctx.RespondAsync($"{artifact.Name} Added!");
                }
                else if (userResponse.Message.Content.ToLower() == "n")
                {
                    await ctx.RespondAsync("It's thrown away.");
                }
                else
                {
                    await ctx.RespondAsync("Sorry, I didn't understand that.");
                }
            }

            [Command("inventory")]
            [Aliases("item", "items")]
            public async Task AddInventory(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                var responses = new List<string>();
                responses.Add("What Item do you want to add?");
                responses.Add("0. Add a new Item.");
                responses.Add(await Data.CharacterList.GetCurrentCharacterInventoryAsync(ctx));

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var selection = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                if (selection == null)
                {
                    await ctx.RespondAsync("Sorry, you didn't get back to me timely.");
                    return;
                }

                var success = int.TryParse(selection.Message.Content, out int selectionIndex);

                if (!success || (selectionIndex < 0 || selectionIndex > chr.Inventory.Count))
                {
                    await ctx.RespondAsync("Sorry, I didn't understand that.");
                    return;
                }

                if (selectionIndex == 0)
                {
                    await ctx.RespondAsync("What do you want to add?");
                    var descresponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                    if (descresponse == null)
                    {
                        await ctx.RespondAsync("Sorry, you didn't get back to me timely.");
                        return;
                    }

                    await ctx.RespondAsync("how many do you want to add?");

                    var qtyResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                    if (qtyResponse == null)
                    {
                        await ctx.RespondAsync("Sorry, you didn't get back to me timely.");
                        return;
                    }

                    success = int.TryParse(qtyResponse.Message.Content, out int itemQty);

                    if (!success || itemQty < 0)
                    {
                        await ctx.RespondAsync("Sorry, I didn't understand that.");
                        return;
                    }

                    chr.Inventory.Add(new CharacterInventory { ItemName = descresponse.Message.Content, Qty = itemQty });
                    await ctx.RespondAsync($"Added {itemQty} {descresponse.Message.Content}");
                }
                else
                {
                    await ctx.RespondAsync("How many do you want to add?");
                    var addResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                    if (addResponse == null)
                    {
                        await ctx.RespondAsync("Sorry, you didn't get back to me timely.");
                        return;
                    }

                    success = int.TryParse(addResponse.Message.Content, out int itemQty);

                    if (!success || itemQty < 0)
                    {
                        await ctx.RespondAsync("Sorry, I didn't understand that.");
                        return;
                    }

                    chr.Inventory[selectionIndex - 1].Qty += itemQty;

                    await ctx.RespondAsync($"You now have {chr.Inventory[selectionIndex - 1].Qty} of {chr.Inventory[selectionIndex - 1].ItemName}");
                }
            }
        }

        [Group("view")]
        [Description("Commands for viewing the character or character's items.")]
        public class ViewCommands
        {
            [Command("char")]
            [Aliases("character")]
            [Description("Shows the players character")]
            public async Task GetCharacter(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    return;
                }

                var responses = new List<string>();

                responses.Add($"Here is {ctx.Member.DisplayName}'s Character");
                responses.Add("**Name:** " + chr.Name);

                foreach (var pool in chr.Pools.OrderBy(x=>x.PoolIndex))
                {
                    responses.Add($"**{pool.Name} Pool:** {pool.PoolCurrentVaue}/{pool.PoolMax}");
                }

                responses.Add("**Cyphers Held:** " + (chr?.Cyphers.Count == 0 ? "None" : string.Join(", ", chr.Cyphers.Select(x => x.Name))));

                var response = string.Join(Environment.NewLine, responses);

                await ctx.RespondAsync(response);
            }

            [Command("cyphers")]
            [Aliases("cypher")]
            [Description("Lists all of the character's cyphers")]
            public async Task GetCharacterCyphers(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    return;
                }

                var response = await Data.CharacterList.GetCurrentCharacterCyphersAsync(ctx);

                await ctx.RespondAsync(response);
            }

            [Command("artifacts")]
            [Aliases("artifact")]
            [Description("Lists all of the character's artifacts")]
            public async Task GetCharacterArtifacts(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    return;
                }

                var response = await Data.CharacterList.GetCurrentCharacterArtifactsAsync(ctx);

                await ctx.RespondAsync(response);
            }

            [Command("inventory")]
            [Aliases("items", "item")]
            [Description("Shows the character's inventory")]
            public async Task ViewInventory(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    await ctx.RespondAsync("Hey!  you don't have any characters!");
                    return;
                }

                if (!chr.Inventory.Any())
                {
                    await ctx.RespondAsync($"{chr.Name} doesn't have any items.");
                    return;
                }

                await ctx.RespondAsync(string.Join(Environment.NewLine, await Data.CharacterList.GetCurrentCharacterInventoryAsync(ctx)));
            }

            [Command("ucyphers")]
            [Aliases("ucypher")]
            [Description("Lists all of the character's cyphers")]
            public async Task GetUnidentifiedCyphers(CommandContext ctx)
            {
                var ucyphers = await CypherHelper.GetAllUnidentifiedCyphersAsync();

                var response = "Wow!  look what I found out back!" + Environment.NewLine;

                foreach (var ucypher in ucyphers)
                {
                    response += "**Key:** " + ucypher.UnidentifiedCypherKey;
                    response += " **Level:** " + ucypher.Level;
                    response += " **Form:** " + ucypher.Form + Environment.NewLine;
                }

                await ctx.RespondAsync(response);
            }

            [Command("uartifacts")]
            [Aliases("uartifact")]
            [Description("Lists all of the character's cyphers")]
            public async Task GetUnidentifiedArtifacts(CommandContext ctx)
            {
                var uartifacts = await ArtifactHelper.GetAllUnidentifiedArtifactsAsync();

                var response = "Wow!  look what I found out back!" + Environment.NewLine;

                foreach (var uartifact in uartifacts)
                {
                    response += "**Key:** " + uartifact.UnidentifiedArtifactKey;
                    response += " **Level:** " + uartifact.Level;
                    response += " **Form:** " + uartifact.Form + Environment.NewLine;
                }

                await ctx.RespondAsync(response);
            }
        }

        [Group("roll")]
        [Description("Commands to roll up items.")]
        public class RollCommands
        {
            [Command("cypher")]
            [Description("Gets a random Cypher")]
            public async Task RandomCypher(CommandContext ctx)
            {
                //var cyphers = Models.Cypher.GetCyphers().ToList();
                try
                {
                    var cypher = await CypherHelper.GetRandomCypherAsync();
                    var rnd = RandomGenerator.GetRandom();

                    var cf = cypher.Forms.ToList()[rnd.Next(0, cypher.Forms.Count() - 1)];
                    var response = "Wow!  look what I found out back!" + Environment.NewLine;
                    response += "**Name:** " + cypher.Name + Environment.NewLine;
                    response += "**Level:** " + cypher.Level + Environment.NewLine;
                    response += "**Form:** " + cf.Form + " - " + cf.FormDescription + Environment.NewLine;
                    response += "**Effect:** " + cypher.Effect;
                    if (cypher.EffectOptions.Any())
                    {
                        response += Environment.NewLine + "Effects:";
                        foreach (var eo in cypher.EffectOptions.OrderBy(x => x.StartRange))
                        {
                            response += Environment.NewLine + $"{eo.StartRange}-{eo.EndRange}: {eo.EffectDescription}";
                        }

                        var rn = rnd.Next(cypher.EffectOptions.Min(x => x.StartRange), cypher.EffectOptions.Max(x => x.EndRange));

                        var effectOption = cypher.EffectOptions.SingleOrDefault(x => x.StartRange <= rn && x.EndRange >= rn);

                        response += Environment.NewLine + $"Rolled {rn} Chosen Effect:  {effectOption.StartRange}-{effectOption.EndRange}: {effectOption.EffectDescription}";
                    }

                    await ctx.RespondAsync(response);
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Oops! Something went wrong!  I gotta get the code monkey on that.");
                    throw ex;
                }
            }

            [Command("ucypher")]
            [Description("Gets a random unidentified Cypher")]
            public async Task RandomUCypher(CommandContext ctx)
            {
                //var cyphers = Models.Cypher.GetCyphers().ToList();
                try
                {
                    var cy = await CypherHelper.GetRandomCypherAsync();
                    var rnd = RandomGenerator.GetRandom();

                    var cf = cy.Forms.ToList()[rnd.Next(0, cy.Forms.Count() - 1)];

                    var cypher = new UnidentifiedCypher()
                    {
                        UnidentifiedCypherId = cy.CypherId,
                        UnidentifiedCypherKey = RandomGenerator.GetRandomDesination(4),
                        Effect = cy.Effect,
                        LevelBonus = cy.LevelBonus,
                        LevelDie = cy.LevelDie,
                        Level = cy.Level,
                        Name = cy.Name,
                        Source = cy.Source,
                        Type = cy.Type,
                        IsIdentified = false,
                        Form = cf.Form + " - " + cf.FormDescription,
                        EffectOption = cy.EffectOptions.Count() == 0 ? "" : cy.EffectOptions.ToList()[rnd.Next(0, cy.EffectOptions.Count() - 1)].EffectDescription
                    };

                    var response = "Wow!  look what I found out back!" + Environment.NewLine;
                    response += "**Form:** " + cf.Form + " - " + cf.FormDescription + Environment.NewLine;
                    response += "**Key:** " + cypher.UnidentifiedCypherKey + Environment.NewLine;
                    response += "I stuffed it into the unidentified cypher box.";

                    await CypherHelper.SaveUnidentifiedCypherAsync(cypher);

                    await ctx.RespondAsync(response);
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Oops! Something went wrong!  I gotta get the code monkey on that.");
                    throw ex;
                }
            }

            [Command("artifact")]
            [Description("Gets a random Artifact")]
            public async Task RandomArtifact(CommandContext ctx)
            {
                //var cyphers = Models.Cypher.GetCyphers().ToList();
                try
                {
                    var artifact = await ArtifactHelper.GetRandomArtifactAsync();
                    var quirk = await ArtifactHelper.GetRandomArtifactQuirkAsync();
                    var response = "Wow!  look what I found out back!" + Environment.NewLine;
                    response += "**Name:** " + artifact.Name + Environment.NewLine;
                    response += "**Level:** " + artifact.Level + Environment.NewLine;
                    response += "**Form:** " + artifact.Form + Environment.NewLine;
                    response += "**Genre:** " + artifact.Genre + Environment.NewLine;
                    response += "**Effect:** " + artifact.Effect + Environment.NewLine;
                    response += "**Depletion:** " + artifact.Depletion + Environment.NewLine;
                    response += "**Quirk:** " + quirk.Quirk;

                    await ctx.RespondAsync(response);
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Oops! Something went wrong!  I gotta get the code monkey on that.");
                    throw ex;
                }
            }

            [Command("uartifact")]
            [Description("Gets a random Artifact")]
            public async Task RandomUArtifact(CommandContext ctx)
            {
                try
                {
                    var artifact = await ArtifactHelper.GetRandomArtifactAsync();
                    var quirk = await ArtifactHelper.GetRandomArtifactQuirkAsync();

                    UnidentifiedArtifact unidentifiedArtifact = new UnidentifiedArtifact
                    {
                        UnidentifiedArtifactKey = RandomGenerator.GetRandomDesination(4),
                        Effect = artifact.Effect,
                        LevelBonus = artifact.LevelBonus,
                        LevelDie = artifact.LevelDie,
                        Level = artifact.Level,
                        Name = artifact.Name,
                        Source = artifact.Source,
                        IsIdentified = false,
                        Form = artifact.Form,
                        Quirk = quirk.Quirk,
                        Depletion = artifact.Depletion
                    };

                    var response = "Wow!  look what I found out back!" + Environment.NewLine;
                    response += "**Level:** " + artifact.Level + Environment.NewLine;
                    response += "**Form:** " + artifact.Form + Environment.NewLine;

                    await ctx.RespondAsync(response);

                    await ArtifactHelper.SaveUnidentifiedArtifactAsync(unidentifiedArtifact);
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Oops! Something went wrong!  I gotta get the code monkey on that.");
                    throw ex;
                }
            }

            [Command("oddity")]
            [Description("Gets a random Oddity")]
            public async Task RandomOddity(CommandContext ctx)
            {
                //var cyphers = Models.Cypher.GetCyphers().ToList();
                try
                {
                    var oddity = await OddityHelper.GetRandomOddityAsync();

                    var response = "Wow!  look what I found out back!" + Environment.NewLine;
                    response += "**Oddity:** " + oddity.OddityDescription;

                    await ctx.RespondAsync(response);
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Oops! Something went wrong!  I gotta get the code monkey on that.");
                    throw ex;
                }
            }

            [Command("char")]
            [Aliases("character")]
            [Description("Rolls a Character up for a player.")]
            public async Task GenerateCharacter(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();
                var loop = true;
                try
                {
                    var chr = new Character();

                    while (loop)
                    {
                        await ctx.RespondAsync("What is your character's name?");
                        var msgName = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                        if (msgName != null)
                        {
                            chr.Name = msgName.Message.Content;
                            loop = false;
                        }
                    }

                    loop = true;
                    while (loop)
                    {
                        await ctx.RespondAsync("What is your character's type?");
                        var msgName = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                        if (msgName != null)
                        {
                            chr.Type = msgName.Message.Content;
                            loop = false;
                        }
                    }

                    loop = true;
                    while (loop)
                    {
                        await ctx.RespondAsync("What is your character's descriptor?");
                        var msgName = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                        if (msgName != null)
                        {
                            chr.Descriptor = msgName.Message.Content;
                            loop = false;
                        }
                    }

                    loop = true;
                    while (loop)
                    {
                        await ctx.RespondAsync("What is your character's Focus?");
                        var msgName = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                        if (msgName != null)
                        {
                            chr.Focus = msgName.Message.Content;
                            loop = false;
                        }
                    }

                    //while (loop)
                    //{
                    //    var msgMight = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id && xm.Content.ToLower() == "What is your character's Might?", TimeSpan.FromMinutes(1));
                    //    if (msgName != null)
                    //    {
                    //        chr.Name = msgName.Message.Content;
                    //        loop = false;
                    //    }
                    //}

                    chr.Pools.Add(new CharacterPool() { Name = "Might", PoolIndex = 0, PoolMax = 10, PoolCurrentVaue = 10 });
                    chr.Pools.Add(new CharacterPool() { Name = "Speed", PoolIndex = 1, PoolMax = 10, PoolCurrentVaue = 10 });
                    chr.Pools.Add(new CharacterPool() { Name = "Intellect", PoolIndex = 2, PoolMax = 10, PoolCurrentVaue = 10 });

                    chr.Tier = 1;
                    chr.RecoveryDie = 6;
                    chr.RecoveryMod = 0;

                    chr.Player = ctx.Member.Username + ctx.User.Discriminator;
                    chr.Cyphers = new List<CharacterCypher>();
                    chr.RecoveryRolls = new List<CharacterRecoveryRoll>();

                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "first" });
                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "second" });
                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "third" });
                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "fourth" });

                    var cyls = (IEnumerable<Cypher>)await CypherHelper.GetRandomCypherAsync(2);

                    chr.Cyphers = cyls.Select(x => new CharacterCypher()
                    {
                        CypherId = x.CypherId,
                        Effect = x.Effect,
                        LevelBonus = x.LevelBonus,
                        LevelDie = x.LevelDie,
                        Level = x.Level,
                        Name = x.Name,
                        Source = x.Source,
                        Type = x.Type
                    }).ToList();

                    chr.Inventory = new List<CharacterInventory>();
                    chr.Inventory.Add(new CharacterInventory { ItemName = "Backpack", Qty = 1 });
                    chr.Inventory.Add(new CharacterInventory { ItemName = "Torch", Qty = 5 });

                    var response = $"Hey!  Here is a new character for {ctx.Member.DisplayName}" + Environment.NewLine;
                    response += "**Name:** " + chr.Name + Environment.NewLine;
                    response += $"is a {chr.Descriptor} {chr.Type} who {chr.Focus}" + Environment.NewLine;
                    foreach (var pool in chr.Pools.OrderBy(x => x.PoolIndex))
                    {
                        response += $"**{pool.Name} Pool:** {pool.PoolCurrentVaue}/{pool.PoolMax}" + Environment.NewLine;
                    }
                    response += "**Cyphers Held:** " + (chr?.Cyphers.Count == 0 ? "None" : string.Join(", ", chr.Cyphers.Select(x => x.Name)));

                    Data.CharacterList.Characters.Remove(Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.Username + ctx.User.Discriminator));
                    Data.CharacterList.Characters.Add(chr);

                    await ctx.RespondAsync(response);
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Oops! Something went wrong!  I gotta get rthe code monkey on that.");
                    throw ex;
                }
            }

            [Command("recovery")]
            [Description("Rolls the character's recovery roll and marks it as used")]
            public async Task RecoveryRoll(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); ;

                var roll = chr.RecoveryRolls.OrderBy(x => x.RecoveryRollId).FirstOrDefault(x => !x.IsUsed);

                if (roll == null)
                {
                    await ctx.RespondAsync("Error: No recovery rolls left!");
                    return;
                }

                roll.IsUsed = true;

                var rnd = RandomGenerator.GetRandom();
                var dieroll = rnd.Next(1, chr.RecoveryDie);

                await ctx.RespondAsync($"🎲 You recovered {dieroll + chr.RecoveryMod + chr.Tier}: Rolled: {dieroll} Mod: {chr.RecoveryMod} Tier: {chr.Tier} Using your {roll.RollName}");
            }
        }

        [Group("mod")]
        [Aliases("modify")]
        [Description("Commands for modifying attributes")]
        public class ModCommands
        {
            [Command("pool")]
            [Description("Modifys the pool of a character")]
            public async Task ModifyPool(CommandContext ctx, [Description("Pool to modify")]string pool, [Description("How much to modify it by. Negative numbers are valid")]int mod)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); //await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);;

                var selPool = chr.Pools.FirstOrDefault(x => x.Name.ToLower() == pool.ToLower());

                if (selPool == null)
                {
                    await ctx.RespondAsync($"Error: {pool} not a valid pool. please try again.");
                    return;
                }

                selPool.PoolCurrentVaue += mod;

                var response = $"{pool} pool Modified for {chr.Name}! new values are:" + Environment.NewLine;
                response += "**Name:** " + chr.Name + Environment.NewLine;
                foreach (var p in chr.Pools.OrderBy(x => x.PoolIndex))
                {
                    response += $"**{p.Name} Pool:** {p.PoolCurrentVaue}/{p.PoolMax}" + Environment.NewLine;
                }

                await ctx.RespondAsync(response);
            }

            [Command("tier")]
            [Description("Modifys the character's tier")]
            public async Task ModifyTier(CommandContext ctx, [Description("What the tier will be changed to.")] int mod)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                chr.Tier = mod;

                await ctx.RespondAsync($"Your tier has been updated to {mod}");
            }

            [Command("xp")]
            [Description("Modifies the character's XP")]
            public async Task ModifyXp(CommandContext ctx, [Description("What the XP will be changed to.")] int mod)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); ;

                chr.XP = mod;

                await ctx.RespondAsync($"Your XP has been updated to {mod}");
            }

            [Command("type")]
            [Description("Modifys the character's Type")]
            public async Task ModifyType(CommandContext ctx, [Description("What the Type will be changed to.")] string type)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); ;

                chr.Type = type;

                await ctx.RespondAsync($"Your Type has been updated to {type}");
            }

            [Command("descriptor")]
            [Aliases("desc")]
            [Description("Modifys the character's Descriptor")]
            public async Task ModifyDescriptor(CommandContext ctx, [Description("What the Descriptor will be changed to.")] string descriptor)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); ;

                chr.Descriptor = descriptor;

                await ctx.RespondAsync($"Your Descriptor has been updated to {descriptor}");
            }

            [Command("Focus")]
            [Description("Modifys the character's Focus")]
            public async Task ModifyFocus(CommandContext ctx, [Description("What the focus will be changed to.")] string focus)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx); ;

                chr.Focus = focus;

                await ctx.RespondAsync($"Your Focus has been updated to {focus}");
            }
        }

        [Group("export")]
        [Description("Commands for exporting data")]
        public class ExportCommands
        {
            [Command("char")]
            [Aliases("character")]
            [Description("Exports the current character")]
            public async Task ExportCharacter(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    await ctx.RespondAsync("Hey! you don't have any characters!");
                }


                try
                {
                    var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(chr, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                    if (jsonstr.Length > 2000)
                    {
                        var jsonStream = new MemoryStream(System.Text.ASCIIEncoding.Unicode.GetBytes(jsonstr));

                        jsonStream.Position = 0;
                        //var stream = new MemoryStream();
                        //using (var sw = new StreamWriter(stream))
                        //{
                        //    sw.Write(jsonstr);
                        //    sw.Flush();
                        //    stream.Position = 0;
                        //}
                        await ctx.RespondWithFileAsync(jsonStream, chr.Name + ".txt", "Here you go!");
                        return;
                    }
                    await ctx.RespondAsync("Keep it safe!" + Environment.NewLine + jsonstr.Replace('"', '\''));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                


                
            }
        }

        [Group("import")]
        [Description("Commands for importing data")]
        public class ImportCommands
        {
            [Command("char")]
            [Aliases("character")]
            [Description("Imports a character for the player")]
            public async Task ImportCharacter(CommandContext ctx, string chr)
            {
                Data.CharacterList.Characters.Remove(await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx));

                try
                {
                    var newchr = Newtonsoft.Json.JsonConvert.DeserializeObject<Character>(chr);
                    Data.CharacterList.Characters.Add(newchr);
                    await ctx.RespondAsync("All Done!");
                }
                catch (Exception ex)
                {
                    await ctx.RespondAsync("Something went wrong! Are you sure that's a character?");
                    throw ex;
                }

            }
        }

        [Group("save")]
        [Description("Commands for Saving Data")]
        public class SaveCommands
        {
            [Command("char")]
            [Aliases("character")]
            [Description("Saves a character for the player.")]
            public async Task SaveCharacter(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                if (chr == null)
                {
                    return;
                }
                try
                {
                    var chrString = JsonConvert.SerializeObject(chr, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                    await CharacterHelper.SaveCurrentCharacterAsync("", chr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }

        [Group("load")]
        [Description("Commands for loading data.")]
        public class LoadCommands
        {
            [Command("char")]
            [Aliases("character")]
            [Description("Load a character from the Player's saved characters.")]
            public async Task LoadCharacter(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                //var files = Data.FileIO.GetFilesInDatabase("Players\\" + ctx.Member.Username + ctx.Member.Discriminator);

                var chars = await CharacterHelper.GetCurrentPlayersCharactersAsync(ctx);

                var response = new List<string>();
                response.Add("Here are your saved characters:");
                foreach (var character in chars.OrderBy(x => x.Name).Select((model, i) => new { model, i }))
                {
                    response.Add($"{character.i + 1}. {character.model.Name}");
                }
                response.Add("What character do you wish to load? (0 to cancel)");

                await ctx.RespondAsync(string.Join(Environment.NewLine, response));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                if (userResponse.Message.Content.Trim() == "0")
                {
                    await ctx.RespondAsync("Thanks! Come again");
                }

                if (int.TryParse(userResponse.Message.Content.Trim(), out int selected))
                {
                    var chr = chars.OrderBy(x => x.Name).ToList()[selected - 1];

                    try
                    {
                        Data.CharacterList.Characters.Remove(Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.Username + ctx.User.Discriminator));
                        Data.CharacterList.Characters.Add(chr);

                        await ctx.RespondAsync($"Successfully loaded {chr.Name}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        [Group("identify")]
        [Aliases("id")]
        [Description("Commands for identifying objects.")]
        public class IdentifyCommands
        {
            [Command("cypher")]
            [Description("Identifies a cypher from the list of unidentified cyphers.")]
            public async Task IdentifyCypher(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                UnidentifiedCypher selectedCypher = null;

                var ucyphers = await CypherHelper.GetAllUnidentifiedCyphersAsync();

                var response = "What cypher would you like to identify? (Case Insensative) 0 to quit" + Environment.NewLine;

                foreach (var ucypher in ucyphers)
                {
                    response += "**Key:** " + ucypher.UnidentifiedCypherKey;
                    response += " **Level:** " + ucypher.Level;
                    response += " **Form:** " + ucypher.Form + Environment.NewLine;
                }

                await ctx.RespondAsync(response);

                while (true)
                {
                    var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                    if (userResponse.Message.Content == "0")
                    {
                        return;
                    }

                    selectedCypher = ucyphers.FirstOrDefault(x => x.UnidentifiedCypherKey.ToLower() == userResponse.Message.Content.ToLower());

                    if (selectedCypher != null)
                    {
                        break;
                    }
                    else
                    {
                        await ctx.RespondAsync("Sorry I didn't get that, Please try the command again");
                    }
                }

                response = "Wow!  look what I found out back!" + Environment.NewLine;
                response += "**Name:** " + selectedCypher.Name + Environment.NewLine;
                response += "**Level:** " + selectedCypher.Level + Environment.NewLine;
                response += "**Form:** " + selectedCypher.Form + Environment.NewLine;
                response += "**Effect:** " + selectedCypher.Effect + Environment.NewLine;
                response += "Removing from the List.";

                await ctx.RespondAsync(response);

                await CypherHelper.RemoveUnidentifiedCypherAsync(selectedCypher.UnidentifiedCypherId);
            }

            [Command("artifact")]
            [Description("Identifies a artifact from the list of unidentified cyphers.")]
            public async Task IdentifyArtifact(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                UnidentifiedArtifact selectedArtifact = null;

                var uartifacts = await ArtifactHelper.GetAllUnidentifiedArtifactsAsync();

                var response = "What artifact would you like to identify? (Case Insensative) 0 to quit" + Environment.NewLine;

                foreach (var uartifact in uartifacts)
                {
                    response += "**Key:** " + uartifact.UnidentifiedArtifactKey;
                    response += " **Level:** " + uartifact.Level;
                    response += " **Form:** " + uartifact.Form + Environment.NewLine;
                }

                await ctx.RespondAsync(response);

                while (true)
                {
                    var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                    if (userResponse.Message.Content == "0")
                    {
                        return;
                    }

                    selectedArtifact = uartifacts.FirstOrDefault(x => x.UnidentifiedArtifactKey.ToLower() == userResponse.Message.Content.ToLower());

                    if (selectedArtifact != null)
                    {
                        break;
                    }
                    else
                    {
                        await ctx.RespondAsync("Sorry I didn't get that, Please try the command again");
                    }
                }

                response = "Wow!  look what I found out back!" + Environment.NewLine;
                response += "**Name:** " + selectedArtifact.Name + Environment.NewLine;
                response += "**Level:** " + selectedArtifact.Level + Environment.NewLine;
                response += "**Form:** " + selectedArtifact.Form + Environment.NewLine;
                response += "**Effect:** " + selectedArtifact.Effect + Environment.NewLine;
                response += "**Quirk:** " + selectedArtifact.Quirk + Environment.NewLine;
                response += "Removing from the List.";

                await ctx.RespondAsync(response);

                await ArtifactHelper.RemoveUnidentifiedArtifactAsync(selectedArtifact.UnidentifiedArtifactId);
            }
        }

        [Group("reset")]
        [Description("Commands for Resetting.")]
        public class ResetCommands
        {
            [Command("Recovery")]
            [Description("Resets the current character's recovery rolls")]
            public async Task ResetRecoveryRolls(CommandContext ctx)
            {
                var chr = await Data.CharacterList.GetCurrentPlayersCharacterAsync(ctx);

                chr.RecoveryRolls.ForEach(x => x.IsUsed = false);

                string response = $"{chr.Name}'s recovery rolls are reset.";

                await ctx.RespondAsync(response);
            }
        }
    }
}
