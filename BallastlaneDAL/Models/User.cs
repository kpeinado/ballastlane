using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BallastlaneDAL.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public User()
        {
            
        }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }
    }
}
