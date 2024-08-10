using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework.DbContexts
{
    public class SimpleTraderDbContext : DbContext
    {
        /*** DbSet is a collection of entities that can be queried from the database ***/

        // DbSet<User> Users will be used to query the Users table
        public DbSet<User> Users { get; set; }

        // DbSet<Account> Accounts will be used to query the Accounts table
        public DbSet<Account> Accounts { get; set; }

        // DbSet<AssetTransaction> AssetTransactions will be used to query the AssetTransactions table
        public DbSet<AssetTransaction> AssetTransactions { get; set; }


        public SimpleTraderDbContext(DbContextOptions options) : base(options)
        {
        }

        // 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Owned types (Stock) are special types that are tightly bound to their parent
            // entity (AssetTransaction) and do not have their own identity
            // (they do not have their own key).
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<AssetTransaction>().HasKey(a => a.Id);
            modelBuilder.Entity<Account>().HasKey(a => a.Id);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Connection string for the localdb 
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleTraderDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}