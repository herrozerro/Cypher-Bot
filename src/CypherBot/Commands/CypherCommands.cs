﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using CypherBot.Models;
using Newtonsoft.Json;


namespace CypherBot.Commands
{
    public class CypherCommands
    {
        [Group("use")]
        [Description("Commands for using things")]
        public class UseCommands
        {
            [Command("cypher")]
            public async Task UseCharacterCyphers(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

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
            [Aliases("item","items")]
            public async Task UseInventory(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

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

                var chr = await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);

                if(chr == null)
                {
                    return;
                }

                var cypher = (Cypher) await Data.CypherList.GetRandomCypherAsync();

                var responses = new List<string>();

                responses.Add($"Here is what you found:");
                responses.Add($"**Name:** {cypher.Name}");
                responses.Add($"**Level:** {cypher.Level}");
                responses.Add($"**Effect:** {cypher.Effect}");
                responses.Add($"Do you wish to keep this one? (y/n)");

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));
                if(userResponse.Message.Content.ToLower() == "y")
                {
                    chr.Cyphers.Add(cypher);
                    await ctx.RespondAsync($"{cypher.Name} Added!");
                }else if (userResponse.Message.Content.ToLower() == "n")
                {
                    await ctx.RespondAsync("It's thrown away.");
                }
                else
                {
                    await ctx.RespondAsync("Sorry, I didn't understand that.");
                }
            }

            [Command("inventory")]
            [Aliases("item","items")]
            public async Task AddInventory(CommandContext ctx)
            {
                var interactivity = ctx.Client.GetInteractivityModule();

                var chr = await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);

                var responses = new List<string>();
                responses.Add("What Item do you want to add?");
                responses.Add("0. Add a new Item.");
                responses.Add(await Utilities.CharacterHelper.GetCurrentCharacterInventory(ctx));

                await ctx.RespondAsync(string.Join(Environment.NewLine, responses));

                var selection = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                if(selection == null)
                {
                    await ctx.RespondAsync("Sorry, you didn't get back to me timely.");
                    return;
                }

                var success = int.TryParse(selection.Message.Content, out int selectionIndex);

                if (!success || (selectionIndex <0 || selectionIndex > chr.Inventory.Count))
                {
                    await ctx.RespondAsync("Sorry, I didn't understand that.");
                    return;
                }

                if(selectionIndex == 0)
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

