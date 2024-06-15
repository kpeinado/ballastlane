using BallastlaneDAL;
using BallastlaneDAL.Interfaces;
using BallastlaneDAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BallastlaneBLL
{
    public class UserBLL
    {
        private readonly ICrud<User> userDal;

        public UserBLL(IConfiguration configuration)
        {
            this.userDal = new UserDAL(configuration);
        }

        public bool InitUsers()
        {
            var firstUser = userDal.Read(u => u.UserName == "kpeinado").FirstOrDefault();
            if (firstUser == null)
            {
                firstUser = new User()
                        {
                            UserName = "kpeinado",
                    FirstName = "Kevin",
                    LastName = "Peinado",
                    PasswordHash = Crypto.getHash("Ballastlane2024")
                };

                userDal.Create(firstUser);
            }
            return true;
        }

        public string Login(string userName, string password)
        {
            var user = userDal.Read(u => u.UserName == userName).SingleOrDefault();
            if (user == null)
                return null;
            else if (user.PasswordHash != Crypto.getHash(password))
                return null;
            else 
                return Crypto.Encrypt(DateTime.Now.AddHours(1).ToString());
        }

        public void Create(string userName, string firstName, string lastName, string password)
        {
            var user = new User()
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = Crypto.getHash(password)
            };

            userDal.Create(user);
        }

        public IEnumerable<User> Read()
        {
            return userDal.Read(u => true).Select(u => new User()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            });
        }

        public void Update(string userName, string firstName, string lastName, string password)
        {
            var user = new User()
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = Crypto.getHash(password)
            };

            userDal.Update(user);
        }

        public void Delete(string userName)
        {
            var user = new User()
            {
                UserName = userName
            };

            userDal.Delete(user);
        }
    }
}
