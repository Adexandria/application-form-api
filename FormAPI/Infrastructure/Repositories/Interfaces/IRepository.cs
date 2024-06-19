using FormAPI.Models;

namespace FormAPI.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// A generic class to manage model container
    /// </summary>
    /// <typeparam name="T">Base class type</typeparam>
    public interface IRepository<T> where T : BaseClass
    {
        /// <summary>
        /// Adds new item to the container
        /// </summary>
        /// <param name="item">Manages model</param>
        /// <returns>A boolean value to denote if it was successfully</returns>
        Task<bool> AddItem(T item);
    }
}
