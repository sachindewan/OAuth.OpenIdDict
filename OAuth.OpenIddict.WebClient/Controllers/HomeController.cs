using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuth.OpenIddict.WebClient.Models;
using OpenIddict.Client.AspNetCore;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;

namespace OAuth.OpenIddict.WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var token = HttpContext.GetTokenAsync("id_token").GetAwaiter().GetResult();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("~/login")]
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                // Only allow local return URLs to prevent open redirect attacks.
                RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
            };

            // Ask the OpenIddict client middleware to redirect the user agent to the identity provider.
            return Challenge(properties, "oidc");
        }
    }
}
