using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace SimpleTrader.EntityFramework.DbContexts
{
    public class SimpleTraderDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SimpleTraderDbContext>
    {
        public SimpleTraderDbContext CreateDbContext(string[] args)
        {
            // Get configuration from the appsettings.json file
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the current directory
                .AddJsonFile("appsettings.json"); // Add the appsettings.json file

            // Get the connection string from the configuration
            string? connectionString = configurationBuilder.Build().GetConnectionString("SQLiteDefaultConnection"); 
            if(configurationBuilder == null)
            {
                throw new Exception("Could not find the connection string 'SQLiteDefaultConnection'");
            }

            // Create a new instance of DbContextOptionsBuilder with the connection string
            var optionsBuilder = new DbContextOptionsBuilder<SimpleTraderDbContext>()
                .UseSqlite(connectionString); // Use the SQLite connection string

            // Return a new instance of SimpleTraderDbContext with the options
            return new SimpleTraderDbContext(optionsBuilder.Options);

        }
    }
}
