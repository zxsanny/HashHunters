﻿using HashHunters.Autotrader.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace HashHunters.Autotrader.Core.Interfaces
{
    public interface ISecurityService
    {
        ExchangeKey GetKey(ExchangeEnum exchange);

        TokenValidationParameters GetTokenValidationParameters();
        JwtSecurityToken GetToken(User user);
    }
}