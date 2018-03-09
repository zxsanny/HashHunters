using HashHunters.Autotrader.Core.Interfaces;
using HashHunters.Autotrader.Entities;
using HashHuntres.Autotrader.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace HashHuntres.Autotrader.Web.Controllers
{
    public class AccountController : Controller
    {
        ISecurityService SecurityService { get; }
        IUserRepository UserRepository { get; }
        IConfigurationRoot Configuration { get; }

        public AccountController(ISecurityService securityService, IUserRepository userRepository, IConfigurationRoot configuration)
        {
            SecurityService = securityService;
            UserRepository = userRepository;
            Configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<object> Login([FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await UserRepository.Login(loginDto);
            var token = SecurityService.GetToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email
            };
            var result = await UserRepository.CreateUserAsync(user, registerDto.Password);

            if (!result)
            {
                throw new ApplicationException("UNKNOWN_ERROR");
            }
            return SecurityService.GetToken(user);            
        }
    }
}
