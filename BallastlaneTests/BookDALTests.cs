using BallastlaneDAL;
using BallastlaneDAL.Models;
using Microsoft.Extensions.Configuration;

namespace BallastlaneTests
{
    public class BookDALTests
    {
        private BookDAL bookDal;

        [SetUp]
        public void Setup()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://root:ballastlane2024@localhost:27017" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            bookDal = new BookDAL(configuration);
        }

        [Test]
        public void CreateBookTest()
        {
            Book book = new Book()
            {
                Name = "Test",
                Author = "kpeinado",
                Year = 2023
            };
            bool created = bookDal.Create(book);
            Assert.IsTrue(created);
        }
    }
}