                    await ctx.RespondAsync($"You now have {chr.Inventory[selectionIndex - 1].Qty} of {chr.Inventory[selectionIndex-1].ItemName}");
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
                var chr = await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);

                if (chr == null)
                {
                    return;
                }

                var responses = new List<string>();

                responses.Add($"Here is {ctx.Member.DisplayName}'s Character");
                responses.Add("**Name:** " + chr.Name);
                responses.Add("**Might Pool:** " + chr.MightPool);
                responses.Add("**Speed Pool:** " + chr.SpeedPool);
                responses.Add("**Intellect Pool:** " + chr.IntPool);
                responses.Add("**Cyphers Held:** " + (chr?.Cyphers.Count == 0 ? "None" : string.Join(", ", chr.Cyphers.Select(x => x.Name))));

                var response = string.Join(Environment.NewLine, responses);

                await ctx.RespondAsync(response);
            }

            [Command("cyphers")]
            [Aliases("cypher")]
            [Description("Lists all of the character's cyphers")]
            public async Task GetCharacterCyphers(CommandContext ctx)
            {
                var chr = await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);

                if (chr == null)
                {
                    return;
                }

                var response = await Utilities.CharacterHelper.GetCurrentCharacterCyphers(ctx);

                await ctx.RespondAsync(response);
            }

            [Command("inventory")]
            [Aliases("items","item")]
            [Description("Shows the character's inventory")]
            public async Task ViewInventory(CommandContext ctx)
            {
                var chr = await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);

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

                await ctx.RespondAsync(string.Join(Environment.NewLine,await Utilities.CharacterHelper.GetCurrentCharacterInventory(ctx)));
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
                    var cypher = (Cypher) await Data.CypherList.GetRandomCypherAsync();

                    var response = "Wow!  look what I found out back!" + Environment.NewLine;
                    response += "**Name:** " + cypher.Name + Environment.NewLine;
                    response += "**Level:** " + cypher.Level + Environment.NewLine;
                    response += "**Effect:** " + cypher.Effect;

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

                    //while (loop)
                    //{
                    //    var msgMight = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id && xm.Content.ToLower() == "What is your character's Might?", TimeSpan.FromMinutes(1));
                    //    if (msgName != null)
                    //    {
                    //        chr.Name = msgName.Message.Content;
                    //        loop = false;
                    //    }
                    //}

                    chr.MightPool = 10;
                    chr.SpeedPool = 10;
                    chr.IntPool = 10;

                    chr.Tier = 1;
                    chr.RecoveryDie = 6;
                    chr.RecoveryMod = 0;

                    chr.Player = ctx.Member.Username + ctx.User.Discriminator;
                    chr.Cyphers = new List<Cypher>();
                    chr.RecoveryRolls = new List<CharacterRecoveryRoll>();

                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "first" });
                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "second" });
                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "third" });
                    chr.RecoveryRolls.Add(new CharacterRecoveryRoll { IsUsed = false, RollName = "fourth" });

                    var cyls = (IEnumerable<Cypher>) await Data.CypherList.GetRandomCyphersAsync(2);

                    chr.Cyphers = cyls.ToList();

                    chr.Inventory = new List<CharacterInventory>();
                    chr.Inventory.Add(new CharacterInventory { ItemName = "Backpack", Qty = 1 });
                    chr.Inventory.Add(new CharacterInventory { ItemName = "Torch", Qty = 5 });

                    var response = $"Hey!  Here is a new character for {ctx.Member.DisplayName}" + Environment.NewLine;
                    response += "**Name:** " + chr.Name + Environment.NewLine;
                    response += "**Might Pool:** " + chr.MightPool + Environment.NewLine;
                    response += "**Speed Pool:** " + chr.SpeedPool + Environment.NewLine;
                    response += "**Intellect Pool:** " + chr.IntPool + Environment.NewLine;
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
            public async Task RecoveryRoll(CommandContext ctx,[Description("Which roll do you want to make? 1,2,3,4,(5 if you are sturdy, etc...)")] int rollIndex)
            {
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

                if(chr.RecoveryRolls[rollIndex - 1].IsUsed)
                {
                    await ctx.RespondAsync("Error: that recover roll is used already");
                    return;
                }

                chr.RecoveryRolls[rollIndex - 1].IsUsed = true;

                var rnd = new Random();
                var dieroll = rnd.Next(1, chr.RecoveryDie);
                
                await ctx.RespondAsync($"🎲 You recovered {dieroll + chr.RecoveryMod + chr.Tier}: Rolled: {dieroll} Mod: {chr.RecoveryMod} Tier: {chr.Tier}");
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
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

                if (pool.ToLower() == "might")
                {
                    chr.MightPool += mod;
                }
                if (pool.ToLower() == "speed")
                {
                    chr.SpeedPool += mod;
                }
                if (pool.ToLower() == "int")
                {
                    chr.IntPool += mod;
                }

                var response = $"{pool} pool Modified for {chr.Name}! new values are:" + Environment.NewLine;
                response += "**Name:** " + chr.Name + Environment.NewLine;
                response += "**Might Pool:** " + chr.MightPool + Environment.NewLine;
                response += "**Speed Pool:** " + chr.SpeedPool + Environment.NewLine;
                response += "**Intellect Pool:** " + chr.IntPool + Environment.NewLine;

                await ctx.RespondAsync(response);
            }

            [Command("tier")]
            [Description("Modifys the character's tier")]
            public async Task ModifyTier(CommandContext ctx, [Description("What the tier will be changed to.")] int mod)
            {
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

                chr.Tier = mod;

                await ctx.RespondAsync($"Your tier has been updated to {mod}");
            }

            [Command("xp")]
            [Description("Modifies the character's XP")]
            public async Task ModifyXp(CommandContext ctx, [Description("What the XP will be changed to.")] int mod)
            {
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

                chr.XP = mod;

                await ctx.RespondAsync($"Your XP has been updated to {mod}");
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
                var chr = Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName);

                if (chr == null)
                {
                    await ctx.RespondAsync("Hey! you don't have any characters!");
                }

                var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(chr, Formatting.Indented);

                await ctx.RespondAsync("Keep it safe!" + Environment.NewLine + jsonstr.Replace('"', '\''));
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
                Data.CharacterList.Characters.Remove(Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.DisplayName));

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
            [Description("Saves a character for the player")]
            public async Task SaveCharacter(CommandContext ctx)
            {
                var chr = await Utilities.CharacterHelper.GetCurrentPlayersCharacter(ctx);

                if(chr == null)
                {
                    return;
                }

                var chrString = Newtonsoft.Json.JsonConvert.SerializeObject(chr);

                await Data.FileIO.SaveFileString(chr.Name.Substring(0,Math.Min(chr.Name.Length, 25)), ctx.Member.Username+ctx.Member.Discriminator,chrString);
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

                var files = Data.FileIO.GetFilesInDatabase("Players\\" + ctx.Member.Username + ctx.Member.Discriminator);

                var response = new List<string>();
                response.Add("Here are your saved characters:");
                foreach (var file in files)
                {
                    response.Add(file);
                }
                response.Add("What character do you wish to load? (0 to cancel)");

                await ctx.RespondAsync(string.Join(Environment.NewLine, response));

                var userResponse = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(1));

                if(userResponse.Message.Content.Trim() == "0")
                {
                    await ctx.RespondAsync("Thanks!  Come again");
                }

                if (files.Contains(userResponse.Message.Content))
                {
                    var file = files.First(x => x.Contains(userResponse.Message.Content));

                    var chr = await Data.FileIO.GetFileString(file, "Players\\" + ctx.User.Username + ctx.User.Discriminator);

                    try
                    {
                        var character = JsonConvert.DeserializeObject<Character>(chr);
                        Data.CharacterList.Characters.Remove(Data.CharacterList.Characters.FirstOrDefault(x => x.Player == ctx.Member.Username + ctx.User.Discriminator));
                        Data.CharacterList.Characters.Add(character);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}