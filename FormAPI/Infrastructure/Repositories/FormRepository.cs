using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Models.Forms;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace FormAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Handles form container
    /// </summary>
    /// <param name="database">Manages operations in the database</param>
    public class FormRepository(Database database) : IFormRepository
    {

        /// <summary>
        /// Adds new form to the container
        /// </summary>
        /// <param name="item">Manages form model</param>
        /// <returns>A boolean value to denote if it was successfully</returns>
        public async Task<bool> AddItem(Form item)
        {
            var response = await _container.CreateItemAsync(item);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Fetches a single form using the id
        /// </summary>
        /// <param name="id">form id</param>
        /// <returns>A form</returns>
        public async Task<Form> GetForm(string id)
        {
            var response = await _container.ReadItemAsync<Form>(id, new PartitionKey(id));

            return response.Resource;
        }

        /// <summary>
        /// Verifies if form exists
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>boolean value</returns>
        public async Task<bool> IsExist(string id)
        {
            var response = await _container.ReadItemAsync<Form>(id, new PartitionKey(id));

            return response.Resource != null;
        }

        /// <summary>
        /// Database container
        /// </summary>
        private readonly Container _container = database.CreateContainerIfNotExistsAsync("forms","/id").Result;
    }
}
