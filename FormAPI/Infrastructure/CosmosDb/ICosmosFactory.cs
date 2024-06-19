using Microsoft.Azure.Cosmos;

namespace FormAPI.Infrastructure.CosmosDb
{
    /// <summary>
    /// Manages cosmos db client
    /// </summary>
    public interface ICosmosFactory
    {
        /// <summary>
        /// Intalises database using database secrets
        /// </summary>
        /// <returns>Database created</returns>
        Database IntialiseDatabase();
    }
}
