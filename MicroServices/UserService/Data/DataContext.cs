using Microsoft.EntityFrameworkCore;
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


