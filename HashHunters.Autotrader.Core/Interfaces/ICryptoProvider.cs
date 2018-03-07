using System.Security;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface IHHCryptoProvider
    {
        string GetHash(SecureString password);
        bool Validate(SecureString password, string passwordHash);
    }
}
