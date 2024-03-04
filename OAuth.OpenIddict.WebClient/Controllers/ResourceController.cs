using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Client.AspNetCore;
using System.Net.Http.Headers;
using System.Net.Http;

namespace OAuth.OpenIddict.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ResourceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Authorize, HttpGet()]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            // For scenarios where the default authentication handler configured in the ASP.NET Core
            // authentication options shouldn't be used, a specific scheme can be specified here.
            var token = await HttpContext.GetTokenAsync(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken);
            token = await HttpContext.GetTokenAsync("access_token");
            using var client = _httpClientFactory.CreateClient();

            using var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7002/resources");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return Ok(await response.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}
