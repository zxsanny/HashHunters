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
        Lazy<IConfigurationRoot> LazyConfiguration;
        IConfigurationRoot Configuration
            => LazyConfiguration.Value;
        
        public User CurrentUser { private get; set; }

        public SecurityService(Lazy<IConfigurationRoot> configuration)
        {
            LazyConfiguration = configuration;
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

        //TODO: Do it in more secure way
        private SecureString SecurityKey =>
            Configuration[ConfigurationKeys.SecurityKey].ToSecureString();

        public JwtSecurityToken GetToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            int.TryParse(Configuration[ConfigurationKeys.TokenExpirationDays], out int expirationDays);
            expirationDays = Math.Max(Math.Min(expirationDays, 10), 2);

            var token = new JwtSecurityToken
                (
                issuer: Configuration[ConfigurationKeys.Issuer],
                audience: Configuration[ConfigurationKeys.Audience],
                expires: DateTime.UtcNow.AddDays(expirationDays),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(SecurityKey.ToBytes()), SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
