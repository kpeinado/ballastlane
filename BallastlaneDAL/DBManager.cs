using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BallastlaneDAL
{
    internal class DBManager
    {
        private readonly IConfiguration configuration;

        internal DBManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal IMongoDatabase GetDatabase()
        {
            string? connectionString = configuration.GetConnectionString("MongoDb");
            if (connectionString == null)
            {
                throw new NullReferenceException("The connection string is not defined");
            }

            var client = new MongoClient(connectionString);
            return client.GetDatabase("ballastlane");
        }
    }
}
 