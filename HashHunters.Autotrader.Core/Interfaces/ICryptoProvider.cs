using System.Security;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface IHHCryptoProvider
    {
        SecureString Decrypt(string s);
        string Encrypt(SecureString s);
    }
}
