using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CypherBot
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;
        static IConfiguration Configuration { get; set; }
        static InteractivityModule interactivity;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("secrets.json");

            Configuration = builder.Build();

            // Use this if you want App_Data off your project root folder
            string baseDir = Directory.GetCurrentDirectory();
            
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(baseDir, "DataFiles"));

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Configuration["token"],
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = Configuration["commandPrefix"]
            });

            commands.RegisterCommands<Commands.DiceCommands>();
            commands.RegisterCommands<Commands.CypherCommands>();
            commands.RegisterCommands<Commands.AdminCommands>();

            interactivity = discord.UseInteractivity(new InteractivityConfiguration() {
                
            });

            //List<Models.Cypher> cyphers = await Data.CypherList.Cyphers();
            //var conn = /*Environment.GetEnvironmentVariable("mongoConn");*/ Configuration["mongoConn"];
            var conn = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var IOServ = DataAccess.Abstractions.IOService.BuildService(conn, DataAccess.Abstractions.IOService.ServiceTypes.File);

            //foreach (var cy in cyphers)
            //{
            //    mongoService.StoreDocument<Models.Cypher>("cyphers", cy);
            //}

            Dictionary<string, string> filters = new Dictionary<string, string>
            {
                { "LevelDie", "6" }
            };



            var cyphers = IOServ.GetDocuments<Models.Cypher>("");
            var characters = IOServ.GetDocuments<Models.Character>("herrozerro2535");



            var db = new DataAccess.Repos.CypherContext();

            db.Database.Migrate();

            foreach (var cypher in cyphers)
            {
                db.Cyphers.Add(cypher);
            }
            db.SaveChanges();
            var chars = db.Characters
                .Include(x=>x.Inventory)
                .Include(x=>x.RecoveryRolls)
                .Include(x=>x.Cyphers).ToList();

            var cyList = db.Cyphers.ToList();

            //var character = await GetCharacter("Blobby");

            //db.Characters.Add(character);

            //await db.SaveChangesAsync();

            //characters.First().Name += "SaveTest";

            //IOServ.StoreDocuments("Players\\herrozerro2535", characters);

            //var loadedCyphers = IOServ.FilterDocuments<CypherBot.Models.Cypher>("cyphers", filters);


            await discord.ConnectAsync();

            await Task.Delay(-1);
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

            var cyls = (IEnumerable<Models.Cypher>)await Data.CypherList.GetRandomCyphersAsync(2);

            chr.Cyphers = cyls.Select(x => new Models.CharacterCypher() {
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
