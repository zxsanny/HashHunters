using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Entities;
using HashHunters.MinerMonitor.Common.Extensions;
using HashHuntres.Autotrader.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HashHunters.Autotrader.Services
{
    public class SecurityService : ISecurityService
    {
        IConfiguration Configuration;
        IHHCryptoProvider CryptoProvider;

        public User CurrentUser { private get; set; }

        public SecurityService(IConfiguration configuration, IHHCryptoProvider cryptoProvider)
        {
            Configuration = configuration;
            CryptoProvider = cryptoProvider;
        }

        public ExchangeKey GetKey(ExchangeEnum exchange)
        {
            if (!CurrentUser.Exchanges.ContainsKey(exchange))
            {
                return null;
            }
            return CurrentUser.Exchanges[exchange];
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            var cryptedKey = Configuration[ConfigurationKeys.SecurityKey];

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration[ConfigurationKeys.Issuer],
                ValidAudience = Configuration[ConfigurationKeys.Audience],
                IssuerSigningKey = new SymmetricSecurityKey( CryptoProvider.Decrypt(Configuration[ConfigurationKeys.SecurityKey]).ToBytes(Encoding.UTF8))
            };
        }


        public JwtSecurityToken GetToken()
        {
            throw new System.NotImplementedException();
        }
    }
}
