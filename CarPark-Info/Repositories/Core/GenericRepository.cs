using Microsoft.EntityFrameworkCore;
using CarPark_Info.Models;
using System.Linq.Expressions;

namespace CarPark_Info.Repositories.Core
{
    // This file contains the implementation of the Generic Repository interface for all 3 entities 
    // and defines basic CRUD operations
    public class GenericRepository<T> : IGeneric<T> where T : class
    {
        private readonly CarParkContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(CarParkContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // Get all items
        }

        public async Task<List<T>> FilterAsync(Expression<Func<T, bool>>? filter)
        {
            // If filter is not null, apply filter. The specific filter will be applied in the respective entity's repository
            if (filter != null){
                return await _dbSet.Where(filter).ToListAsync(); 
            }

            return await _dbSet.ToListAsync(); // If no filter is provided, return all items
        }

        public async Task<T> GetAsync(string id)
        {
            var item = await _dbSet.FindAsync(id); // Get the item by its primary key 
            if (item == null)
            {
                throw new KeyNotFoundException($"Record with id {id} not found.");
            }
            return item;
        }


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); // Add entity to database and save changes
            await SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity); // Update entity in database and save changes
            await SaveAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            // If entity is not null, remove entity from database and save changes
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); // Save all changes to database
        }
    }
}