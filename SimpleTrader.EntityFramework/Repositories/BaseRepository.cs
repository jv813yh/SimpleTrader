using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.Domain.Interfaces;
using SimpleTrader.Domain.Models;
using SimpleTrader.EntityFramework.DbContexts;

namespace SimpleTrader.EntityFramework.Repositories
{
    /* 
     * Base repository class that implements the IBaseRepository interface
     * Contains CRUD operations for the entity and some additional methods
     */
    public class BaseRepository<T> : IBaseRepository<T> where T : DomainObject
    {
        private readonly DesignTimeSimpleTraderDbContextFactory _contextFactory;

        public BaseRepository(DesignTimeSimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// CreateAsync method that adds an entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                using(var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Add the entity to the database
                        EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                        // Save the changes
                        await context.SaveChangesAsync();
                        // Commit the transaction
                        await transaction.CommitAsync();

                        // return the entity
                        return createdResult.Entity;
                    }
                    catch(DbUpdateException ex)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception
                        throw new Exception("An error occurred while updating the database.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception
                        throw new Exception("An error occurred while creating an entity.");
                    }
                }
            }
        }

        /// <summary>
        /// Async method that deletes an entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if was successfully</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                using(var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Find the entity by id
                        T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

                        if(entity != null)
                        {
                            // Remove the entity from the database
                            context.Set<T>().Remove(entity);
                            await context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            return true;
                        }

                        return false;
                    }
                    catch (DbUpdateException)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception ...
                        throw new Exception("An error occurred while updating the database.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception- ...
                        throw new Exception("An error occurred while creating an entity.");
                    }
                }
            }
        }

        /// <summary>
        /// Async method that gets all entities from the database without any conditions
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    IEnumerable<T> entities = await context.Set<T>().ToListAsync();

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
        public async Task<T?> GetByIdAsync(int id)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

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

        public async Task<T?> UpdateAsync(int id, T entity)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                using(var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        T? findEntity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

                        if (findEntity != null)
                        {
                            // Detach the entity from the context
                            context.Entry(findEntity).State = EntityState.Detached;

                            // Set the id of the entity to the id of the entity that we want to update

                            if(id != entity.Id)
                            {
                                entity.Id = id;
                                EntityEntry<T> updatedResult = context.Set<T>().Update(entity);
                                await context.SaveChangesAsync();
                                await transaction.CommitAsync();

                                return updatedResult.Entity;
                            }
                        }

                        return null;
                    }
                    catch (DbUpdateException)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception ...
                        throw new Exception("An error occurred while updating the database.");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        // maybe log the exception- ...
                        throw new Exception("An error occurred while creating an entity.");
                    }
                }
            }
        }
    }
}
