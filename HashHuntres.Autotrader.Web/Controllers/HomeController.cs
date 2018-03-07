using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using HashHunters.Autotrader.Core.Interfaces;
using HashHuntres.Autotrader.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HashHuntres.Autotrader.Web.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository UserRepository { get; }
        ISecurityService SecurityService { get; }

        public HomeController(IUserRepository userRepository, ISecurityService securityService)
        {
            UserRepository = userRepository;
            SecurityService = securityService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody]LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = UserRepository.Login(loginDto);
            var token = new JwtSecurityTokenHandler().WriteToken(SecurityService.GetToken(user));
            return Ok(token);
        }
    }
}
