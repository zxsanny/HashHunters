﻿using System.Security;

namespace HashHuntres.Autotrader.Core.DTO
{
    public class LoginDto
    {
        public string Name { get; set; }
        public SecureString Password { get; set; }
    }
}