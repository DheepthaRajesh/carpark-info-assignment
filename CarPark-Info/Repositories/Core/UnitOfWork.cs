using Microsoft.EntityFrameworkCore;
using CarPark_Info.Models;
using CarPark_Info.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using EFCore.BulkExtensions; 

namespace CarPark_Info.Repositories.Core
{
    // This file contains the implementation for the Unit of Work interface
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarParkContext _dbContext;
        private ICarParkRepository _carparkRepository;
        private IGeneric<User> _userRepository;
        private IGeneric<Favourite> _favouriteRepository;

        private IDbContextTransaction _transaction;

        public UnitOfWork(CarParkContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICarParkRepository CarParkRepository
        {
            get
            {

                if (this._carparkRepository == null)
                {
                    this._carparkRepository = new CarParkRepository(_dbContext);
                }
                return _carparkRepository;
            }
        }

        public IGeneric<User> UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_dbContext);
                }
                return _userRepository;
            }
        }

        public IGeneric<Favourite> FavouriteRepository
        {
            get
            {

                if (this._favouriteRepository == null)
                {
                    this._favouriteRepository = new GenericRepository<Favourite>(_dbContext);
                }
                return _favouriteRepository;
            }
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task BulkUpsertAsync(List<CarPark> carParkList)
        {
            // Use BulkInsertOrUpdateAsync from EFCore.BulkExtensions to handle insert/update
            await _dbContext.BulkInsertOrUpdateAsync(carParkList); 
        }



        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}