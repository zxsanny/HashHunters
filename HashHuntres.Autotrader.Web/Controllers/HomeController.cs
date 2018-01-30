using System.Diagnostics;
using HashHunters.Autotrader.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HashHuntres.Autotrader.Web.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository UserRepository { get; set; }

        public HomeController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
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
        public IActionResult Post([FromBody]LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = UserRepository.Login(loginDTO);
            
        }
    }
}
