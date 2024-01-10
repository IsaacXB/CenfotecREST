using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using REST.Database.Models;

namespace REST.Web.ClientAPI.Data
{
    public class RESTWebClientAPIContext : DbContext
    {
        public RESTWebClientAPIContext (DbContextOptions<RESTWebClientAPIContext> options)
            : base(options)
        {
        }

        public DbSet<REST.Database.Models.User> User { get; set; } = default!;
    }
}
