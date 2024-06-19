using FormAPI.Models.Forms;

namespace FormAPI.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Handles form container
    /// </summary>
    public interface IFormRepository : IRepository<Form>
    {
        /// <summary>
        /// Fetches a single form using the id
        /// </summary>
        /// <param name="id">form id</param>
        /// <returns>A form</returns>
        Task<Form> GetForm(string id);

        /// <summary>
        /// Verifies if form exists
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>boolean value</returns>
        Task<bool> IsExist(string id);
    }
}
