using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework.DbContexts
{
    public class DesignTimeSimpleTraderDbContextFactory
    {
        // Connection string to the local database SimpleTraderDb
        private readonly string _connectionString = string.Empty;

        public DesignTimeSimpleTraderDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

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
