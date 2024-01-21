using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using UserService.Entities;

namespace UserService.Data
{
    public class DataContext : DbContext

    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
             }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
           options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> User { get; set; } = default!;
    }
}


