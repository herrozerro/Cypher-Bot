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

            interactivity = discord.UseInteractivity(new InteractivityConfiguration() { });

            //Initialize the database and migrate on start.
            var db = new DataAccess.Repos.CypherContext();
            db.Database.Migrate();

            //await InitializeDatabaseAsync();

            await discord.ConnectAsync();

            await Task.Delay(-1);
        }


        /// <summary>
        /// Loads reference data from datafiles
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeDatabaseAsync()
        {
            var cypherStrings = await Data.FileIO.GetFileString("cyphers");

            var cyphers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Cypher>>(cypherStrings);

            using (var db = new DataAccess.Repos.CypherContext())
            {
                db.AddRange(cyphers);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
