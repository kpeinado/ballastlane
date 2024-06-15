using BallastlaneDAL.Interfaces;
using BallastlaneDAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BallastlaneDAL
{
    public class UserDAL : ICrud<User>
    {
        public IConfiguration _configuration { get; set; }

        public UserDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        private IMongoCollection<User> getUserCollection()
        {
            var db = new DBManager(_configuration).GetDatabase();
            return db.GetCollection<User>("user");
        }
        public bool Create(User user)
        {
            var userCollection = getUserCollection();
            userCollection.InsertOne(user);
            return true;
        }

        public IEnumerable<User> Read(Expression<Func<User, bool>> predicate)
        {
            var userCollection = getUserCollection();
            return userCollection.AsQueryable().Where(predicate).ToList();
        }

        public bool Update(User user)
        {
            var userCollection = getUserCollection();
            var filterDefinition = Builders<User>.Filter.Eq(u => u.UserName, user.UserName);
            var updateDefinition = Builders<User>.Update
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.PasswordHash, user.PasswordHash);

            userCollection.UpdateOne(filterDefinition, updateDefinition);
            return true;
        }

        public bool Delete(User user)
        {
            var userCollection = getUserCollection();
            var filterDefinition = Builders<User>.Filter.Eq(u => u.UserName, user.UserName);
            userCollection.DeleteOne(filterDefinition);
            return true;
        }
    }
}
