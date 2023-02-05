using Microsoft.EntityFrameworkCore;
using PicWorldServer.Model;

namespace PicWorldServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Posts> Posts { get; set; }
    }
}
