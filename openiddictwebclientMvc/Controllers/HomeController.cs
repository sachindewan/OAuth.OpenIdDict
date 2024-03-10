using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Client.AspNetCore;
using openiddictwebclient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace openiddictwebclient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        [HttpGet("~/")]
        public ActionResult Index() => View();

        [Authorize, HttpPost("~/")]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            //[TODO] this is meaning full here, because we have configured openid connect handler 
            // For scenarios where the default authentication handler configured in the ASP.NET Core
            // authentication options shouldn't be used, a specific scheme can be specified here.
            var token = await HttpContext.GetTokenAsync(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken);
            var token1 = await HttpContext.GetTokenAsync("access_token");
            var token2 = await HttpContext.GetTokenAsync("id_token");
            using var client = _httpClientFactory.CreateClient();

            using var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7002/resources");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return View(model: await response.Content.ReadAsStringAsync(cancellationToken));
        }

        [HttpGet("swagger")]
        public IActionResult GetCallBack()
        {
            var user = HttpContext.User;
            return Ok(user);
        }
    }
}
