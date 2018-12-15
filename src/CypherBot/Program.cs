﻿using System;
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
#if DEBUG
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("secrets.json");
#else
            .AddJsonFile("appsettings.json", false, true);
#endif

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

            if (Configuration["appInitialize"].ToLower() == "true")
            {
                Console.WriteLine("Initializing the database.");

                await Utilities.DatabaseHelper.InitializeDatabaseAsync();

                Console.WriteLine("Database Initialized, please set the appInitialize flag in appsettings.json to false.");
                Console.WriteLine("Press any key to close.");

                Console.ReadKey();

                return;
            }

            await discord.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
