using BallastlaneDAL.Interfaces;
using BallastlaneDAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BallastlaneDAL
{
    public class BookDAL : ICrud<Book>
    {
        public IConfiguration _configuration { get; set; }

        public BookDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        private IMongoCollection<Book> getBookCollection()
        {
            var db = new DBManager(_configuration).GetDatabase();
            return db.GetCollection<Book>("book");
        }
        public bool Create(Book Book)
        {
            var BookCollection = getBookCollection();
            BookCollection.InsertOne(Book);
            return true;
        }

        public IEnumerable<Book> Read(Expression<Func<Book, bool>> predicate)
        {
            var BookCollection = getBookCollection();
            return BookCollection.AsQueryable().Where(predicate).ToList();
        }

        public bool Update(Book Book)
        {
            var BookCollection = getBookCollection();
            var filterDefinition = Builders<Book>.Filter.Eq(u => u.BookId, Book.BookId);
            var updateDefinition = Builders<Book>.Update
                .Set(u => u.Name, Book.Name)
                .Set(u => u.Author, Book.Author)
                .Set(u => u.Year, Book.Year);

            BookCollection.UpdateOne(filterDefinition, updateDefinition);
            return true;
        }

        public bool Delete(Book Book)
        {
            var BookCollection = getBookCollection();
            var filterDefinition = Builders<Book>.Filter.Eq(u => u.BookId, Book.BookId);
            BookCollection.DeleteOne(filterDefinition);
            return true;
        }
    }
}
