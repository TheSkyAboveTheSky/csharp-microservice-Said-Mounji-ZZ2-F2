using Microsoft.EntityFrameworkCore;
using ProjectService.Entities;

namespace ProjectService.Data
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

        public DbSet<Project> Project { get; set; } = default!;
    }
}


