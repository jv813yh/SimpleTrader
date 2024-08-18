using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Common.Interfaces;
using SimpleTrader.EntityFramework.DbContexts;
using SimpleTrader.Domain.Models;
using SimpleTrader.EntityFramework.Repositories.Common;

namespace SimpleTrader.EntityFramework.Repositories
{
    public class AccountRepository : ICommonRepository<Account>
    {
        // DesignTimeSimpleTraderDbContextFactory instance
        private readonly DesignTimeSimpleTraderDbContextFactory _contextFactory;

        // SharedRepository instance
        private readonly SharedRepository<Account> _sharedRepository;

        public AccountRepository(DesignTimeSimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _sharedRepository = new SharedRepository<Account>(contextFactory);
        }

        /// <summary>
        /// CreateAsync method that adds an entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Account> CreateAsync(Account entity)
            => await _sharedRepository.CreateAsync(entity);

        /// <summary>
        /// Async method that deletes an entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if was successfully</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteAsync(int id)
            => await _sharedRepository.DeleteAsync(id);

        /// <summary>
        /// Async method that gets all entities from the database without any conditions
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    IEnumerable<Account> entities = await context.Accounts
                        .Include(a => a.AssetTransactions)
                        .ToListAsync();

                    return entities;
                }
                catch (DbUpdateException)
                {
                    // maybe log the exception ...
                    throw new Exception("An error occurred while updating the database.");
                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while creating an entity.");
                }
            }
        }

        /// <summary>
        /// Async method that gets an entity by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entity when it exists, otherwise null</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Account?> GetByIdAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    Account? entity = await context.Accounts
                        .Include(a => a.AssetTransactions)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (entity != null)
                    {
                        return entity;
                    }

                    return null;

                }
                catch (DbUpdateException)
                {
                    // maybe log the exception ...
                    throw new Exception("An error occurred while updating the database.");
                }
                catch (Exception)
                {
                    // maybe log the exception- ...
                    throw new Exception("An error occurred while creating an entity.");
                }
            }
        }

        public async Task<Account?> UpdateAsync(int id, Account entity)
         => await _sharedRepository.UpdateAsync(id, entity);
    }
}
