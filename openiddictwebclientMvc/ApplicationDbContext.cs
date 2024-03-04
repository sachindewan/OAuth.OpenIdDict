using Microsoft.EntityFrameworkCore;

namespace openiddictwebclient
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
