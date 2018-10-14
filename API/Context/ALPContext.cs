using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class ALPContext : DbContext
    {
        public ALPContext(DbContextOptions<ALPContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }
}
