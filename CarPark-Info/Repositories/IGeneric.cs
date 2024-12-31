using System.Linq.Expressions;
namespace CarPark_Info.Repositories

{
    // This file contains an interface for the repository pattern and defines all functions necessary for CRUD operations. 

    // We add another operation for filtering which we implement according to our use case in the required repositories. 
    public interface IGeneric<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> FilterAsync(Expression<Func<T, bool>>? filter);
        Task<T> GetAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();

    }
}