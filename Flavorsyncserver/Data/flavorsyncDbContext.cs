using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flavorsyncserver.Model;

namespace Flavorsyncserver.Data
{
    public class flavorsyncDbContext : IdentityDbContext
    {
        public flavorsyncDbContext(DbContextOptions<flavorsyncDbContext> options) : base(options) {


        }

        public DbSet<User> AppUsers {get; set;}
    }
}
