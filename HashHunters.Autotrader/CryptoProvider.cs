using CryptSharp;
using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Extensions;
using System;
using System.Security;
using System.Text;

namespace HashHunters.Autotrader.Services
{
    public class CryptoProvider : IHHCryptoProvider
    {
        const string Delimeter = ":::";
        private byte[] Entropy => Encoding.Unicode.GetBytes("ksdfjbuhvkjsdgbjsglsdnfksdbhlksjdergfkljsdfgij hdflkjhbv ldfkgjhbl sjt");
        public Encoding Encoding => Encoding.UTF8;

        public string GetHash(SecureString password)
        {
            var salt = Crypter.Blowfish.GenerateSalt();
            var hash = Crypter.Blowfish.Crypt(password.ToBytes(), salt);
            return string.Concat(hash, Delimeter, salt);
        }

        public bool Validate(SecureString password, string passwordHash)
        {
            var strs = passwordHash.Split(new[] { Delimeter }, StringSplitOptions.None);
            var hash = strs[0];
            var salt = strs[1];

            return Crypter.Blowfish.Crypt(password.ToBytes(), salt) == hash;
        }
    }
}
