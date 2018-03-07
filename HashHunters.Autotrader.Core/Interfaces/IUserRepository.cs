using HashHunters.Autotrader.Entities;
using HashHuntres.Autotrader.Core.DTO;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(LoginDto loginDTO);
        Task Create(User user);
    }
}
