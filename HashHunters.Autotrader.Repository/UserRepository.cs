using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Entities;
using HashHuntres.Autotrader.Core.DTO;
using MongoDB.Driver;
using System;

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

        public User Login(LoginDTO loginDTO)
        {
            var user = Users.Find(x => x.Name == loginDTO.Name).FirstOrDefault();
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
    }
}
