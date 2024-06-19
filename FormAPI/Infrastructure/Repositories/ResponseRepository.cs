using FormAPI.Infrastructure.Repositories.Interfaces;
using FormAPI.Models.Answers;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace FormAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Handles responses container
    /// </summary>
    /// <param name="database">Manages database operations</param>
    public class ResponseRepository(Database database) : IResponseRepository
    {
        /// <summary>
        /// Adds new response to the container
        /// </summary>
        /// <param name="response">Manages response model</param>
        /// <returns>A boolean value to denote if it was successfully</returns>
        public async Task<bool> AddItem(FormResponse response)
        {
            var result = await _container.CreateItemAsync(response, new PartitionKey(response.FormId));

            if (result.StatusCode != HttpStatusCode.Created)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Manages container in database
        /// </summary>
        private readonly Container _container = database.CreateContainerIfNotExistsAsync("responses", "/FormId").Result;
    }
}
