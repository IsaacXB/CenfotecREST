using Microsoft.EntityFrameworkCore;
using REST.Database.Models;

namespace REST.Database.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Usuarios { get; set; }
    }
}
