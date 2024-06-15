using BallastlaneBLL;
using BallastlaneDAL;
using BallastlaneDAL.Models;
using Microsoft.Extensions.Configuration;

namespace BallastlaneTests
{
    public class UserDALTests
    {
        private UserDAL UserDal;
        private string UserName;

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

            UserDal = new UserDAL(configuration);

            this.UserName = "kpeinado" + new Random().Next(1, 1000);
        }

        [Test]
        public void CRUDUserTest()
        {
            //Create
            User User = new User()
            {
                UserName = UserName,
                FirstName = "Kevin",
                LastName = "Peinado",
                PasswordHash = Crypto.getHash("password")
            };
            bool created = UserDal.Create(User);
            Assert.IsTrue(created);

            // Read
            var user = UserDal.Read(u => u.UserName == UserName).FirstOrDefault();
            Assert.IsNotNull(user);

            //Update
            user.FirstName = "Kevin Edited";
            var updated = UserDal.Update(user);
            Assert.IsTrue(updated);

            //Delete
            var deleted = UserDal.Delete(user);
            Assert.IsTrue(deleted);

            //No users
            var userNull = UserDal.Read(u => u.UserName == UserName).FirstOrDefault();
            Assert.IsNull(userNull);
        }
    }
}