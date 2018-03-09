using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Entities;
using HashHuntres.Autotrader.Core.DTO;
using MongoDB.Driver;
using System;
using System.Security;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly IMongoCollection<User> Users;
        readonly IHHCryptoProvider CryptoProvider;

        public UserRepository(IMongoDatabase mongoDatabase, IHHCryptoProvider cryptoProvider)
        {
            Users = mongoDatabase.Get<User>();
            CryptoProvider = cryptoProvider;
        }

        public async Task<User> Login(LoginDto loginDTO)
        {
            var user = await Users.Find(x => x.Name == loginDTO.Name).SingleAsync();
            if (user == null)
            {
                throw new ArgumentException($"User {loginDTO.Name} is not exists!");
            }

            if (!CryptoProvider.Validate(loginDTO.Password, user.PasswordHash))
            {
                throw new ArgumentException($"Password is incorrect!");
            }

            return user;
        }

        public Task<bool> CreateUserAsync(User user, SecureString password)
        {
            throw new NotImplementedException();
        }
    }
}
