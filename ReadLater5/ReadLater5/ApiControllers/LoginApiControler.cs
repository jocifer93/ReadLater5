using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private ITokenService _tokenService;

        public LoginApiController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromQuery] string username, [FromQuery] string password)
        {
            var identityUser = await _userManager.FindByNameAsync(username);
            if (identityUser == null) { return NotFound(); }

            bool exists = await _userManager.CheckPasswordAsync(identityUser, password);
            if (!exists) { return NotFound(); }

            string token = _tokenService.GetToken(identityUser);

            return Ok(token);
        }
    }
}
