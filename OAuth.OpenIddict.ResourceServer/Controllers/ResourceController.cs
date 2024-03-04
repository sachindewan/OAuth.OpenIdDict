using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAuth.OpenIddict.ResourceServer.Controllers
{
    [Route("resources")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetSecretResources()
        {
            var user = HttpContext.User?.Identity?.Name;
            return Ok($"user: {user}");
        }

        [HttpGet("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("OpenIddict.Server.AspNetCore");
        }
    }
}
