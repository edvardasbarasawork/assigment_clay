using Microsoft.Extensions.Configuration;
using SimpleMigrations;
using SimpleMigrations.Console;
using SimpleMigrations.DatabaseProvider;
using System;
using System.Data.SqlClient;

namespace ClayTest.Migrations
{
    class Program
    {
        public static string EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Program).Assembly;

            using (var db = new SqlConnection(connectionString))
            {
                var databaseProvider = new MssqlDatabaseProvider(db);
                var migrator = new SimpleMigrator(migrationsAssembly, databaseProvider);
                migrator.Load();

                if (args.Length > 1 && long.TryParse(args[0], out long version))
                    migrator.MigrateTo(version);
                else
                    migrator.MigrateToLatest();

                var consoleRunner = new ConsoleRunner(migrator);
                consoleRunner.Run(args);
            }
        }
    }
}
