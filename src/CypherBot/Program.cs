using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using CypherBot.Core.DataAccess.Repos;
using DSharpPlus.Interactivity.Extensions;

namespace CypherBot
{
    class Program : BaseCommandModule
    {
        static DiscordClient discord;
        static IConfiguration Configuration { get; set; }
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("secrets.json", true, true);

            Configuration = builder.Build();

            // Use this if you want App_Data off your project root folder
            string baseDir = Directory.GetCurrentDirectory();

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(baseDir, "DataFiles"));

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Environment.GetEnvironmentVariable("DiscordAPIKey") ?? Configuration["token"],
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { Configuration["commandPrefix"] },
                CaseSensitive = false                
            });

            commands.RegisterCommands<Commands.CypherCommands>();
            //commands.RegisterCommands<Commands.CypherCommands.RollCommands>();

            var interactivity = discord.UseInteractivity(new InteractivityConfiguration() { });

            //Initialize the database and migrate on start.
            var db = new CypherContext();
            db.Database.Migrate();

            if (Configuration["appInitialize"].ToLower() == "true")
            {
                Console.WriteLine("Initializing the database.");

                await Utilities.DatabaseHelper.InitializeDatabaseAsync();

                Console.WriteLine("Database Initialized, please set the appInitialize flag in appsettings.json to false in order to stop the database from being overridden again.");

                System.Threading.Thread.Sleep(3000);
            }

            await discord.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
