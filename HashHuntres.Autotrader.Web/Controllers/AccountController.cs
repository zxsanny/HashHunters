using HashHunters.Autotrader.Core.Interfaces;
using HashHuntres.Autotrader.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await UserRepository.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }



    }
}
