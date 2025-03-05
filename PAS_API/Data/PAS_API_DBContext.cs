using Microsoft.EntityFrameworkCore;
using Model;

namespace PAS_API.Data
{
    public class PAS_API_DBContext : DbContext
    {
        public PAS_API_DBContext(DbContextOptions<PAS_API_DBContext> options) : base(options)
        {
        }
        public DbSet<Item>? Items { get; set; }
        public DbSet<Department>? Departments { get; set; }
    }

}
