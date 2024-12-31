using Microsoft.EntityFrameworkCore;
using CarPark_Info.Models;

namespace CarPark_Info.Repositories
{

// This file contains the interface for the Unit of Work. We use this to manage multiple repositories and to handle changes
// as a single transaction. To ensure data integrity, the transaction is committed or rolled back in case of error

// We also define our transaction methods & bulk upsert methods that we will be using to process the csv file in the 
// unit of work interface to avoid coupling the service with DbContext and to use the abstraction of UnitOfWork
public interface IUnitOfWork : IDisposable
    {
        ICarParkRepository CarParkRepository {get;}
        IGeneric<User> UserRepository {get;}
        IGeneric<Favourite> FavouriteRepository {get;}

        Task SaveAsync(); // To save changes

        Task BeginTransactionAsync(); // To start a transaction

        Task CommitTransactionAsync(); // To commit the transaction

        Task RollBackTransactionAsync(); // To roll back incase of failure

        Task BulkUpsertAsync(List<CarPark> carParkList); // To bulk update or insert
    }
}