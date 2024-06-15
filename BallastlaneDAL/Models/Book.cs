using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BallastlaneDAL.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; }

        public Book()
        {
                
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }
    }
}
