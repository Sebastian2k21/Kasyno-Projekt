using Kasyno;
using Kasyno.Entities;
using Kasyno.Helpers;
using NUnit.Framework;
using System.Linq;
using TestSupport.EfHelpers;

namespace KasynoTesty
{
    /// <summary>
    /// klasa testow kontekstu (bazy danych)
    /// </summary>
    public class ContextTests
    {
        private CasinoDbContext CreateTempDb()
        {
            var options = SqliteInMemory.CreateOptions<CasinoDbContext>(); 
            var context = new CasinoDbContext(options, test: true); 
            context.Database.EnsureCreated(); 
            return context;
        }

        [Test]
        public void EncryptorTest() 
        {
            var expectedHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
            var hash = Encryptor.Sha256("test");
            Assert.AreEqual(expectedHash, hash);
        }

        [Test]
        public void AddUserToDbTest() 
        {
            string login = "test";
            string password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";

            var context = CreateTempDb();
            var user = new AppUser
            {
                Login = login,
                Passsword = password
            };
            context.Add(user);
            context.SaveChanges();
            var user2 = context.Users.FirstOrDefault(x => x.Login == login && x.Passsword == password);
            Assert.NotNull(user2);
        }


        [Test]
        public void AddGameToDbTest() 
        {
            string name = "test3";

            var context = CreateTempDb();
            var game = new Game
            {
                Name = name
            };
            context.Add(game);
            context.SaveChanges();
            var game2 = context.Games.FirstOrDefault(x => x.Name == name);
            Assert.NotNull(game2);
        }

        [Test]
        public void AddUserDetailsToDbTest() 
        {
            string login = "test";
            string password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
            int balance = 1000;
            string firstName = "Jan";
            string surName = "Kowalski";

            var context = CreateTempDb();
            var user = new AppUser
            {
                Login = login,
                Passsword = password
            };
            var userDetails = new AppUserDetails
            {
                AppUser = user,
                Balance = balance,
                FirstName = firstName,
                Surname = surName,
            };
            context.Add(user);
            context.Add(userDetails);
            context.SaveChanges();
            var userDetails2 = context.UsersDetails.FirstOrDefault();

            Assert.AreEqual(firstName, userDetails2.FirstName);
            Assert.AreEqual(surName, userDetails2.Surname);
            Assert.AreEqual(balance, userDetails2.Balance);
            Assert.AreNotEqual(0, userDetails2.AppUserId);
        }
    }
}