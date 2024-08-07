using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework.DbContexts
{
    public class DesignTimeSimpleTraderDbContextFactory : IDesignTimeDbContextFactory<SimpleTraderDbContext>
    {
        // Connection string to the local database SimpleTraderDb
        private const string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleTraderDb;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        /// <summary>
        /// Create a new instance of SimpleTraderDbContext
        /// </summary>
        /// <param name="args"></param>
        /// <returns> SimpleTraderDbContext with connection to the local database SimpleTraderDb </returns>
        public SimpleTraderDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            return new SimpleTraderDbContext(options);
        }
    }
}
