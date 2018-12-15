using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;

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

            //characters.First().Name += "SaveTest";

            //IOServ.StoreDocuments("Players\\herrozerro2535", characters);

            //var loadedCyphers = IOServ.FilterDocuments<CypherBot.Models.Cypher>("cyphers", filters);


            await discord.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
