using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.MinerMonitor.Common.Extensions;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace HashHunters.Autotrader.Services
{
    public class DPAPICryptoProvider : IHHCryptoProvider
    {
        private byte[] Entropy => Encoding.Unicode.GetBytes("ksdfjbuhvkjsdgbjsglsdnfksdbhlksjdergfkljsdfgij hdflkjhbv ldfkgjhbl sjt");
        public Encoding Encoding => Encoding.UTF8;

        public SecureString Decrypt(string s) =>
            ProtectedData.Unprotect(Convert.FromBase64String(s), Entropy, DataProtectionScope.CurrentUser).ToSecureString(Encoding);
        
        public string Encrypt(SecureString sensitiveData) =>
            Convert.ToBase64String(ProtectedData.Protect(sensitiveData.ToBytes(Encoding), Entropy, DataProtectionScope.CurrentUser));
    }
}
