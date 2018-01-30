using HashHunters.Autotrader.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace HashHunters.Autotrader
{
    public interface ISecurityService
    {
        ExchangeKey GetKey(ExchangeEnum exchange);

        TokenValidationParameters GetTokenValidationParameters();
        JwtSecurityToken GetToken();
    }
}