using HashHunters.Autotrader.Entities;
using HashHuntres.Autotrader.Core.DTO;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface IUserRepository
    {
        User Login(LoginDTO loginDTO);
    }
}
