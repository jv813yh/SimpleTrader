using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework.DbContexts
{
    public class SimpleTraderDbContextFactory
    {
        // Action delegate that takes DbContextOptionsBuilder as parameter
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        public SimpleTraderDbContextFactory(Action<DbContextOptionsBuilder> optionsAction)
        {
            _optionsAction = optionsAction;
        }

        /// <summary>
        /// Create a new instance of SimpleTraderDbContext
        /// </summary>
        /// <param name="args"></param>
        /// <returns> SimpleTraderDbContext with connection to the local database SimpleTraderDb </returns>
        public SimpleTraderDbContext CreateDbContext()
        {
            // Create a new instance of DbContextOptionsBuilder<SimpleTraderDbContext>
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
            // Call the delegate with the options
            _optionsAction(options);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}
