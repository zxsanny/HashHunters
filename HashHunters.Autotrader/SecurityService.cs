using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Entities;
using HashHunters.Extensions;
using HashHuntres.Autotrader.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;

namespace HashHunters.Autotrader.Services
{
    public class SecurityService : ISecurityService
    {
        IConfigurationRoot Configuration { get; }
        IHHCryptoProvider CryptoProvider { get; }

        public User CurrentUser { private get; set; }

        public SecurityService(IConfigurationRoot configuration, IHHCryptoProvider cryptoProvider)
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
                IssuerSigningKey = new SymmetricSecurityKey(SecurityKey.ToBytes())
            };
        }

        private SecureString SecurityKey => CryptoProvider.Decrypt(Configuration[ConfigurationKeys.SecurityKey]);

        public JwtSecurityToken GetToken(User user)
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
                (
                issuer: Configuration[ConfigurationKeys.Issuer],
                audience: Configuration[ConfigurationKeys.Audience],
                expires: DateTime.UtcNow.AddDays(2),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(SecurityKey.ToBytes()), SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
