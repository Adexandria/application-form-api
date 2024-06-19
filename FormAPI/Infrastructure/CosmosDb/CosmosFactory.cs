using Microsoft.Azure.Cosmos;

namespace FormAPI.Infrastructure.CosmosDb
{
    /// <summary>
    /// Create database
    /// </summary>
    /// <remarks>
    /// A constructor
    /// </remarks>
    /// <param name="configuration">Handles database secrets</param>
    public class CosmosFactory(IConfiguration configuration) : ICosmosFactory
    {

        /// <summary>
        /// Intalises database using database secrets
        /// </summary>
        /// <returns>Database created</returns>
        public Database IntialiseDatabase()
        {
          // Creates the client using credentials
          var cosmosClient = new CosmosClient(_endpoint, _tokenCredential);

            //Creates the database if it doesn't exists
          var databaseResponse = cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId).Result;

          return databaseResponse.Database;
        }

        /// <summary>
        /// Cosmos db enpoint
        /// </summary>
        private readonly string _endpoint = configuration["Cosmos:Endpoint"] ?? throw new NullReferenceException(nameof(_endpoint));

        /// <summary>
        /// Cosmos db token secret
        /// </summary>
        private readonly string _tokenCredential = configuration["Cosmos:TokenCredential"] ?? throw new NullReferenceException(nameof(_tokenCredential));

        /// <summary>
        /// Database name
        /// </summary>
        private readonly string _databaseId = configuration["Cosmos:Database"] ?? throw new NullReferenceException(nameof(_databaseId));
    }
}
