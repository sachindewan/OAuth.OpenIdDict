using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Client;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);
var claimsRequest = new List<string> { "role", "email" };
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "OauthScheme";

}).AddCookie()
.AddOAuth("OauthScheme", options =>
{
    options.AuthorizationEndpoint = "https://localhost:7000/connect/authorize";
    options.ClientId = "web-client";
    options.ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654";
    options.SaveTokens = true;
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.TokenEndpoint = "https://localhost:7000/connect/token";
    options.UserInformationEndpoint = "test";
    options.CallbackPath = "/swagger/oauth2-redirect.html";
    // options.Scope.Add("api1");
    options.Scope.Add("api1");
    //options.ClaimActions.MapJsonKey("email","email");
    //options.ClaimActions.MapJsonKey("name", "name");
    //options.ClaimActions.MapJsonKey("scope", "scp:api1");
    //options.ClaimActions.MapJsonKey("scope", "api1");
    //options
});
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
