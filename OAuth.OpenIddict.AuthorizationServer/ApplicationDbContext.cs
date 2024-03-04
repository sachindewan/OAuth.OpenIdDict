using Microsoft.EntityFrameworkCore;

namespace OAuth.OpenIddict.AuthorizationServer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
